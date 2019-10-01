using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;

namespace WavToCode
{
    public partial class Form1 : Form
    {
        static int BPS = 0;
        static int k = 0;
        static int s = 0;
        static int start = 0;
        static int end = 0;
        static int f, f1, f2;

        static double num = 0;

        static string directory, h, h1, h2, h3;


        public static bool Check_If_Full_Or_Empty(params object[] t)
        {
            bool b = false;
            string message = "Please fill the following boxes:\n";

            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] is TextBox)
                {
                    if (string.IsNullOrEmpty(((TextBox)t[i]).Text))
                    {
                        b = true;
                        message += "- " + ((TextBox)t[i]).Tag + "\n";
                    }
                }
                if (t[i] is ComboBox)
                {
                    if (string.IsNullOrEmpty(((ComboBox)t[i]).Text))
                    {
                        b = true;
                        message += "- " + ((ComboBox)t[i]).Tag + "\n";
                    }
                }
            }

            if (b == true)
            {
                MessageBox.Show(message, "Warning message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
            return b;
        }
        public static void Check_If_SFile_here(string str1, string str2, object t)
        {
            if (File.Exists(str1))
            {
                File.Delete(str1);
                File.Copy(str2, str1);
                t = str1;
            }
            else
            {
                File.Copy(str2, str1);
                t = str1;
            }
        }
        public static void Analyise_des()
        {
            //f = directory.LastIndexOf('\\');

            if (directory.Contains("\\OutputFiles"))
            {
                directory = directory.Replace("\\OutputFiles", "");
                f = directory.LastIndexOf('\\');
                h = directory.Insert(f, "\\OutputFiles");
            }
            else
            {
                f = directory.LastIndexOf('\\');
                h = directory.Insert(f, "\\OutputFiles");
            }

            //h = directory.Insert(f, "\\OutputFiles");
            h2 = directory.Substring(f + 1).ToString();
            f2 = h2.IndexOf('.');
            for (int i = 0; i < h2.Length; i++)
            {
                if (i <= f2)
                {
                    h3 += h2.ElementAt(i);
                }
                else
                {
                    break;
                }
            }
        }
        public static int Find_BPS_Num(string t)
        {

            string file_BPS_line = t.Substring(240, 30);

            int file_BPS = 0;

            if (file_BPS_line.Contains("8"))
            {
                file_BPS = 8;
            }
            else if (file_BPS_line.Contains("9"))
            {
                file_BPS = 9;
            }
            else if (file_BPS_line.Contains("10"))
            {
                file_BPS = 10;
            }
            else if (file_BPS_line.Contains("11"))
            {
                file_BPS = 11;
            }
            else if (file_BPS_line.Contains("12"))
            {
                file_BPS = 12;
            }

            return file_BPS;
        }
        public static int Find_Sample_Num(string t)
        {
            string file_Sample_line = t.Substring(230, 10);

            int Samples = 0;
            for (int i = 0; i < 10; i++)
            {

                if (t.Contains("0"))
                {
                    Samples *= 10;
                    Samples += 0;
                }
                else if (t.Contains("1"))
                {
                    Samples *= 10;
                    Samples += 1;
                }
                else if (t.Contains("2"))
                {
                    Samples *= 10;
                    Samples += 2;
                }
                else if (t.Contains("3"))
                {
                    Samples *= 10;
                    Samples += 3;
                }
                else if (t.Contains("4"))
                {
                    Samples *= 10;
                    Samples += 4;
                }
                else if (t.Contains("5"))
                {
                    Samples *= 10;
                    Samples += 5;
                }
                else if (t.Contains("6"))
                {
                    Samples *= 10;
                    Samples += 6;
                }
                else if (t.Contains("7"))
                {
                    Samples *= 10;
                    Samples += 7;
                }
                else if (t.Contains("8"))
                {
                    Samples *= 10;
                    Samples += 8;
                }
                else if (t.Contains("9"))
                {
                    Samples *= 10;
                    Samples += 9;
                }

            }
            return Samples;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "C files (*.C)|*.c| txt files (*.txt)|*.txt| All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 3;
            openFileDialog1.InitialDirectory = @"C:\";
            openFileDialog1.FileName = "";
            openFileDialog1.Multiselect = true;
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose your files";
            openFileDialog1.ShowDialog();
            textBox1.Text = openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
            saveFileDialog1.InitialDirectory = @"C:\";
            saveFileDialog1.Title = "Save your files";
            saveFileDialog1.RestoreDirectory = false;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.ShowDialog();
            directory = saveFileDialog1.FileName;
            if (!(directory == ""))
            {
                Analyise_des();
                textBox2.Text = h.Replace("txt", "c");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var my_file = textBox1.Text;
            var my_new_file = Path.ChangeExtension(my_file, ".txt");
            var my_new_file2 = textBox2.Text;
            var my_file2 = h;


            string[] lines = { "/*********************************************************************",
                "* Written by Hexabitz WAVToCode",
                "* Date:		" +DateTime.Now.ToString("M/d/yyyy  hh:mm:ss"),
                "* FileName:		" +h3+"c",
                "* Interleaved:		N/A",
                "* Signed:		    No",
                "* No. of channels:	1",
                "* No. of samples:	6964",
                "* Bits/Sample:		"+BPS,
                "**********************************************************************/",
                "",
                "#include \"wave.h" +"\"",
                "",
                "const uint16_t waveByteCode_"+h3.TrimEnd('.')+"[WAVEBYTECODE_"+h3.TrimEnd('.').ToUpper()+"_LENGTH] = {"
            };

            Thread workerThread = new Thread(delegate ()
            {

                Clean_des(my_new_file2, h, my_file2);

                Check_If_SFile_here(my_new_file, my_file, textBox1.Text);

                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(my_file2))
                {
                    foreach (string line in lines)
                    {
                        sw.WriteLine(line);
                    }
                    h3 = "";
                }

                string text = File.ReadAllText(my_file);
                k = Find_BPS_Num(text);
                s = Find_Sample_Num(text);

                start = text.IndexOf('{');
                end = text.IndexOf('}');

                Do_Magic(start, end, text, my_file2);

                Convert(my_file2, ".c");
                
            });

            if (!Check_If_Full_Or_Empty(textBox1, textBox2, comboBox1))
            {
                    workerThread.Start();
                
                    Cleen(my_new_file, textBox1, textBox2);
                
               
            }
        
        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.facebook.com/HexabitzInc/");
            }
            catch
            {
                string message = "Please check your WiFi connection and try again..\n";
                MessageBox.Show(message, "Warning message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.instagram.com/hexabitz_inc/");
            }
            catch
            {
                string message = "Please check your WiFi connection and try again..\n";
                MessageBox.Show(message, "Warning message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://twitter.com/HexabitzInc");
            }
            catch
            {
                string message = "Please check your WiFi connection and try again..\n";
                MessageBox.Show(message, "Warning message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://www.youtube.com/channel/UCET7EqKQIWUljX3mgNB8WSw?view_as=subscriber");
            }
            catch
            {
                string message = "Please check your WiFi connection and try again..\n";
                MessageBox.Show(message, "Warning message", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {

                BPS = 9;
            }
            else if (comboBox1.SelectedIndex == 1)
            {

                BPS = 10;
            }
            else if (comboBox1.SelectedIndex == 2)
            {

                BPS = 11;
            }
            else if (comboBox1.SelectedIndex == 3)
            {

                BPS = 12;
            }
        }

        public static double calculate(string s, int Old_BPS, int New_BPS)
        {
            
            num = int.Parse(s);
            num = num / (Math.Pow(2, Old_BPS));
            num = num * (Math.Pow(2, New_BPS));
            return num;
        }
        public static void Do_Magic(int start_indx, int end_indx, string source, string file)
        {
            string a = "";
            string b = "";
            
            
            for (int i = start_indx + 1; i < end_indx + 1; i++)
            {
                if (source.ElementAt(i).ToString() == " ")
                {
                    /* Do nothing */
                }
                else if (source.ElementAt(i).ToString() == ",")
                {
                    calculate(a, k, BPS);
                    b += num.ToString() + ", ";
                    a = "";
                    num = 0;
                }
                else if (source.ElementAt(i).ToString() == "/")
                {
                    i++;
                    while (source.ElementAt(i).ToString() != "/")
                    {
                        i++;
                    }
                    using (System.IO.StreamWriter sw2 = new System.IO.StreamWriter(file, true))
                    {
                        sw2.WriteLine(b);
                    }
                    b = "";
                }
                else if (source.ElementAt(i).ToString() == "0")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "1")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "2")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "3")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "4")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "5")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "6")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "7")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "8")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "9")
                {
                    a += source.ElementAt(i).ToString();
                }
                else if (source.ElementAt(i).ToString() == "}")
                {
                    calculate(a, k, BPS);
                    b += num.ToString() + "}";
                    a = "";
                    num = 0;
                    using (System.IO.StreamWriter sw2 = new System.IO.StreamWriter(file, true))
                    {
                        sw2.WriteLine(b);
                    }
                    b = "";
                    num = 0;

                }
                else
                {
                    /* Do nothing */
                }
            }

        }
        public static void Convert(string str1, string extension)
        {
            File.Move(str1, Path.ChangeExtension(str1, extension));
        }
        public static void Clean_des(string str, string str1, object t)
        {
            if (File.Exists(str))
            {
                File.Delete(str);
            }

            if (File.Exists(t.ToString()))
            {
                File.Delete(t.ToString());
                f1 = str1.LastIndexOf('\\');
                h1 = str1.Substring(0, f1);
            }
            else
            {
                f1 = str1.LastIndexOf('\\');
                h1 = str1.Substring(0, f1);
                Directory.CreateDirectory(h1);

            }
        }
        public static void Cleen(string str1, params object[] t)
        {
            if (File.Exists(str1))
            {
                File.Delete(str1);
            }
            for (int i = 0; i < t.Length; i++)
            {
                if (t[i] is TextBox)
                {
                    if (string.IsNullOrEmpty(((TextBox)t[i]).Text))
                    {

                    }
                    else
                    {
                        ((TextBox)t[i]).Text = "";
                    }
                }
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

    }
}
