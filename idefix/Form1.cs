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

namespace idefix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
            string file_path = @"C:\test\test.txt";
            var lines = System.IO.File.ReadAllLines(file_path);
            int deger = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(textBox3.Text))
                {
                    MessageBox.Show("found");
                    deger = i;
                    string[] sub = SubArray(lines, i - 16, 30);
                    Console.Write(sub);
                    StreamWriter file = System.IO.File.CreateText(@"C:\test\test.json");
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(file, sub);
                    file.Close();
                }
            }
        }
        public static string[] SubArray(string[] data, int index, int length)
        {
            string[] result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {


        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var files = Directory.GetFiles("C:\\test\\", "*.*", SearchOption.AllDirectories)
            .Where(s => s.EndsWith(".txt") || s.EndsWith(".css") || s.EndsWith(".json") || s.EndsWith(".php") || s.EndsWith(".html"));

        }
    }
}
