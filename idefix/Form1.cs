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
using static System.Net.WebRequestMethods;
using Newtonsoft.Json;
using log4net;
using log4net.Config;
using File = System.IO.File;

namespace idefix
{
    public partial class Form1 : Form
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(idefix.Form1));
        public Form1()
        {
            BasicConfigurator.Configure();
            InitializeComponent();
            log.Info("Entering application.");
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "burak";
            FileRead();
        }

        private void FileRead()
        {

            var files = Directory.GetFiles(textBox4.Text, "*.*", SearchOption.AllDirectories)
.Where(s => s.EndsWith(".txt") || s.EndsWith(".css") || s.EndsWith(".json") || s.EndsWith(".php") || s.EndsWith(".html"));
            foreach (var file in files)
            {
                var lines = System.IO.File.ReadAllLines(file);
                int deger = 0;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].Contains(textBox3.Text))
                    {

                        //List<string> ReccomendedString = new List<string>();
                        //ReccomendedString.Add(i.ToString());
                        if (i <= 16)
                        {
                            MessageBox.Show("istenilen degerlers bulundu");
                            string[] sub = SubArray(lines, i - i, i + i);
                            Console.WriteLine(sub);
                            JsonSerializer serializer = new JsonSerializer();
                        }
                        else
                        {
                            deger = i;
                            string[] sub = SubArray(lines, i - 16, 32);
                            Console.WriteLine(sub);
                            string json = JsonConvert.SerializeObject(sub);
                            JsonSerializer serializer = new JsonSerializer();

                            StreamWriter jsonStream = System.IO.File.CreateText(@"C:/Users/admin/Desktop/test.json");
                            JsonSerializer jsonSerializer = new JsonSerializer();
                            serializer.Serialize(jsonStream, sub);
                            jsonStream.Close();
                            System.IO.File.AppendAllText(@"C:/Users/admin/Desktop/test.json", file + i);

                        }



                    }

                }


            }

        }
        public static string[] SubArray(string[] data, int index, int length)
        {
            string[] result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox4.Text = "C:\test";
        }
    }

}

