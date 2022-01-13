using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace laba5
{
    public partial class AuthForm : Form
    {
        string _clientId = "8007606";
        string _autorizeUri = "https://oauth.vk.com/authorize";
        string _redirectUri = "https://oauth.vk.com/blank.html";

        public string Token { get; private set; }
        public AuthForm()
        {
            InitializeComponent();
        }

        private void AuthForm_Shown(object sender, EventArgs e)
        {
            webBrowser.Navigate(new Uri($"https://oauth.vk.com/authorize?client_id={_clientId}&display=page&redirect_uri={_redirectUri}&response_type=token&v=5.131"));
        }


        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            string uri = e.Url.ToString();
            
            if (uri.StartsWith(_autorizeUri)) return;

            if (!uri.StartsWith(_redirectUri))
            {
                DialogResult = DialogResult.No;
                return;
            }
           

            var parameters = (from param in uri.Split('#')[1].Split('&')
                              let parts = param.Split('=')
                              select new
                              {
                                  Name = parts[0],
                                  Value = parts[1]
                              }
                             ).ToDictionary(v => v.Name, v => v.Value);

            Token = parameters["access_token"];
            //MessageBox.Show(Token);
            DialogResult = DialogResult.Yes;

        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
