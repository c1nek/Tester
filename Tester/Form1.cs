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

namespace Tester
{
    public partial class Form1 : Form
    {
        String ciag;
        public Form1()
        {
            InitializeComponent();
            debug.AppendText("[" + DateTime.Now + "] " + "Start programu.\n");
            debug.AppendText("[" + DateTime.Now + "] " + "Miłego testowania :)\n");
        }

        private byte[] przepiszNaBity(String tekst)
        {
            debug.AppendText("[" + DateTime.Now + "] " + "Przepisanie stringa na tablice bitów...\n");
            char[] x;
            x = tekst.ToCharArray();
            byte[] tab = new byte[x.Length];
            for (int oooU = 0; oooU < x.Length; oooU++)
                tab[oooU] = (byte)(x[oooU] - 48);

            debug.AppendText("[" + DateTime.Now + "] " + "Przepisano!\n");

            return tab;
        }

        private void testPokerowy(byte[] ciag, ProgressBar progress)
        {
            int[,] tab = new int[,] { 
            { 0000, 0 }, 
            { 0001, 0 }, 
            { 0010, 0 }, 
            { 0011, 0 },
            { 0100, 0 }, 
            { 0101, 0 },
            { 0110, 0 },
            { 0111, 0 }, 
            { 1000, 0 }, 
            { 1001, 0 }, 
            { 1010, 0 },
            { 1011, 0 }, 
            { 1100, 0 },
            { 1101, 0 },
            { 1110, 0 },
            { 1111, 0 },};

            progress.Value = 0;
            progress.Maximum = ciag.Length;
            int temp = 0;
            String xaxa;
            double x=0, temp1=0, X=0;

            for (int i = 0; i < ciag.Length; i=i+4)
            {
                xaxa = Convert.ToString(Convert.ToString(ciag[i]) + Convert.ToString(ciag[i + 1]) + Convert.ToString(ciag[i + 2]) + Convert.ToString(ciag[i + 3]));
                temp = Convert.ToInt32(xaxa);
                progress.Value += 4;
                for (int j = 0; j < 16; j++)
                {
                    if (temp == tab[j, 0])
                    {
                        tab[j, 1]++;
                        break;
                    }

                }
   
            }
            for (int k = 0; k < 16; k++)
            {
                temp1 = (double)tab[k, 1] * (double)tab[k, 1];
                x += temp1;
            }
            X = (double)16 / ((double)ciag.Length / 4) * x - 5000;
            debug.AppendText("[" + DateTime.Now + "] " + "Wystąpienia\n");
            debug.AppendText("___________________________________________\n");
            for (int i = 0; i < 16; i++)
            {
                debug.AppendText(tab[i,0]+ "\t"+ tab[i,1] + "\n");
            }
            debug.AppendText("___________________________________________\n");

            if (X > 2.16 && X<46.17)
            {
                pictureBox1.Image = Tester.Properties.Resources.tick;
                debug.AppendText("[" + DateTime.Now + "] " + "Test zaliczony!\n");
                debug.AppendText("[" + DateTime.Now + "] " + "X= " + X + "\n");
            }
            else
            {
                pictureBox1.Image = Tester.Properties.Resources.BlotterWarningRed;
                debug.AppendText("[" + DateTime.Now + "] " + "Test niezaliczony!\n");
                debug.AppendText("[" + DateTime.Now + "] " + "X nie mieści sie w zaresie! "+ X +"\n");
            }

            
        }

        private void testPojedynczychBitow(byte[] ciag, ProgressBar progress)
        {
            int x0 = 0, x1 = 0;
            double s;
            for (int i = 0; i < ciag.Length - 1; i++)
            {
                if (ciag[i] == 0)
                {
                    x0++;
                }
                else
                {
                    x1++;
                }
            }
            if (ciag[ciag.Length - 1] == 0)
            {
                x0++;
            }
            else
            {
                x1++;
            }


            s = (double)(Math.Pow(x0 - x1, 2)) / (double)ciag.Length;

            debug.AppendText("[" + DateTime.Now + "] " + "Rozkład wynosi: " + s + "\n");
            label4.Text = "S= " + s;
        }

       
        private void testDlugiejSerii(byte[] ciag, int dlugosc, ProgressBar progress)
        {
            debug.AppendText("[" + DateTime.Now + "] " + "Wskazana maksymalna długość serii: " + dlugosc +" \n");
            int seriaMax = 1;
            int seria = 1;
            progress.Value = 0;
            progress.Maximum = ciag.Length-2;
            for (int i = 0; i < ciag.Length-1; i++)
            {
                if (ciag[i] == ciag[i + 1])
                {
                    seria++;
                }
                else 
                    if (seria > seriaMax)
                {
                    seriaMax = seria;
                    seria = 1;
                }
                progress.Value = i;
                progress.Update();
            }
            if (seriaMax < dlugosc)
            {
                pictureBox3.Image = Tester.Properties.Resources.tick;
                debug.AppendText("[" + DateTime.Now + "] " + "Test zaliczony!\n");
                debug.AppendText("[" + DateTime.Now + "] " + "Maksymalna długość serii: " + seriaMax + "\n");
            }
            else
            {
                pictureBox3.Image = Tester.Properties.Resources.BlotterWarningRed;
                debug.AppendText("[" + DateTime.Now + "] " + "Test niezaliczony!\n");
                debug.AppendText("[" + DateTime.Now + "] " + "Maksymalna długość serii: " + seriaMax + "\n");
            }

        }

        private void testParBitów(byte[] ciag)
        {
            int x0 = 0, x1 = 0, x00 = 0, x01 = 0, x10 = 0, x11 = 0;
            double s;
            int xx = 0;

            for (int i = 0; i < ciag.Length-1; i++)
            {
                if (ciag[i] == 0)
                {
                    x0++;
                }
                else
                {
                    x1++;
                }
                if (ciag[i] == 0 && ciag[i + 1] == 0)
                {
                    x00++;
                }
                else if (ciag[i] == 0 && ciag[i + 1] == 1)
                {
                    x01++;
                }
                else if (ciag[i] == 1 && ciag[i + 1] == 0)
                {
                    x10++;
                }
                else if (ciag[i] == 1 && ciag[i + 1] == 1)
                {
                    x11++;
                }
                xx = i;
            }
            if (ciag[ciag.Length-1] == 0)
            {
                x0++;
            }
            else
            {
                x1++;
            }

            double a = 0, b = 0, c = 0, d = 0;
            a = (double)4 / (double)(xx+1);
            b = x00 * x00 + x01 * x01 + x10 * x10 + x11 * x11;
            c = (double)2 / (double)(xx + 2);
            d = (x0 * x0 + x1 * x1);
            s = ((a * b) - (c * d)) + 1;


            debug.AppendText("[" + DateTime.Now + "] " + "Rozkład wynosi: " + s + "\n");
            label3.Text = "S= " + s;

            
        }
        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tester ciągów \nMarcin Gluza \n2014 \n\ngumball300@gmail.com", "Autor");
        }

        private void zakończToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {   
            Stream myStream = null;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    debug.AppendText("[" + DateTime.Now + "] " + "Wczytano dane z pliku: " + openFileDialog1.FileName +"\n");
                    if ((myStream = openFileDialog1.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            ciag = File.ReadAllText(openFileDialog1.FileName);

                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Błąd. Nie można odczytać wskazenego pliku! \n " + ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            debug.AppendText("[" + DateTime.Now + "] " + "Uruchamiam testy! \n");
            byte[] kod = przepiszNaBity(ciag);
            debug.AppendText("[" + DateTime.Now + "] " + "Długość ciągu to: " + kod.Length + "\n");
  
            if (checkBox1.Checked == true)
            {
                debug.AppendText("[" + DateTime.Now + "] " + "Test pokerowy.\n");
                testPokerowy(kod, progressBar1);
            }
            if (checkBox2.Checked == true)
            {
                debug.AppendText("[" + DateTime.Now + "] " + "Test pojedyńczych bitów.\n");
                testPojedynczychBitow(kod, progressBar1);
            }
            if (checkBox3.Checked == true)
            {
                debug.AppendText("[" + DateTime.Now + "] " + "Test długiej serii\n");
                testDlugiejSerii(kod, Convert.ToInt16(textBox2.Text), progressBar3);
            }
            if (checkBox4.Checked == true)
            {
                debug.AppendText("[" + DateTime.Now + "] " + "Test par bitów\n");
                testParBitów(kod);

            }

                
        }
    }
}
