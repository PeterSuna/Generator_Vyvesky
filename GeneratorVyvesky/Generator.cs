using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service_Konektor.poseidon;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using static System.Int32;

namespace GeneratorVyvesky
{
    public class Generator
    {
        private readonly VSTrasaBod[] _trasaBodyVlakov;
        private readonly VSTrasaBod[] _trasaBodyVybStanice;
        private readonly Document _document;
        private readonly VSTrasaDruh[] _druh;
        private readonly VSVlak[] _vlaky;
        private readonly VSDopravnyBod[] _dopravneBody;
        private readonly VSTrasaObecPozn[] _trasaObPoznamka;
        private readonly VSObecnaPoznamka[] _obecnaPoznamka;


        public Generator(VSTrasaBod[] trasaBodyVlakov, VSVlak[] vlaky,VSTrasaBod[] trasaBodyVybStanice, VSDopravnyBod dopravnyBod, string cesta)
        {
            _trasaBodyVlakov = trasaBodyVlakov.OrderBy(c => c.CasPrijazdu).ToArray();
            _druh = Zapisovac.Zapisovac.NacitajZoSuboruDopravneDruhy(cesta+ "\\TrasaDruh");
            _vlaky = vlaky;
            _trasaBodyVybStanice = trasaBodyVybStanice.OrderBy(c => TimeSpan.FromSeconds(c.CasPrijazdu)).ToArray();
            _dopravneBody = Zapisovac.Zapisovac.NacitajDopravneBodyZoSuboru(@"..\..\..\Projekt\");
            _trasaObPoznamka = Zapisovac.Zapisovac.NacitajZoSuboruTrasaObPozn(cesta + "\\Poznamky");
            _obecnaPoznamka = Zapisovac.Zapisovac.NacitajZoSuboruObecnuPoznam(cesta + "\\Poznamky");

            _document = new Document();
            _document.LoadFromFile(@"..\..\..\vzor.docx");
            for (int i = 0; i < 2; i++)
            {
                TextSelection selection = _document.FindString("#Stanica", true, true);
                TextRange range = selection.GetAsOneRange();
                Paragraph paragraph = range.OwnerParagraph;
                Body body = paragraph.OwnerTextBody;
                int index = body.ChildObjects.IndexOf(paragraph);

                Section section = _document.Sections[0];
                Paragraph para = section.AddParagraph();
                para.AppendText(dopravnyBod.Nazov);
                para.Format.HorizontalAlignment = HorizontalAlignment.Right;

                body.ChildObjects.Remove(paragraph);
                body.ChildObjects.Insert(index, para);
            }
           
        }

        public void GenerujDocxSubor()
        {
            int hodina =-1;
            VytvorHlavičku();
            
            for (int i = 0; i < _trasaBodyVybStanice.Length; i++)
            {
                if (hodina == Parse(TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh")))
                {
                    NastavenieTabulkyVlakov(_trasaBodyVybStanice[i]);
                }
                else
                {
                    hodina= Parse(TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh"));
                    NastavenieCasu(hodina);
                    NastavenieTabulkyVlakov(_trasaBodyVybStanice[i]);
                }
            }
           
            _document.SaveToFile("result.docx", FileFormat.Auto);
            System.Diagnostics.Process.Start("result.docx");
        }



        public void NastavenieCasu(int h)
        {
            Section section = _document.Sections[1];
            Table table = section.AddTable(true);
            table.ResetCells(1, 1);

            table.TableFormat.HorizontalAlignment = RowAlignment.Center;
            TableRow DataRow1 = table.Rows[0];
            Paragraph p2 = DataRow1.Cells[0].AddParagraph();
            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR2 = p2.AppendText(h+".00 - "+h+".59");
            TR2.CharacterFormat.FontSize = 7;
            DataRow1.Cells[0].Width = 170;
        }

        public void NastavenieTabulkyVlakov(VSTrasaBod trasBodStanice)
        {
            Section section = _document.Sections[1];
            Table table1 = section.AddTable(true);
            table1.ResetCells(1, 7);
            table1.TableFormat.Paddings.All = 0.2f;
            table1.TableFormat.HorizontalAlignment = RowAlignment.Center;
            table1.TableFormat.Positioning.HorizPositionAbs = HorizontalPosition.Outside;
            table1.TableFormat.Positioning.VertRelationTo = VerticalRelation.Margin;
            TableRow DataRow = table1.Rows[0];

            //čas
            Paragraph p1 = DataRow.Cells[0].AddParagraph();
            p1.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR1 = p1.AppendText(TimeSpan.FromSeconds(trasBodStanice.CasPrijazdu).ToString("hh")+"."+TimeSpan.FromSeconds(trasBodStanice.CasPrijazdu).ToString("hh"));
            TR1.CharacterFormat.FontSize = 5;
            DataRow.Cells[0].Width = 5;

            //druh
            Paragraph p2 = DataRow.Cells[1].AddParagraph();
            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR2 = p2.AppendText(FilterDat.TrasaDruh.NajdiDruhVlaku(trasBodStanice.VlakID,_druh));
            TR2.CharacterFormat.FontSize = 5;
            DataRow.Cells[1].Width = 12;

            //čislo
            Paragraph p3 = DataRow.Cells[2].AddParagraph();
            p3.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR3 = p3.AppendText(FilterDat.Vlak.ZisiteCisloVlaku(trasBodStanice.VlakID,_vlaky)+"");
            TR3.CharacterFormat.FontSize = 5;
            DataRow.Cells[2].Width = 10;

            //zo smeru
            string[] stanice = FilterDat.DopravnyBod.NajdiNazvyStanic(trasBodStanice,_trasaBodyVlakov, _dopravneBody);
            int[] casi = FilterDat.TrasaBod.NajdiCasiPrichodov(trasBodStanice,_trasaBodyVlakov);
            string text = "";
            for (int i = 0; i < stanice.Length; i++)
            {
                if (i == stanice.Length - 1)
                {
                    text += string.Format("{0}({1:%h}.{1:%m})", stanice[i], TimeSpan.FromSeconds(casi[i]));
                }
                else
                {
                    text += string.Format("{0}({1:%h}.{1:%m}) - ", stanice[i], TimeSpan.FromSeconds(casi[i]));
                }
            }
            Paragraph p4 = DataRow.Cells[3].AddParagraph();
            p4.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR4 = p4.AppendText(text);
            TR4.CharacterFormat.FontSize = 5;
            DataRow.Cells[3].Width = 93;

            //Poznamky
            Paragraph p5 = DataRow.Cells[4].AddParagraph();
            p5.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR5 = p5.AppendText(FilterDat.Poznamka.ZistiPoznamku(trasBodStanice.VlakID,_trasaObPoznamka,_obecnaPoznamka));
            TR5.CharacterFormat.FontSize = 5;
            DataRow.Cells[4].Width = 25;
            DataRow.Cells[5].Width = 12;
            DataRow.Cells[6].Width = 12;

        }

        public void VytvorHlavičku()
        {
            Section section = _document.Sections[1];
           
            Table table = section.AddTable(true);
            table.ResetCells(2, 7);
            table.ApplyVerticalMerge(0, 0, 1);
            table.ApplyHorizontalMerge(0, 1, 2);
            table.ApplyVerticalMerge(3,0,1);
            table.ApplyVerticalMerge(4,0,1);
            table.ApplyVerticalMerge(5,0,1);
            table.ApplyVerticalMerge(6,0,1);

            string[][] data = { new string[] {"Čas", "Vlak","", "Zo smeru", "Poznámky", "Nást.", "Kol." },
                                new string[]{ "","Druh", "Čislo" }};

            table.TableFormat.HorizontalAlignment = RowAlignment.Center;
            table.Rows[0].Cells[4].Width = 40;
            table.Rows[0].Cells[3].Width = 500;
            table.TableFormat.WrapTextAround = true;
            table.TableFormat.Positioning.HorizPositionAbs = HorizontalPosition.Outside;
            table.TableFormat.Positioning.VertRelationTo = VerticalRelation.Margin;
            table.TableFormat.Paddings.All = 0;

            for (int r = 0; r < data.Length; r++)
            {
                TableRow DataRow = table.Rows[r];

                for (int c = 0; c < data[r].Length; c++)
                {
                    if ((r == 0 && c == 2) || (r == 1 && c == 0) || (r == 1 && c > 2))
                    {
                        continue;
                    }

                    if (c != 3 && c != 4)
                    {
                        if (c == 0)
                        {
                            table.Rows[r].Cells[c].Width = 18;
                        }
                        else
                        {
                            if (c == 2 && r == 1)
                            {
                                table.Rows[r].Cells[c].Width = 17;
                            }
                            else
                            {
                                table.Rows[r].Cells[c].Width = 15;
                            }
                        }
                       
                    }

                    DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                    DataRow.RowFormat.HorizontalAlignment = RowAlignment.Center;
                    Paragraph p2 = DataRow.Cells[c].AddParagraph();
                    TextRange TR2 = p2.AppendText(data[r][c]);
                    TR2.CharacterFormat.FontSize = 5;
                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                }

            }
        }
    }
}

