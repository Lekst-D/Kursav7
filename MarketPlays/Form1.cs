using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

using System.Net.Http;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace MarketPlays
{
    public partial class Form1 : Form
    {
        string id_user = "";
        public Form1()
        {
            InitializeComponent();
            /* string connectionString = @"Data Source=C:\Users\Кирилл\Desktop\MarketPlays\Shop\db.sqlite3;Cache=Shared;Mode=ReadOnly;";
             var connection = new SqliteConnection(connectionString);
               connection.Open();
             Console.Read();
              SqliteCommand command = new SqliteCommand();
         command.Connection = connection;
           command.CommandText = "SELECT * FROM hello_user";
           SqliteDataReader reader = command.ExecuteReader();
           var id = reader.GetValue(0);

           textBox1.Text = ""+id;*/
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string log = textBox1.Text;
                string pas = textBox2.Text;

                string json = null;
                ASCIIEncoding encoding = new ASCIIEncoding();
                //string postData = "Login=" + "User" + "&Password=" + "123";
                string postData = "Login=" + log + "&Password=" + pas;
                byte[] data = encoding.GetBytes(postData);
                WebRequest request = WebRequest.Create("http://127.0.0.1:8000/Login_destop");
                request.Method = "POST";
                request.Timeout = 1000;
                request.Headers.Add("X-CSRF-TOKEN", "X-CSRF-TOKEN");

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream stream1 = request.GetRequestStream();
                stream1.Write(data, 0, data.Length);
                stream1.Close();

                WebResponse response = request.GetResponse();
                stream1 = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream1);
                UserID id = JsonConvert.DeserializeObject<UserID>(sr.ReadToEnd());

                response.Close();

                id_user = id.id;

                textBox30.Text = id.username;
                textBox31.Text = id.email;
                textBox8.Text = id.wallet;

                panel23.Controls.Clear();




                json = null;
                encoding = new ASCIIEncoding();
                postData = "id_user=" + id.id;
                data = encoding.GetBytes(postData);
                request = WebRequest.Create("http://127.0.0.1:8000/UserGames_destop");
                request.Method = "POST";
                request.Timeout = 1000;
                request.Headers.Add("X-CSRF-TOKEN", "X-CSRF-TOKEN");

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                stream1 = request.GetRequestStream();
                stream1.Write(data, 0, data.Length);
                stream1.Close();

                response = request.GetResponse();
                stream1 = response.GetResponseStream();

                sr = new StreamReader(stream1);
                Game[] gamesUser = JsonConvert.DeserializeObject<Game[]>(sr.ReadToEnd());

                response.Close();
                int x = 45;
                int y = 60;
                foreach (Game gameuser in gamesUser)
                {
                    Button but = new Button();
                    Button but1 = new Button();
                    PictureBox pic = new PictureBox();
                    TextBox text1 = new TextBox();
                    int newSize = 6;

                    pic.Size = new Size(109, 109);
                    pic.Location = new Point(x, y);
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Load(@"http://127.0.0.1:8000/" + gameuser.cover);

                    text1.Size = new Size(109, 26);
                    text1.Location = new Point(x, y + 90);
                    text1.Text = gameuser.name;

                    but.Size = new Size(55, 26);
                    but.Font = new Font(but.Font.FontFamily, newSize);
                    but.Location = new Point(x, y + 115);
                    but.Text = "Страница";
                    but.Click += new EventHandler(delegate (Object o, EventArgs a)
                    {
                        GameShow(gameuser.id);
                    });

                    but1.Size = new Size(55, 26);
                    but1.Font = new Font(but.Font.FontFamily, newSize);
                    but1.Location = new Point(x + 55, y + 115);
                    but1.Text = "Скачать";

                    panel23.Controls.Add(text1);
                    panel23.Controls.Add(pic);
                    panel23.Controls.Add(but);
                    panel23.Controls.Add(but1);

                    x += +109 + 15;

                    if (x > 293)
                    {
                        x = 45;
                        y += 175 + 10;
                    }
                }

                tabControl1.SelectedTab = tabPage4;
                textBox1.Text = "";
                textBox2.Text = "";
            }
            catch 
            {
                MessageBox.Show(
                "Вы вели не правильно либо логин, либо пароль",
                "Сообщение",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            finally
            {
                
            }
        }

        void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button17_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage1;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedTab = tabPage3;
        }


        private void GameShow(string id)
        {
            ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = "Login=" + "User" + "&Password=" + "123";
            string postData = "id_game=" + id;
            byte[] data = encoding.GetBytes(postData);
            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/Game_destop");
            request.Method = "POST";
            request.Timeout = 1000;
            request.Headers.Add("X-CSRF-TOKEN", "X-CSRF-TOKEN");

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream1 = request.GetRequestStream();
            stream1.Write(data, 0, data.Length);
            stream1.Close();

            WebResponse response = request.GetResponse();
            stream1 = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream1);
            Game game = JsonConvert.DeserializeObject<Game>(sr.ReadToEnd());
            response.Close();

            pictureBox7.Load(@"http://127.0.0.1:8000/" + game.cover);

            label38.Text = "Название игры: "+game.name;
            label39.Text = "Дата выхода: " + game.date;
            label40.Text = "Создатель: " + game.creater;
            textBox23.Text = "Описание: "+game.description;
            textBox24.Text =game.price;

            tabControl1.SelectedTab = tabPage7;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/Games_destop");
            WebResponse response = request.GetResponse();
            string line = "";
            string st = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        Game[] games = JsonConvert.DeserializeObject<Game[]>(line);
                     
                        int y= 35;
                        foreach (Game id in games) {
                            PictureBox picture = new PictureBox();
                            TextBox text = new TextBox();
                            TextBox text1 = new TextBox();
                            TextBox text2 = new TextBox();

                            picture.Size = new Size(65, 75);
                            picture.Location = new Point(15, y);
                            picture.SizeMode = PictureBoxSizeMode.StretchImage;
                            picture.Load(@"http://127.0.0.1:8000/" + id.cover);
                            picture.Click += new EventHandler(delegate (Object o, EventArgs a)
                            {
                                GameShow(id.id);
                                
                            });

                            text.Size = new Size(109, 109);
                            text.Location = new Point(135, y + 28);
                            text.Text = id.name;
                            text.Click += new EventHandler(delegate (Object o, EventArgs a)
                            {
                                GameShow(id.id);
                            });

                            text1.Size = new Size(109, 109);
                            text1.Location = new Point(320, y + 28);
                            text1.Text = id.date;
                            text1.Click += new EventHandler(delegate (Object o, EventArgs a)
                            {
                                GameShow(id.id);
                            });

                            text2.Size = new Size(109, 109);
                            text2.Location = new Point(515, y + 28);
                            text2.Text = id.price;
                            text2.Click += new EventHandler(delegate (Object o, EventArgs a)
                            {
                                GameShow(id.id);
                            });


                            y += picture.Width + 20;

                            panel3.Controls.Add(picture);
                            panel3.Controls.Add(text);
                            panel3.Controls.Add(text1);
                            panel3.Controls.Add(text2); 
                        }

                    }
                }
            }
            Console.WriteLine("sdf " + st);
            response.Close();
            //Console.WriteLine("Запрос выполнен");
            Console.Read();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/login_zap/");
            WebResponse response = request.GetResponse();
            string line = "";
            string st = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        textBox1.Text = line;
                    }
                }
            }
            Console.WriteLine("sdf " + st);
            response.Close();
            //Console.WriteLine("Запрос выполнен");
            Console.Read();










            /*
            var client = new HttpClient();
            
            var result = client.GetAsync("http://127.0.0.1:8000/login_zap/");
            textBox1.Text=Convert.ToString(result.Result);
            

            
            string password, login;
            password = "RYE";
            login = "Nic";
            ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = "log=" + "User12345" + "&pas=" + "1234567890";
            string postData = "Login=" + login + "&Password=" + password;
            byte[] data = encoding.GetBytes(postData);

            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/login_zap/");
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.UseDefaultCredentials = true;
            request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;

            

            Stream stream1 = request.GetRequestStream();
            stream1.Write(data, 0, data.Length);
            stream1.Close();

            WebResponse response = request.GetResponse();
            stream1 = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream1);

            string json = sr.ReadToEnd();
            sr.Close();
            stream1.Close();

            textBox1.Text = json;
            */
        }

        private void button9_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/testGetZap?id=8");
            WebResponse response = request.GetResponse();
            string line = "";
            string st = "";
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {

                    while ((line = reader.ReadLine()) != null)
                    {
                        textBox1.Text = line;
                    }
                }
            }
            Console.WriteLine("sdf " + st);
            response.Close();
            //Console.WriteLine("Запрос выполнен");
            Console.Read();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            string json = null;
            ASCIIEncoding encoding = new ASCIIEncoding();
            //string postData = "log=" + "User12345" + "&pas=" + "1234567890";
            string postData = "id=" + "9";
            byte[] data = encoding.GetBytes(postData);
            WebRequest request = WebRequest.Create("http://127.0.0.1:8000/testGetZap1");
            request.Method = "POST";
            request.Timeout = 1000;
            request.Headers.Add("X-CSRF-TOKEN", "X-CSRF-TOKEN");

            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            Stream stream1 = request.GetRequestStream();
            stream1.Write(data, 0, data.Length);
            stream1.Close();

            WebResponse response = request.GetResponse();
            stream1 = response.GetResponseStream();

            StreamReader sr = new StreamReader(stream1);
            json = sr.ReadToEnd();
            response.Close();
            textBox2.Text = json;
        }

        private void button28_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nic = textBox9.Text;
            string mail = textBox4.Text;
            string pass = textBox3.Text;
            string pass_dub = textBox5.Text;

            if (nic == "") 
            {
                MessageBox.Show(
                "Вы не вели ник",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            else if (mail == "") 
            {
                MessageBox.Show(
                "Вы не вели почту",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            else if (pass == "") 
            {
                MessageBox.Show(
                "Вы не вели пароль",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            else if (pass_dub == "") 
            {
                MessageBox.Show(
                "Вы не вели павтор пароля",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            else if (pass != pass_dub) 
            {
                MessageBox.Show(
                "Пароль и повтор пароля не равны",
                "Ошибка",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information,
                MessageBoxDefaultButton.Button1,
                MessageBoxOptions.DefaultDesktopOnly);
            }
            else
            {
                string json = null;
                ASCIIEncoding encoding = new ASCIIEncoding();
                //string postData = "log=" + "User12345" + "&pas=" + "1234567890";
                string postData = "Nic=" + nic + "&Mail=" + mail + "&Password=" + pass;
                byte[] data = encoding.GetBytes(postData);
                WebRequest request = WebRequest.Create("http://127.0.0.1:8000/Registration_destop");
                request.Method = "POST";
                request.Timeout = 1000;
                request.Headers.Add("X-CSRF-TOKEN", "X-CSRF-TOKEN");

                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = data.Length;

                Stream stream1 = request.GetRequestStream();
                stream1.Write(data, 0, data.Length);
                stream1.Close();

                WebResponse response = request.GetResponse();
                stream1 = response.GetResponseStream();

                StreamReader sr = new StreamReader(stream1);
                json = sr.ReadToEnd();

                UserID user_reg = JsonConvert.DeserializeObject<UserID>(json);

                response.Close();
                
            

                string usid = user_reg.id;

                response.Close();
                
                if (usid == "Yes" )
                {
                    MessageBox.Show(
                      "пользователь был зарегистрирован",
                      "Регистрация",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);

                    tabControl1.SelectedTab = tabPage1;
                }
                else 
                {
                    MessageBox.Show(
                      "пользователь не был зарегистрирован",
                      "Регистрация",
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Information,
                      MessageBoxDefaultButton.Button1,
                      MessageBoxOptions.DefaultDesktopOnly);
                }

            }
        }
    }

    public class UserID
    {
        public string id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string wallet { get; set; }

    }
    public class Game
    {
        
        public string creater { get; set; }
        public string description { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string cover { get; set; }
        public string date { get; set; }
        public string price { get; set; }

    }
}
