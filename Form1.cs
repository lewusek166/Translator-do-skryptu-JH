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
using System.Xml;

namespace Translator_do_skryptu_JH
{
    public partial class Form1 : Form
    {
        string [] dane;
        
        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = "@c:\\";
            openFileDialog1.Filter = "xml files (*.xml)|*.xml";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.FileName = "";
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

                string klucz = "UserMessage";
                if (checkBox1.Checked == true)
                {
                    string search = "UserMessage="+'"';
                    int startindex = dane[40].IndexOf(search);
                    int endindex = dane[40].IndexOf('"');
                    string sub = dane[40].Substring(startindex, endindex);
                    int z = 0;

                }
                if (checkBox2.Checked == true)
                {



                }

            }
        }
    }
}
