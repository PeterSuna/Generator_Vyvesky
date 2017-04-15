using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace TestGeneratora
{
    class Program
    {
        static void Main(string[] args)
        {
            //Document document = new Document();
            string s = @"Kelebia(3.4) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Beograd(21.50) - Novi Beograd(3.14) - Nova Pazova(3.14) - Stara Pazova(22.27) - Indija(22.36) - Novi Sad(23.16) - Vrbas(0.5) - Bačka Topola(0.45) - Subotica(1.41) - Kelebia(2.30) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Budapest - Keleti pu.(22.25) - Ferencváros(3.14) - Soroksári út(3.14) - Kunszentmiklós - Tass(3.14) - Szabadszállás(3.14) - Fülöpszállás(3.14) - Soltszentimre(3.14) - Csengöd(3.14)
Kelebia(3.4) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Beograd(21.50) - Novi Beograd(3.14) - Nova Pazova(3.14) - Stara Pazova(22.27) - Indija(22.36) - Novi Sad(23.16) - Vrbas(0.5) - Bačka Topola(0.45) - Subotica(1.41) - Kelebia(2.30) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Budapest - Keleti pu.(22.25) - Ferencváros(3.14) - Soroksári út(3.14) - Kunszentmiklós - Tass(3.14) - Szabadszállás(3.14) - Fülöpszállás(3.14) - Soltszentimre(3.14) - Csengöd(3.14)
Kelebia(3.4) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Beograd(21.50) - Novi Beograd(3.14) - Nova Pazova(3.14) - Stara Pazova(22.27) - Indija(22.36) - Novi Sad(23.16) - Vrbas(0.5) - Bačka Topola(0.45) - Subotica(1.41) - Kelebia(2.30) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Budapest - Keleti pu.(22.25) - Ferencváros(3.14) - Soroksári út(3.14) - Kunszentmiklós - Tass(3.14) - Szabadszállás(3.14) - Fülöpszállás(3.14) - Soltszentimre(3.14) - Csengöd(3.14)
Kelebia(3.4) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Beograd(21.50) - Novi Beograd(3.14) - Nova Pazova(3.14) - Stara Pazova(22.27) - Indija(22.36) - Novi Sad(23.16) - Vrbas(0.5) - Bačka Topola(0.45) - Subotica(1.41) - Kelebia(2.30) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Budapest - Keleti pu.(22.25) - Ferencváros(3.14) - Soroksári út(3.14) - Kunszentmiklós - Tass(3.14) - Szabadszállás(3.14) - Fülöpszállás(3.14) - Soltszentimre(3.14) - Csengöd(3.14)
Kelebia(3.4) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Beograd(21.50) - Novi Beograd(3.14) - Nova Pazova(3.14) - Stara Pazova(22.27) - Indija(22.36) - Novi Sad(23.16) - Vrbas(0.5) - Bačka Topola(0.45) - Subotica(1.41) - Kelebia(2.30) - Kiskunhalas(3.14) - Soltvadkert(3.14)
Budapest - Keleti pu.(22.25) - Ferencváros(3.14) - Soroksári út(3.14) - Kunszentmiklós - Tass(3.14) - Szabadszállás(3.14) - Fülöpszállás(3.14) - Soltszentimre(3.14) - Csengöd(3.14)
";
            Console.WriteLine(s.Length);
            Console.ReadKey();
            //document.LoadFromFile("vzor.docx");
            //Section section = document.Sections[1];


            //Table table = section.AddTable(true);
            //table.ResetCells(1, 1);
            //TableRow DataRow = table.Rows[0];
            //Paragraph p2 = DataRow.Cells[0].AddParagraph();
            //p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //TextRange TR2 = p2.AppendText("0.00 - 0.59");
            //TR2.CharacterFormat.FontSize = 12;
            //table.AddRow(false, 2);
            //TableRow DataRow1 = table.Rows[1];
            //Paragraph p3 = DataRow1.Cells[0].AddParagraph();
            //p3.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //TextRange TR3 = p3.AppendText("0.00 - 0.59");
            //TR3.CharacterFormat.FontSize = 12;
            //Paragraph p4 = DataRow1.Cells[1].AddParagraph();
            //p4.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //TextRange TR4 = p4.AppendText("0.00 - 0.59");
            //TR4.CharacterFormat.FontSize = 12;


            ////Paragraph para = document.Sections[0].AddParagraph();
            ////para.AppendText("halo");
            ////Paragraph para1 = document.Sections[1].AddParagraph();
            ////para1.AppendText("halo2");
            ////Paragraph para2 = document.Sections[2].AddParagraph();
            ////para2.AppendText("halo1");

            //document.SaveToFile("result.docx", FileFormat.Auto);
            //System.Diagnostics.Process.Start("result.docx");

            //Document document = new Document();
            //Section section = document.AddSection();
            //section.PageSetup.PageSize = PageSize.A4;
            //section.PageSetup.Orientation = PageOrientation.Landscape;
            //Paragraph para1 = section.Paragraphs[0];
            //para1.Text = "Spire.Doc for para1";
            //document.Sections[0].AddColumn(200f, 20f);
            //Section section2 = document.AddSection();
            //Paragraph para2 = section2.Paragraphs[0];
            //para2.Text = "Spire.Doc para2";
            //Section section3 = document.AddSection();
            //document.Sections[2].AddColumn(200f, 20f);

            ////Create Table
            //Section s = document.AddSection();
            //Table table = s.AddTable(true);


            //String[] Header = { "Item", "Description", "Qty", "Unit Price", "Price" };
            //String[][] data = {
            //                      new String[]{ "Spire.Doc for .NET",".NET Word Component","1","$799.00","$799.00"},
            //                      new String[]{"Spire.XLS for .NET",".NET Excel Component","2","$799.00","$1,598.00"},
            //                      new String[]{"Spire.Office for .NET",".NET Office Component","1","$1,899.00","$1,899.00"},
            //                      new String[]{"Spire.PDF for .NET",".NET PDFComponent","2","$599.00","$1,198.00"},
            //                  };
            ////Add Cells
            //table.ResetCells(data.Length + 1, Header.Length);


            //TableRow FRow = table.Rows[0];
            //FRow.IsHeader = true;
            //FRow.Height = 23;
            //FRow.RowFormat.BackColor = Color.AliceBlue;
            //for (int i = 0; i < Header.Length; i++)
            //{
            //    Paragraph p = FRow.Cells[i].AddParagraph();
            //    FRow.Cells[i].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //    p.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //    TextRange TR = p.AppendText(Header[i]);
            //    TR.CharacterFormat.FontName = "Calibri";
            //    TR.CharacterFormat.FontSize = 14;
            //    TR.CharacterFormat.TextColor = Color.Teal;
            //    TR.CharacterFormat.Bold = true;
            //}

            //for (int r = 0; r < data.Length; r++)
            //{
            //    TableRow DataRow = table.Rows[r + 1];

            //    DataRow.Height = 20;


            //    for (int c = 0; c < data[r].Length; c++)
            //    {
            //        DataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Middle;
            //        Paragraph p2 = DataRow.Cells[c].AddParagraph();
            //        TextRange TR2 = p2.AppendText(data[r][c]);
            //        p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            //        TR2.CharacterFormat.FontName = "Calibri";
            //        TR2.CharacterFormat.FontSize = 12;
            //        TR2.CharacterFormat.TextColor = Color.Brown;
            //    }
            //}



            //document.SaveToFile(@"..\..\..\Edit Word.docx", FileFormat.Docx);
            //System.Diagnostics.Process.Start("Edit Word.docx");
        }
    }
}
