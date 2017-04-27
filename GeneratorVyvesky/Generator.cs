using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Service_Konektor.Entity;
using Service_Konektor.poseidon;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace GeneratorVyvesky
{
    public class Generator
    {
        private readonly MapTrasaBod[] _trasaBodyVlakov;
        private readonly Document _document;
        private readonly MapTrasaDruh[] _druh;
        private readonly MapVlak[] _vlaky;
        private readonly MapDopravnyBod[] _dopravneBody;
        private readonly MapTrasaObecPozn[] _trasaObPoznamka;
        private readonly VSObecnaPoznamka[] _obecnaPoznamka;
        private readonly MapDopravnyBod _dopravnyBod;
        private readonly VSProject _projekt;

        public MapTrasaBod[] TrasaBodyVybStanice { get; }

        /// <summary>
        /// Vytvorenie inštancie generátora
        /// </summary>
        /// <param name="dopravnyBod"></param>
        /// <param name="cesta"></param>
        /// <param name="projekt"></param>
        public Generator(MapDopravnyBod dopravnyBod, string cesta, VSProject projekt)
        {
            //nacítanie potrebných údajov zo súboru
            string cestaProj = Path.GetDirectoryName(cesta);
            _dopravneBody = DataZoSuboru.Nacitaj.ZoSuboru<MapDopravnyBod[]>(Path.Combine(cestaProj, "MapDopravneBody.json"));
            _trasaObPoznamka = DataZoSuboru.Nacitaj.ZoSuboru<MapTrasaObecPozn[]>(Path.Combine(cesta, "MapTrasaObPoznamky.json"));
            _obecnaPoznamka = DataZoSuboru.Nacitaj.ZoSuboru<VSObecnaPoznamka[]>(Path.Combine(cestaProj, "ObecnaPoznamka.json"));
            _druh = DataZoSuboru.Nacitaj.ZoSuboru<MapTrasaDruh[]>(Path.Combine(cesta, "MapDopravneDruhy.json"));
            var trasaBody = DataZoSuboru.Nacitaj.ZoSuboru<MapTrasaBod[]>(Path.Combine(cesta, "MapTrasaBody.json"));
            var mapDu = DataZoSuboru.Nacitaj.ZoSuboru<MapDopravnyUsek[]>(Path.Combine(cestaProj, "MapDopravneUseky.json"));
            var vlaky = DataZoSuboru.Nacitaj.ZoSuboru<MapVlak[]>(Path.Combine(cesta, "MapVlaky.json"));

            if (_dopravneBody == null ||_druh == null || trasaBody == null || mapDu == null || vlaky == null)
            {
                throw new ApplicationException();
            }
            //Upravenie potrebných údajov
            var trasaBodyVybStanice = FilterDat.TrasaBod.NajdiPodlaDopravnehoBodu(dopravnyBod.ID, trasaBody);
            _vlaky = FilterDat.Vlak.NajdiVlakyVTrasaBody(trasaBodyVybStanice,vlaky);
            var trasiVlakov = FilterDat.TrasaBod.NajdiTrasyPoldaVlaku(_vlaky, trasaBody);                   
            _trasaBodyVlakov = FilterDat.TrasaBod.NajdiDopravnéUzly(mapDu, trasiVlakov);
            TrasaBodyVybStanice = trasaBodyVybStanice.GroupBy(c => c.VlakID).Select(c=>c.First()).ToArray();
            
            _dopravnyBod = dopravnyBod;
            _projekt = projekt;
            _document = new Document();
        }

        /// <summary>
        /// hlavná metóda ktorá v cykle prejde všetky vlaky ktoré majú trasu cez vybranú stanicu a vypíše ich
        /// </summary>
        public int GenerujPrichodyDocxSubor(int index)
        {
            _document.LoadFromFile(@"Data\vzorP.docx");
            var trasaBodyVybStanciePrich = TrasaBodyVybStanice.OrderBy(c => c.CasPrijazdu).ToArray();
            NastavDokument();
            int hodina =-1; 
            int row = 0;
            Table table = VytvorHlavičku();
            bool prvy = true;
            int i = index;
            int zlomy = 0;
            int znaky = 0; //2450
            while(zlomy < 4 && trasaBodyVybStanciePrich.Length > i)
            {
                string text = FilterDat.DopravnyBod.VytvorTextZoSmeru(trasaBodyVybStanciePrich[i], _trasaBodyVlakov, _dopravneBody);
                string poznamka = FilterDat.Poznamka.ZistiPoznamku(trasaBodyVybStanciePrich[i].VlakID, _trasaObPoznamka, _obecnaPoznamka);
                if (text == "" || text.Length >= 2300 || !FilterDat.TrasaDruh.ZisitiSpravnyDruhVlaku(trasaBodyVybStanciePrich[i].VlakID, _druh))
                {
                    i++;
                    continue;
                }
                //riadok zbiera info o tom kolko je vypísaného textu stĺpci aby to nepresiahlo jednu stranu
                int riadok = (poznamka!=null && (poznamka.Length * 3) +30 > text.Length) ? (poznamka.Length * 3) +30 : text.Length;
                riadok = (riadok < 75) ? 75 : riadok;   //ak je text menší ako by mal zabrať miesta
                znaky += riadok;
                string cas = TimeSpan.FromSeconds(trasaBodyVybStanciePrich[i].CasPrijazdu).ToString("hh") + "." + TimeSpan.FromSeconds(trasaBodyVybStanciePrich[i].CasPrijazdu).ToString("mm");
                //rozhodnutie či vypísať hlavičku z časom
                if (hodina == int.Parse(TimeSpan.FromSeconds(trasaBodyVybStanciePrich[i].CasPrijazdu).ToString("hh")))
                {
                    //približný počet kolko znakou sa vopchá do tabulky na jednu stranu
                    if (znaky >= 2650)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        if (zlomy > 3)
                        {
                            break;
                        }
                        table = VytvorHlavičku();
                        // _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        row = 2;
                        znaky = riadok;
                        //prvy = true;
                    }
                    NastavenieTabulkyVlakov(trasaBodyVybStanciePrich[i], table, row, text, poznamka, cas);
                    row++;
                }
                else
                {
                    znaky += 60;    //vypisanie asi jeden riadok 
                    hodina = int.Parse(TimeSpan.FromSeconds(trasaBodyVybStanciePrich[i].CasPrijazdu).ToString("hh"));
                    if (znaky >= 2650)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        if (zlomy > 3)
                        {
                            break;
                        }
                        table = VytvorHlavičku();
                        row = 2;
                        // _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        znaky = riadok;

                    }
                    //NastavenieCasu(hodina);
                    if (prvy)
                    {
                        prvy = false;
                        table.AddRow(false, 7);
                        table.ApplyHorizontalMerge(2, 0, 6);
                        nastavCas(table, hodina, 2);
                        NastavenieTabulkyVlakov(trasaBodyVybStanciePrich[i], table, 3, text, poznamka, cas);
                        row = 3;
                    }
                    else
                    {
                        _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        //table = VytvorTabulkuSCasom();
                        table.AddRow(false, 7);
                        table.ApplyHorizontalMerge(row, 0, 6);
                        nastavCas(table, hodina, row);
                        row++;
                        //nastavCas(table, hodina, 0);
                        NastavenieTabulkyVlakov(trasaBodyVybStanciePrich[i], table, row, text, poznamka, cas);
                    }

                    row++;
                }
                i++;
            }
            _document.SaveToFile("result"+i+".docx", FileFormat.Docm2013);
            System.Diagnostics.Process.Start("result"+i+".docx");
            return i;
        }

        /// <summary>
        /// hlavná metóda ktorá v cykle prejde všetky vlaky ktoré majú trasu cez vybranú stanicu a vypíše ich
        /// </summary>
        public int GenerujOdchodyDocxSubor(int index)
        {
            _document.LoadFromFile(@"Data\vzorO.docx");     //načíta vzor pre Odchody vlakov
            var trasaBodyVybStancieOdch = TrasaBodyVybStanice.OrderBy(c => c.CasOdjazdu).ToArray();
            NastavDokument();                                   //prednastavý potrebné veci na dokumente
            int hodina = -1;                                    //ukladá hodinu v akej odchádza vlak
            int row = 0;                                        //riadok na akom sa nachádzam vo vytvorenej tabulke
            bool prvy = true;                                   //ak potrebujem oddeliť abulky
            Table table = VytvorHlavičku();
            int i = index;
            int zlomy = 0;                                      //počíta zlomy strán aby sa vytvoril práve jeden dokument
            int znaky = 0; //2450
            while (zlomy < 4 && trasaBodyVybStancieOdch.Length > i)
            {
                string text = FilterDat.DopravnyBod.VytvorTextOdchodovZoSmeru(trasaBodyVybStancieOdch[i], _trasaBodyVlakov, _dopravneBody);
                string poznamka = FilterDat.Poznamka.ZistiPoznamku(trasaBodyVybStancieOdch[i].VlakID, _trasaObPoznamka, _obecnaPoznamka);
                if (text == "" || text.Length >= 2300 || !FilterDat.TrasaDruh.ZisitiSpravnyDruhVlaku(trasaBodyVybStancieOdch[i].VlakID, _druh))
                {
                    i++;
                    continue;
                }
                //riadok zbiera info o tom kolko je vypísaného textu stĺpci aby to nepresiahlo jednu stranu
                int riadok = (poznamka != null && (poznamka.Length * 3) + 30 > text.Length) ? (poznamka.Length * 3) + 30 : text.Length;
                riadok = (riadok < 75) ? 75 : riadok;   //ak je text menší ako by mal zabrať miesta
                znaky += riadok;
                string cas = TimeSpan.FromSeconds(trasaBodyVybStancieOdch[i].CasOdjazdu).ToString("hh") + "." + TimeSpan.FromSeconds(trasaBodyVybStancieOdch[i].CasOdjazdu).ToString("mm");

                //rozhodnutie či vypísať hlavičku z časom
                if (hodina == int.Parse(TimeSpan.FromSeconds(trasaBodyVybStancieOdch[i].CasOdjazdu).ToString("hh")))
                {
                    //približný počet kolko znakou sa vopchá do tabulky na jednu stranu
                    if (znaky >= 2650)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        if (zlomy > 3)
                        {
                            break;
                        }
                        table = VytvorHlavičku();
                       // _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        row = 2;
                        znaky = riadok;
                        //prvy = true;
                    }
                    NastavenieTabulkyVlakov(trasaBodyVybStancieOdch[i], table, row, text, poznamka,cas);
                    row++;
                }
                else
                {
                    znaky += 60;    //vypisanie asi jeden riadok 
                    hodina = int.Parse(TimeSpan.FromSeconds(trasaBodyVybStancieOdch[i].CasPrijazdu).ToString("hh"));
                    if (znaky >= 2650)
                    {
                        _document.Sections[1].AddParagraph().AppendBreak(BreakType.ColumnBreak);
                        zlomy++;
                        if (zlomy > 3)
                        {
                            break;
                        }
                       table = VytvorHlavičku();
                        row = 2;
                       // _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        znaky = riadok;

                    }
                  //NastavenieCasu(hodina);
                    if (prvy)
                    {
                        prvy = false;
                        table.AddRow(false, 7);
                        table.ApplyHorizontalMerge(2, 0, 6);
                        nastavCas(table, hodina, 2);
                        NastavenieTabulkyVlakov(trasaBodyVybStancieOdch[i], table, 3, text, poznamka, cas);
                        row = 3;
                    }
                    else
                    {
                        _document.Sections[1].AddParagraph().Format.LineSpacing = 0.1f;
                        //table = VytvorTabulkuSCasom();
                        table.AddRow(false, 7);
                        table.ApplyHorizontalMerge(row, 0, 6);
                        nastavCas(table, hodina, row);
                        row++;
                        //nastavCas(table, hodina, 0);
                        NastavenieTabulkyVlakov(trasaBodyVybStancieOdch[i], table, row, text, poznamka, cas);
                    }
                    
                    row++;
                }
                i++;
            }

            _document.SaveToFile("Odchody"+i+".docx", FileFormat.Docm2013);
            System.Diagnostics.Process.Start("Odchody" +i + ".docx");
            return i;
        }


        /// <summary>
        /// Pre vytvorenú tabulki 1x1 vloží čas
        /// </summary>
        /// <param name="table"></param>
        /// <param name="h"></param>
        /// <param name="row"></param>
        private void nastavCas(Table table, int h,int row)
        {
            TableRow dataRow1 = table.Rows[row];
            Paragraph p2 = dataRow1.Cells[0].AddParagraph();
            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tR2 = p2.AppendText(h + ".00 - " + h + ".59");
            tR2.CharacterFormat.FontSize = 7;
            dataRow1.Cells[0].Width = 150;
        }


        /// <summary>
        /// Do dokumentu pridá vlak ktorý prechádza danou stanicou
        /// </summary>
        /// <param name="trasBodStanice"> vybraná trasa vlaku pre ktorý sa robý výpis</param>
        /// <param name="table"> tabulka do ktorej sa vloží info o vlaku</param>
        /// <param name="riadok"> na akú pozíciu riadku v tabulke sa zapíše info o vlaku</param>
        /// <param name="text"> zoznam staníc a časou cez ktoré prechádza daný vlak </param>
        /// <param name="poznamka"> poznámka priradená k danému vlaku</param>
        /// <param name="cas"></param>
        private void NastavenieTabulkyVlakov(MapTrasaBod trasBodStanice, Table table, int riadok, string text, string poznamka, string cas)
        {
            table.AddRow(false, 7);

            TableRow dataRow = table.Rows[riadok];

            //čas
            Paragraph p1 = dataRow.Cells[0].AddParagraph();
            p1.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tR1 = p1.AppendText(cas);
            tR1.CharacterFormat.FontSize = 5;
            dataRow.Cells[0].Width = 5;

            //druh
            Paragraph p2 = dataRow.Cells[1].AddParagraph();
            p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tR2 = p2.AppendText(FilterDat.TrasaDruh.NajdiDruhVlaku(trasBodStanice.VlakID,_druh));
            tR2.CharacterFormat.FontSize = 5;
            dataRow.Cells[1].Width = 12;

            //čislo
            Paragraph p3 = dataRow.Cells[2].AddParagraph();
            p3.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tR3 = p3.AppendText(FilterDat.Vlak.ZisiteCisloVlaku(trasBodStanice.VlakID,_vlaky)+"");
            tR3.CharacterFormat.FontSize = 5;
            dataRow.Cells[2].Width = 10;
           
            //zo smeru
            Paragraph p4 = dataRow.Cells[3].AddParagraph();
            p4.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tR4 = p4.AppendText(text);
            tR4.CharacterFormat.FontSize = 5;
            dataRow.Cells[3].Width = 93;
            dataRow.Cells[3].CellFormat.VerticalAlignment = VerticalAlignment.Middle;

            //Poznamky
            Paragraph p5 = dataRow.Cells[4].AddParagraph();
            p5.Format.HorizontalAlignment = HorizontalAlignment.Center;
            TextRange tR5 = p5.AppendText(poznamka);
            tR5.CharacterFormat.FontSize = 5;
            dataRow.Cells[4].Width = 25;
            dataRow.Cells[5].Width = 12;
            dataRow.Cells[6].Width = 12;

        }

        /// <summary>
        /// V dokumente vytvorý hlavičku z metadátami
        /// </summary>
        private Table VytvorHlavičku()
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

            string[][] data = { new [] {"Čas", "Vlak","", "Zo smeru", "Poznámky", "Nást.", "Kol." },
                                new [] { "","Druh", "Čislo" }};

            table.TableFormat.HorizontalAlignment = RowAlignment.Center;
            table.TableFormat.WrapTextAround = true;
            table.TableFormat.Positioning.HorizPositionAbs = HorizontalPosition.Outside;
            table.TableFormat.Positioning.VertRelationTo = VerticalRelation.Margin;
            table.TableFormat.Paddings.All = 0;
            table.ColumnWidth[4] = 300;

            for (int r = 0; r < data.Length; r++)
            {
                TableRow dataRow = table.Rows[r];

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

                    dataRow.Cells[c].CellFormat.VerticalAlignment = VerticalAlignment.Bottom;
                    dataRow.RowFormat.HorizontalAlignment = RowAlignment.Center;
                    Paragraph p2 = dataRow.Cells[c].AddParagraph();
                    TextRange tR2 = p2.AppendText(data[r][c]);
                    tR2.CharacterFormat.FontSize = 5;
                    p2.Format.HorizontalAlignment = HorizontalAlignment.Center;
                }
            }
            return table;
        }

        /// <summary>
        /// Prepísanie #Stanice na stanicu pre kotú sa robý vývseka a nastavenie času platnosti podla vybraného projektu
        /// </summary>
        private void NastavDokument()
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

