﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace idefix
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void JsonWriter(JObject JsonString)
        {
            
            using (StreamWriter file = File.CreateText(@"C:\test\test.json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                JsonString.WriteTo(writer);
            }
        }

        private void TextBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            textBox3.Text = "burak";
            DosyaOku();
        }

        private void DosyaOku()
        {
            string dosya_yolu = @"C:\test\test.txt";
            FileStream fs = new FileStream(dosya_yolu, FileMode.Open, FileAccess.Read);
            StreamReader sw = new StreamReader(fs);

            string yazi = sw.ReadLine();
            while (yazi != null)
            {
                if (yazi.Contains(textBox3.Text))
                {
                    MessageBox.Show("found" + textBox3.Text);
                }
                yazi = sw.ReadLine();
            }
            sw.Close();
            fs.Close();
        }

        private void TextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
