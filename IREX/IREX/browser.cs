using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
namespace IREX
{ 
    public partial class browser : Form
    {
        //----------------------Browser Coding----------------------
        //----------------------Start here--------------------------
        voice_system vsy;
        public browser(voice_system f)
        {
            InitializeComponent();
            webBrowser1.Navigate("www.google.com");
            webBrowser1.ScriptErrorsSuppressed = true;
            vsy = f;
        }
        public void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            textBox1.Text = webBrowser1.Url.ToString();
        }

        public void tab_Click(object sender, EventArgs e)
        {
            TabPage Tab = new TabPage();
            Tab.Text = "New Tab";
            tabControl1.Controls.Add(Tab);
            WebBrowser webbrowser = new WebBrowser();
            webbrowser.Parent = Tab;
            webbrowser.Navigate("www.google.com");
            webbrowser.Dock = DockStyle.Fill;

        }
        private void browser_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
        public void go_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate(textBox1.Text);
        }

        public void back_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }
        public void forward_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }
        public void refresh_Click(object sender, EventArgs e)
        {
            webBrowser1.Refresh();
        }
        public void home_Click(object sender, EventArgs e)
        {
            this.textBox1.Text = "www.google.com";
            webBrowser1.Navigate(textBox1.Text);
        }
        //===========================Coding for IREX input and output by voice========================================
        //=========================================Starte here========================================================
        //============================================================================================================
        public void sear_ching()
        {
            webBrowser1.Navigate(textBox1.Text);
        }
        public void go_back()
        {
            webBrowser1.GoBack();
        }
        public void go_forward()
        {
            webBrowser1.GoForward();

        }
        public void refershing()
        {
            webBrowser1.Refresh();
        }
        public void googleMap()
        {
            webBrowser1.Navigate("https://www.google.com/maps/@37.0625,-95.677068,4z");
        }
        public void go_home()
        {
            this.textBox1.Text = "www.google.com";
            webBrowser1.Navigate(textBox1.Text);
        }
        public void facebook()
        {
            webBrowser1.Navigate("www.FaceBook.com");
        }
        public void login_facebook()
        {
            email_pass_forWebsites fbObj = new email_pass_forWebsites();
            fbObj.fbEmail();
            fbObj.fbPass();
            HtmlElement ele = webBrowser1.Document.GetElementById("email");
            if (ele != null)
                ele.InnerText = fbObj.fb_email;

            ele = webBrowser1.Document.GetElementById("pass");
            if (ele != null)
                ele.InnerText = fbObj.fb_pass;
            SendKeys.Send("~");
        }
        public void outlook()
        {
            webBrowser1.Navigate("https://login.live.com/?wa=wsignin1.0&rpsnv=12&ct=1455120243&rver=6.4.6456.0&wp=MBI_SSL_SHARED&wreply=https%3a%2f%2fmail.live.com%2fm%2f%3ffl%3d635907170436505842%2c%252fm%252f&lc=1033&id=64855&mspco=1&pcexp=false");
        }
        public void newtab()
        {
            TabPage Tab = new TabPage();
            Tab.Text = "New Tab";
            tabControl1.Controls.Add(Tab);
            WebBrowser webbrowser = new WebBrowser();
            webbrowser.Parent = Tab;
            webbrowser.Navigate("www.google.com");
            webbrowser.Dock = DockStyle.Fill;
        }
        public void login_Outlook()
        {
            email_pass_forWebsites outlookObj = new email_pass_forWebsites();
            outlookObj.outlookEmail();
            outlookObj.outlookPass();
            HtmlElement ele = webBrowser1.Document.GetElementById("i0116");
            if (ele != null)
                ele.InnerText = outlookObj.OL_Email;

            ele = webBrowser1.Document.GetElementById("i0118");
            if (ele != null)
                ele.InnerText = outlookObj.OL_Password;

            SendKeys.Send("~");
        }
        public void gMail()
        {
            webBrowser1.Navigate("https://mail.google.com/");
        }
        public void gmail_Login()
        {
            email_pass_forWebsites gmailObj = new email_pass_forWebsites();
            gmailObj.gmailEmail();
            gmailObj.gmailPass();
            HtmlElement ele = webBrowser1.Document.GetElementById("Email");
            if (ele != null)
                ele.InnerText = gmailObj.Gmail_Email; ;

            ele = webBrowser1.Document.GetElementById("Passwd");
            if (ele != null)
                ele.InnerText = gmailObj.Gmail_password;
            SendKeys.Send("~");
        }
        public void portal()
            {
            webBrowser1.Navigate("http://mcom.pgc.edu/Student/StdLogin.jsp");
            }
        public void Quran_Pak()
        {
            webBrowser1.Navigate("http://www.flashquran.com/en/");
        }
        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            webBrowser1.Navigate(new Uri(textBox1.Text));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
