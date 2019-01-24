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
using System.Collections;
using Newtonsoft.Json.Linq;

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

        private void Button1_Click(object sender, EventArgs e)
        {
            JsonInformationClass json = new JsonInformationClass();
            FileRead(json);

        }
        IEnumerable<string> SearchAccessibleFiles(string root, string searchTerm)
        {
            var files = new List<string>();

            foreach (var file in Directory.GetFiles(root))
            {
                //    string extension = Path.GetExtension(file);
                files.Add(root);
            }
            foreach (var subDir in Directory.EnumerateDirectories(root))
            {
                try
                {
                    files.AddRange(SearchAccessibleFiles(subDir, searchTerm));
                }
                catch (UnauthorizedAccessException ex)
                {
                    Console.WriteLine("ex");
                }
            }
            return files.Distinct().ToList();
        }
        public JsonInformationClass JsonSerilaize(string[] subLower, string[] SubUpper, string SearchTerm, int SearchLine, string path, bool IsSucces)
        {
            JsonInformationClass jsonObj = new JsonInformationClass();
            jsonObj.SubLowerr = subLower;
            jsonObj.SubUpper = SubUpper;
            jsonObj.SearchLine = SearchLine;
            jsonObj.SearchTerm = SearchTerm;
            jsonObj.Path = path;
            jsonObj.IsSucces = IsSucces;
            return jsonObj;
        }
        private void FileRead(object Json)
        {
            List<JsonInformationClass> shapes = new List<JsonInformationClass>();
            SearchAccessibleFiles(textBox4.Text, textBox3.Text);
            var files = Directory.GetFiles(textBox4.Text, "*.*", SearchOption.AllDirectories)
           .Where(s => s.EndsWith(".txt") || s.EndsWith(".css") || s.EndsWith(".json") || s.EndsWith(".php") || s.EndsWith(".html"));
            foreach (var file in files)
            {

                SearchAccessibleFiles(textBox4.Text, textBox3.Text);
                var lines = System.IO.File.ReadAllLines(file);
                int deger = 0;

                for (int i = 0; i < lines.Length; i++)
                {

                    if (lines[i].Contains(textBox3.Text))
                    {

                        if (i <= 16)
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            MessageBox.Show("istenilen degerler bulundu");
                            string[] subLower = null;

                            string[] subUpper = SubArray(lines, 0, i + 1);

                            //StreamWriter jsonStream = System.IO.File.CreateText(path);
                            //JsonSerializer jsonSerializer = new JsonSerializer();
                            //serializer.Serialize(jsonStream, sub);
                            //jsonStream.Close();
                            //        System.IO.File.AppendAllText(path, file + "/n Istenilen Deger" + i + textBox3.Text);
                            bool IsSucces = true;
                            Json = new JsonInformationClass();
                            string root = Path.GetFileName(file);
                            string root2 = Path.GetPathRoot(file);
                            string pathFiles = root2 + root;
                            JsonInformationClass JsonData = JsonSerilaize(subLower, subUpper, textBox3.Text, i, pathFiles, IsSucces);
                       
                            shapes.Add(JsonData);

                        }
                        else
                        {
                            deger = i;

                            string[] subLower = SubArray(lines, i - 15, 15);
                            int a = lines.Length - i;
                            if (a < 15)
                            {
                                bool IsSucces = true;
                                string[] subUpper2 = SubArray(lines, i, lines.Length - i);
                                string root = Path.GetFileName(file);
                                string root2 = Path.GetPathRoot(file);
                                string pathFiles = root2 + root;
                                JsonInformationClass JsonData2 = JsonSerilaize(subLower, subUpper2, textBox3.Text, i, pathFiles, IsSucces);
                               
                                shapes.Add(JsonData2);
                            }
                            else
                            {
                                string[] subUpper = SubArray(lines, i, 15);
                                string[] subLower2 = SubArray(lines, i - 15, 15);
                                bool IsSucces = true;
                                string root = Path.GetFileName(file);
                                string root2 = Path.GetPathRoot(file);
                                string pathFiles = root2 + root;
                                JsonInformationClass JsonData = JsonSerilaize(subLower2, subUpper, textBox3.Text, i, root, IsSucces);
                               
                                shapes.Add(JsonData);
                            }
                            //    Console.WriteLine(sub);
                            //    string json = JsonConvert.SerializeObject(sub);
                            //     JsonSerializer serializer = new JsonSerializer();
                            //    StreamWriter jsonStream = System.IO.File.CreateText(@"C:/Users/admin/Desktop/test.json");
                            //    System.IO.File.AppendAllText(@"C:/Users/admin/Desktop/test.json", file + i + textBox3.Text);
                            //   object a =   JsonSerilaize(i-16, i+16, textBox3.Text, i, path, true);
                            //    string JsonData = JsonConvert.SerializeObject(a);
                            //   System.IO.File.WriteAllText(path, JsonData);
                        }
                    }
                }
            }
            string path = @"C:\Users\admin\Desktop/test.json";
            string shapesData = JsonConvert.SerializeObject(shapes);
            System.IO.File.WriteAllText(path, shapesData);
        }
        public static string[] SubArray(string[] data, int index, int length)
        {
            string[] result = new string[length];
            Array.Copy(data, index, result, 0, length);
            return result;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox4.Text = @"C:\test";
            textBox3.Text = @"burak";
        }
    }
}