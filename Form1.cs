using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel; 

namespace Translator_do_skryptu_JH
{
    public partial class Form1 : Form
    {
        
        string [] dane;
        string [,] translator;
        Excel.Application ap;
        Excel.Workbook wb;
        Excel.Worksheet ws;
        Excel.Range range;
        int rw;
        int cl;
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = "@c:\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
            ap = new Excel.Application();
            TranslatorPobranie(translator);
            
        }
        void TranslatorPobranie(string[,] tab)
        {
            int i = 0;
            wb = ap.Workbooks.Open(@"C:\Users\plpha\source\repos\Translator do skryptu JH\Translator-do-skryptu-JH\Image\translator.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            ws = (Excel.Worksheet)wb.Worksheets.get_Item(1);
            
            while (ws.Cells[i+1, 1].Value2 != "" && ws.Cells[i + 1, 1].Value2 != null)
            {
                i++;
            }
            tab = new string[i,2];
            for(int z = 0; z < tab.Length/2; z++)
            {
                tab[z ,0] = ws.Cells[z + 1, 1].Value2;
                tab[z, 1] = ws.Cells[z + 1, 2].Value2;
            }
            translator = tab;
            wb.Close(false, null, null);
            ap.Quit();
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
            this.Close();
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                checkBox2.Checked = false;
            }
        }

        private void CheckBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                checkBox1.Checked = false;
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string filestream = openFileDialog1.FileName;
                int iter = 0;
                string licz;
                File.Copy(filestream, @"..\..\bin\pomoc.txt");
                StreamReader reader = new StreamReader(@"..\..\bin\pomoc.txt");
                while((licz=reader.ReadLine())!=null)
                {
                    iter++;
                }
                iter ++;
                dane = new string[iter];
                reader.Dispose();
                reader = new StreamReader(@"..\..\bin\pomoc.txt");
               for(int z =0; z < iter; z++)
                {
                    dane[z] = reader.ReadLine();
                    
                }
                reader.Dispose();
                File.Delete(@"..\..\bin\pomoc.txt");

                string klucz = "UserMessage=";
                string s = char.ConvertFromUtf32(34);
                string pomocniczy="";
                int dlugosclini=0;
                
                if (checkBox1.Checked == true)
                {
                    for (int i = 40; i < iter-1; i++)
                    {
                        bool znalazl = false;
                        dlugosclini = dane[i].Length;
                        for(int z = 0; z < dlugosclini; z++)
                        {
                            if (z + klucz.Length > dlugosclini)
                            {
                                break;
                            }
                            else
                            {
                                for(int x =3; x < dlugosclini-12; x++)
                                {
                                    if (dane[i].Substring(x, 12) == klucz && znalazl == false)
                                    {
                                        znalazl = true;
                                        x += 13;
                                        for (int q = x; q < dlugosclini; q++)
                                        {
                                            if (dane[i].Substring(q, 1) != s)
                                            {
                                                pomocniczy += dane[i].Substring(q, 1);
                                            }
                                            else
                                            {
                                                break;
                                            }

                                        }
                                        
                                        
                                    }
                                    if (znalazl)
                                    {
                                        break;
                                    }
                                }

                            }
                            int powtorneSprawdzanie = 0;
                            if (znalazl)
                            {
                                int zasieg = 0;
                                while ((pomocniczy.ToLower()) != (translator[zasieg, 0]).ToLower())
                                {
                                    zasieg++;
                                    if (zasieg >= translator.Length / 2)
                                    {
                                        zasieg = 0;
                                        powtorneSprawdzanie++;
                                    }
                                    if (powtorneSprawdzanie > 1)
                                    {
                                        /////////tutaj ma powiedzieć że danego tłumaczenia nie ma 
                                        var meseage = MessageBox.Show("Nie ma w słowniku tłumaczenia : " + pomocniczy, "Uwaga", MessageBoxButtons.OK);
                                        break;
                                    }
                                }

                                if (powtorneSprawdzanie < 2)
                                {
                                    dane[i] = dane[i].Replace(pomocniczy, translator[zasieg, 1]);
                                    pomocniczy = "";
                                    break;
                                }
                                else
                                {
                                    pomocniczy = "";
                                    break;
                                }
                                
                            }
                        }
                    }
                    if (File.Exists(@"..\..\Przetłumaczony skrypt\script.xml"))
                    {
                        File.Delete(@"..\..\Przetłumaczony skrypt\script.xml");
                    }
                    File.Create(@"..\..\Przetłumaczony skrypt\script.xml").Close();
                    
                    StreamWriter save = new StreamWriter(@"..\..\Przetłumaczony skrypt\script.xml");
                    for(int i = 0; i < dane.Length; i++)
                    {
                        save.WriteLine(dane[i]);
                    }
                    save.Close();
                    
                }
                if (checkBox2.Checked == true)
                {
                    for (int i = 40; i < iter - 1; i++)
                    {
                        bool znalazl = false;
                        dlugosclini = dane[i].Length;
                        for (int z = 0; z < dlugosclini; z++)
                        {
                            if (z + klucz.Length > dlugosclini)
                            {
                                break;
                            }
                            else
                            {
                                for (int x = 3; x < dlugosclini - 12; x++)
                                {
                                    if (dane[i].Substring(x, 12) == klucz && znalazl == false)
                                    {
                                        znalazl = true;
                                        x += 13;
                                        for (int q = x; q < dlugosclini; q++)
                                        {
                                            if (dane[i].Substring(q, 1) != s)
                                            {
                                                pomocniczy += dane[i].Substring(q, 1);
                                            }
                                            else
                                            {
                                                break;
                                            }

                                        }


                                    }
                                    if (znalazl)
                                    {
                                        break;
                                    }
                                }

                            }
                            int powtorneSprawdzanie = 0;
                            if (znalazl)
                            {
                                int zasieg = 0;
                                while ((pomocniczy.ToLower()) != (translator[zasieg, 1]).ToLower())
                                {
                                    zasieg++;
                                    if (zasieg >= translator.Length / 2)
                                    {
                                        zasieg = 0;
                                        powtorneSprawdzanie++;
                                    }
                                    if (powtorneSprawdzanie > 1)
                                    {
                                        /////////tutaj ma powiedzieć że danego tłumaczenia nie ma 
                                        var meseage = MessageBox.Show("Nie ma w słowniku tłumaczenia : " + pomocniczy, "Uwaga", MessageBoxButtons.OK);
                                        break;
                                    }
                                }

                                if (powtorneSprawdzanie < 2)
                                {
                                    dane[i] = dane[i].Replace(pomocniczy, translator[zasieg, 0]);
                                    pomocniczy = "";
                                    break;
                                }
                                else
                                {
                                    pomocniczy = "";
                                    break;
                                }

                            }
                        }
                    }
                    if (File.Exists(@"..\..\Przetłumaczony skrypt\script.xml"))
                    {
                        File.Delete(@"..\..\Przetłumaczony skrypt\script.xml");
                    }
                    File.Create(@"..\..\Przetłumaczony skrypt\script.xml").Close();

                    StreamWriter save = new StreamWriter(@"..\..\Przetłumaczony skrypt\script.xml");
                    for (int i = 0; i < dane.Length; i++)
                    {
                        save.WriteLine(dane[i]);
                    }
                    save.Close();
                    var message = MessageBox.Show("Gotowe!!! \n  Przetłumaczony skrypt znajduje się w folderze 'Przetłumaczony scrypt' ", "Przetłumaczony skrypt", MessageBoxButtons.OK);

                }

            }
        }
    }
}
