using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
namespace IREX
{
    class email_pass_forWebsites
    {
       public string fb_email, fb_pass, OL_Email, OL_Password, Gmail_Email, Gmail_password;
        public void fbEmail()
        {
            using (StreamReader reader = new StreamReader(@"C:\IREX\setting\fb details\fb_email.txt"))
            {
                fb_email = reader.ReadLine();
            }
        }
        public void fbPass()
        {
            using (StreamReader reader = new StreamReader(@"C:\IREX\setting\fb details\fb_pass.txt"))
            {
                fb_pass = reader.ReadLine();
            }
        }
        public void outlookEmail()
        {
            using (StreamReader reader = new StreamReader(@"C:\IREX\setting\outlook details\ol_email.txt"))
            {
                OL_Email = reader.ReadLine();
            }
        }
        public void outlookPass()
        {
            using (StreamReader reader = new StreamReader(@"C:\IREX\setting\outlook details\ol_pass.txt"))
            {
                OL_Password = reader.ReadLine();
            }
        }
        public void gmailEmail()
        {
            using (StreamReader reader = new StreamReader(@"C:\IREX\setting\gmail details\gmail_email.txt"))
            {
                Gmail_Email = reader.ReadLine();
            }
        }
        public void gmailPass()
        {
            using (StreamReader reader = new StreamReader(@"C:\IREX\setting\gmail details\gmail_pass.txt"))
            {
                Gmail_password = reader.ReadLine();
            }
        }
    }
}
