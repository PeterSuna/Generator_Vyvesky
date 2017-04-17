using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly VSDopravnyBod _dopravnyBod;
        private readonly VSProject _projekt;


        public Generator(VSTrasaBod[] trasaBodyVlakov, VSVlak[] vlaky, VSTrasaBod[] trasaBodyVybStanice,
            VSDopravnyBod dopravnyBod, string cesta, VSProject projekt)
        {
            //načítanie potrebných údajov
            _trasaBodyVlakov = trasaBodyVlakov;
            _druh = DataZoSuboru.Nacitaj.ZoSuboruDopravneDruhy(cesta + "\\TrasaDruh");
            _vlaky = vlaky;
            _trasaBodyVybStanice =
                trasaBodyVybStanice.OrderBy(c => Parse(TimeSpan.FromSeconds(c.CasPrijazdu).ToString("hh"))).ToArray();
            _dopravneBody = DataZoSuboru.Nacitaj.DopravneBodyZoSuboru(@"..\..\..\Projekt\");
            _trasaObPoznamka = DataZoSuboru.Nacitaj.ZoSuboruTrasaObPozn(cesta + "\\Poznamky");
            _obecnaPoznamka = DataZoSuboru.Nacitaj.ZoSuboruObecnuPoznam(cesta + "\\Poznamky");
            _dopravnyBod = dopravnyBod;
            _projekt = projekt;
            _document = new Document();
        }

        /// <summary>
        /// hlavná metóda ktorá v cykle prejde všetky vlaky ktoré majú trasu cez vybranú stanicu a vypíše ich
        /// </summary>
        public void GenerujDocxSubor()
        {
            _document.LoadFromFile(@"..\..\..\vzorP.docx");
            nastavDokument();
            int hodina =-1;
            VytvorHlavičku();
            int row = 0;
            Table table =null;
            int i = 0;
            int zlomy = 0;
            int znaky = 0; //2450
            while(zlomy < 4 && _trasaBodyVybStanice.Length > i)
            {
                string text = FilterDat.DopravnyBod.VytvorTextZoSmeru(_trasaBodyVybStanice[i], _trasaBodyVlakov, _dopravneBody);
                string poznamka = FilterDat.Poznamka.ZistiPoznamku(_trasaBodyVybStanice[i].VlakID, _trasaObPoznamka, _obecnaPoznamka);
                if (text == "" || text.Length >= 2300 || !FilterDat.TrasaDruh.ZisitiSpravnyDruhVlaku(_trasaBodyVybStanice[i].VlakID, _druh))
                {
                    i++;
                    continue;
                }
                //riadok zbiera info o tom kolko je vypísaného textu stĺpci aby to nepresiahlo jednu stranu
                int riadok = (poznamka!=null && poznamka.Length*5 > text.Length) ? poznamka.Length*5 : text.Length;
                znaky += riadok;
                string cas = TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh") + "." + TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("mm");
                //rozhodnutie či vypísať hlavičku z časom
                if (hodina == Parse(TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh")))
                {
                    //približný počet kolko znakou sa vopchá do tabulky na jednu stranu
                    if (znaky >= 2450)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        VytvorHlavičku();
                        _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        row = 0;
                        table = vytvorTabulku();
                        znaky = riadok;
                    }
                    NastavenieTabulkyVlakov(_trasaBodyVybStanice[i], table, row,text,poznamka,cas);
                    row++;
                }
                else
                {
                    znaky += 40;
                    hodina = Parse(TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh"));
                    if (znaky >= 2450)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        VytvorHlavičku();
                        _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        znaky = riadok;
                    }
                    NastavenieCasu(hodina);
                    row = 0;
                    table = vytvorTabulku();
                    NastavenieTabulkyVlakov(_trasaBodyVybStanice[i],table,row,text,poznamka,cas);
                    row++;
                }
                i++;
            }
           
            _document.SaveToFile("result.docx", FileFormat.Auto);
            System.Diagnostics.Process.Start("result.docx");
        }

        /// <summary>
        /// hlavná metóda ktorá v cykle prejde všetky vlaky ktoré majú trasu cez vybranú stanicu a vypíše ich
        /// </summary>
        public void GenerujOdchodyDocxSubor()
        {
            _document.LoadFromFile(@"..\..\..\vzorO.docx");
            nastavDokument();
            int hodina = -1;
            VytvorHlavičku();
            int row = 0;
            Table table = null;
            int i = 0;
            int zlomy = 0;
            int znaky = 0; //2450
            while (zlomy < 4 && _trasaBodyVybStanice.Length > i)
            {
                string text = FilterDat.DopravnyBod.VytvorTextOdchodovZoSmeru(_trasaBodyVybStanice[i], _trasaBodyVlakov, _dopravneBody);
                string poznamka = FilterDat.Poznamka.ZistiPoznamku(_trasaBodyVybStanice[i].VlakID, _trasaObPoznamka, _obecnaPoznamka);
                if (text == "" || text.Length >= 2300 || !FilterDat.TrasaDruh.ZisitiSpravnyDruhVlaku(_trasaBodyVybStanice[i].VlakID, _druh))
                {
                    i++;
                    continue;
                }
                //riadok zbiera info o tom kolko je vypísaného textu stĺpci aby to nepresiahlo jednu stranu
                int riadok = (poznamka != null && poznamka.Length * 5 > text.Length) ? poznamka.Length * 5 : text.Length;
                znaky += riadok;
                string cas = TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasOdjazdu).ToString("hh") + "." + TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasOdjazdu).ToString("mm");
                //rozhodnutie či vypísať hlavičku z časom
                if (hodina == Parse(TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh")))
                {
                    //približný počet kolko znakou sa vopchá do tabulky na jednu stranu
                    if (znaky >= 2450)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        VytvorHlavičku();
                        _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        row = 0;
                        table = vytvorTabulku();
                        znaky = riadok;
                    }
                    NastavenieTabulkyVlakov(_trasaBodyVybStanice[i], table, row, text, poznamka,cas);
                    row++;
                }
                else
                {
                    znaky += 40;
                    hodina = Parse(TimeSpan.FromSeconds(_trasaBodyVybStanice[i].CasPrijazdu).ToString("hh"));
                    if (znaky >= 2450)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        VytvorHlavičku();
                        _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        znaky = riadok;
                    }
                    NastavenieCasu(hodina);
                    row = 0;
                    table = vytvorTabulku();
                    NastavenieTabulkyVlakov(_trasaBodyVybStanice[i], table, row, text, poznamka,cas);
                    row++;
                }
                i++;
            }

            _document.SaveToFile("result.docx", FileFormat.Auto);
            System.Diagnostics.Process.Start("result.docx");
        }




        /// <summary>
        /// Vloží hlavičku kedy daný vlak prechádza stanicou
        /// </summary>
        /// <param name="h"> hodina rozpetia daných vlakov</param>
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

        /// <summary>
        /// vytvorý tabulku a vráti jej inštanciu
        /// </summary>
        /// <returns></returns>
        public Table vytvorTabulku()
        {
            Section section = _document.Sections[1];
            Table table1 = section.AddTable(true);
            table1.ResetCells(1, 7);
            table1.TableFormat.Paddings.All = 0.2f;
            table1.TableFormat.HorizontalAlignment = RowAlignment.Center;
            table1.TableFormat.Positioning.HorizPositionAbs = HorizontalPosition.Outside;
            table1.TableFormat.Positioning.VertRelationTo = VerticalRelation.Margin;
            return table1;
        }


        /// <summary>
        /// Do dokumentu pridá vlak ktorý prechádza danou stanicou
        /// </summary>
        /// <param name="trasBodStanice"> vybraná trasa vlaku pre ktorý sa robý výpis</param>
        /// <param name="table"> tabulka do ktorej sa vloží info o vlaku</param>
        /// <param name="riadok"> na akú pozíciu riadku v tabulke sa zapíše info o vlaku</param>
        /// <param name="text"> zoznam staníc a časou cez ktoré prechádza daný vlak </param>
        /// <param name="poznamka"> poznámka priradená k danému vlaku</param>
        public void NastavenieTabulkyVlakov(VSTrasaBod trasBodStanice, Table table, int riadok, string text, string poznamka, string cas)
        {
            if (riadok != 0)
            {
                table.AddRow(true, 7);
            }

            TableRow DataRow = table.Rows[riadok];

            //čas
            Paragraph p1 = DataRow.Cells[0].AddParagraph();
            p1.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR1 = p1.AppendText(TimeSpan.FromSeconds(trasBodStanice.CasPrijazdu).ToString("hh")+"."+TimeSpan.FromSeconds(trasBodStanice.CasPrijazdu).ToString("mm"));
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
           
            Paragraph p4 = DataRow.Cells[3].AddParagraph();
            p4.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR4 = p4.AppendText(text);
            TR4.CharacterFormat.FontSize = 5;
            DataRow.Cells[3].Width = 93;

            //Poznamky
            Paragraph p5 = DataRow.Cells[4].AddParagraph();
            p5.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR5 = p5.AppendText(poznamka);
            TR5.CharacterFormat.FontSize = 5;
            DataRow.Cells[4].Width = 25;
            DataRow.Cells[5].Width = 12;
            DataRow.Cells[6].Width = 12;

        }

        /// <summary>
        /// V dokumente vytvorý hlavičku z metadátami
        /// </summary>
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
            table.TableFormat.WrapTextAround = true;
            table.TableFormat.Positioning.HorizPositionAbs = HorizontalPosition.Outside;
            table.TableFormat.Positioning.VertRelationTo = VerticalRelation.Margin;
            table.TableFormat.Paddings.All = 0;
            table.ColumnWidth[4] = 300;

            for (int r = 0; r < data.Length; r++)
            {
                TableRow DataRow = table.Rows[r];

                for (int c = 0; c < data[r].Length; c++)
                {
                    if ((r == 0 && c == 2) || (r == 1 && c == 0) || (r == 1 && c > 2))
                    {
                        continue;
                    }
                    switch (c)
                    {
                        case 0:
                            table.Rows[r].Cells[c].Width = 18;
                            break;
                        case 2:
                            if (r == 1)
                            {
                                table.Rows[r].Cells[c].Width = 17;
                            }
                            break;
                        case 3:
                            table.Rows[r].Cells[c].Width = 500;
                            break;
                        case 4:
                            table.Rows[r].Cells[c].Width = 40;
                            break;
                        default:
                            table.Rows[r].Cells[c].Width = 15;
                            break;
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

        /// <summary>
        /// Prepísanie #Stanice na stanicu pre kotú sa robý vývseka a nastavenie času platnosti podla vybraného projektu
        /// </summary>
        private void nastavDokument()
        {
            ParagraphStyle style = new ParagraphStyle(_document);
            style.Name = "FontStyle";
            style.CharacterFormat.FontSize = 18;
            style.CharacterFormat.Bold = true;
            _document.Styles.Add(style);
            CultureInfo ci = CultureInfo.InvariantCulture;
            // prepíše v pripravenom dokumente #Stanica za vybranú stanicu a #Datum za datum v projekte
            for (int i = 0; i < 2; i++)
            {
                //nazov stanice
                TextSelection selection = _document.FindString("#Stanica", true, true);
                TextRange range = selection.GetAsOneRange();
                Paragraph paragraph = range.OwnerParagraph;
                Body body = paragraph.OwnerTextBody;
                int index = body.ChildObjects.IndexOf(paragraph);

                Section section = _document.Sections[0];
                Paragraph para = section.AddParagraph();
                para.AppendText(_dopravnyBod.Nazov);
                para.ApplyStyle(style.Name);
                para.Format.HorizontalAlignment = HorizontalAlignment.Right;

                body.ChildObjects.Remove(paragraph);
                body.ChildObjects.Insert(index, para);

                //datum
                TextSelection selectionOd = _document.FindString("#Datum", true, true);
                TextRange rangeOd = selectionOd.GetAsOneRange();
                Paragraph paragraphOd = rangeOd.OwnerParagraph;
                Body bodyOd = paragraphOd.OwnerTextBody;
                int indexOd = bodyOd.ChildObjects.IndexOf(paragraphOd);

                Section sectionOd = _document.Sections[0];
                Paragraph paraOd = sectionOd.AddParagraph();
                string text = "Platí od " + _projekt.PlatnostOd.ToString("dd.MM.yyyy", ci) + " do " +
                              _projekt.PlatnostDo.ToString("dd.MM.yyyy", ci);
                paraOd.AppendText(text);
                paraOd.Format.HorizontalAlignment = HorizontalAlignment.Center;
                bodyOd.ChildObjects.Remove(paragraphOd);
                bodyOd.ChildObjects.Insert(indexOd, paraOd);
            }
        }

    }
}

