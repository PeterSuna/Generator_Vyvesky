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

namespace GeneratorVyvesky
{
    public class Generator
    {
        private VSTrasaBod[] _trasaBody;
        private VSDopravnyBod _dopravnyBod;
        private Document _document;

        public Generator()
        {

        }

        public Generator(VSTrasaBod[] trasaBody, VSDopravnyBod dopravnyBod)
        {
            _trasaBody = trasaBody.OrderBy(c => c.CasPrijazdu).ToArray();
            _dopravnyBod = dopravnyBod;
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
                para.AppendText(_dopravnyBod.Nazov);
                para.Format.HorizontalAlignment = HorizontalAlignment.Right;

                body.ChildObjects.Remove(paragraph);
                body.ChildObjects.Insert(index, para);
            }
           
        }

        public void GenerujDocxSubor()
        {
            VytvorHlavičku();
            NastavenieCasu(0);
            _document.SaveToFile("result.docx", FileFormat.Auto);
            System.Diagnostics.Process.Start("result.docx");
        }

        public void tableVlakov()
        {
            
        }

        public void NastavenieCasu(int h)
        {
            Section section = _document.Sections[1];
            Table table = section.AddTable(true);
            table.TableFormat.HorizontalAlignment = RowAlignment.Center;
            table.ResetCells(1, 1);
            TableRow DataRow = table.Rows[0];
            Paragraph p2 = DataRow.Cells[0].AddParagraph();
            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR2 = p2.AppendText(h+".00 - "+h+".59");
            TR2.CharacterFormat.FontSize = 7;
            DataRow.Cells[0].Width = 170;
        }

        public void NastavenieTabulkyVlakov(VSTrasaBod bod)
        {
            Section section = _document.Sections[1];
            Table table = section.AddTable(true);
            table.TableFormat.Paddings.All = 0.2f;
            table.TableFormat.HorizontalAlignment = RowAlignment.Center;
            table.ResetCells(1, 7);
            TableRow DataRow = table.Rows[0];
            Paragraph p1 = DataRow.Cells[0].AddParagraph();
            p1.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR1 = p1.AppendText(TimeSpan.FromSeconds(bod.CasPrijazdu).ToString("hh.mm"));
            TR1.CharacterFormat.FontSize = 7;
            DataRow.Cells[0].Width = 15;

            Paragraph p2 = DataRow.Cells[0].AddParagraph();
            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange TR2 = p2.AppendText(TimeSpan.FromSeconds(bod.CasPrijazdu).ToString("hh.mm"));
            TR2.CharacterFormat.FontSize = 7;
            DataRow.Cells[0].Width = 15;

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
                        table.Rows[r].Cells[c].Width = 15;
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
