using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace laba5
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string token = null;

            
            using (AuthForm authForm = new AuthForm())
            {
                if (authForm.ShowDialog() == DialogResult.Yes)
                {
                    token = authForm.Token;
                }

                if (string.IsNullOrEmpty(token))
                {
                    MessageBox.Show("Ошибка авторизации", "Ошибка",
                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                WebClient wc = new WebClient() { Encoding = Encoding.UTF8 };

                int myId = 136180129;

                //var q = wc.DownloadString($"https://api.vk.com/method/users.get?user_id={myId}&fields=education,universities&access_token={token}&v=5.131");

                Random rand = new Random();
                int id1 = (textBox1.Text
                    .Where(c => char.IsDigit(c))
                    .Aggregate(0, (s, a) => s * 10 + (int)char.GetNumericValue(a)));
                int id2 = (textBox2.Text
                    .Where(c => char.IsDigit(c))
                    .Aggregate(0, (s, a) => s * 10 + (int)char.GetNumericValue(a)));

                for (int id = id1; id < id2; id++)
                {

                    var r = wc.DownloadString($"https://api.vk.com/method/users.get?user_id={id}&fields=education,universities&access_token={token}&v=5.131");

                    //MessageBox.Show(r);

                    var maskToString = JObject.Parse(r).ToString();
                    var mask = JObject.Parse(r);
                   

                    if (maskToString.Contains("university_name")&& maskToString.Contains("first_name"))
                    {
                        string univerName = mask["response"].Select(jt => (string)jt["university_name"]).FirstOrDefault();
                        if (univerName != "")
                        {
                            string firstName = mask["response"].Select(jt => (string)jt["first_name"]).FirstOrDefault();
                            string lastName = mask["response"].Select(jt => (string)jt["last_name"]).FirstOrDefault();
                            textBox.Text += $"Пользователь с id:{id} {firstName} {lastName} учится в {univerName}"+ Environment.NewLine;

                        }
                        
                    }


                }
            }

        }

    }
}