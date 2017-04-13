
using Service_Konektor.poseidon;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace GeneratorDocx
{
    public class GeneratorDocx
    {
        private VSVlak[] _vlaky;
        private VSTrasaBod[] _trasaBody;
        private VSDopravnyBod _dopravnyBod;
        private Document _document;

        public GeneratorDocx(VSVlak[] vlaky, VSTrasaBod[] trasaBody, VSDopravnyBod dopravnyBod)
        {
            _vlaky = vlaky;
            _trasaBody = trasaBody;
            _dopravnyBod = dopravnyBod;
            _document = new Document();
            _document.LoadFromFile("vzor.docx");
            TextSelection selection = _document.FindString("#Stanica", true, true);
            TextRange range = selection.GetAsOneRange();
            Paragraph paragraph = range.OwnerParagraph;
            Body body = paragraph.OwnerTextBody;
            int index = body.ChildObjects.IndexOf(paragraph);

            Section section = _document.Sections[0];
            Paragraph para = section.AddParagraph();
            para.AppendText(_dopravnyBod.Nazov);
            body.ChildObjects.Remove(paragraph);
            body.ChildObjects.Insert(index, para);
        }

        public void GenerujDocxSubor()
        {
            _document.SaveToFile("result.docx", FileFormat.Auto);
            System.Diagnostics.Process.Start("result.docx");
        }
    }
}
