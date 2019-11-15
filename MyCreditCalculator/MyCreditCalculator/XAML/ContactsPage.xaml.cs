using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyCreditCalculator.XAML
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ContactsPage : ContentPage
	{
		public ContactsPage ()
		{
			InitializeComponent ();
		}

        private async void OnContactCloseClicked(object _sender, EventArgs e)
        {
            await  Navigation.PushModalAsync(new MainPage());
            
        }
        /// <summary>
        /// send email to developer
        /// </summary>
        /// <param name="_sender"></param>
        /// <param name="e"></param>
        private void OnSendMailClicked(object _sender, EventArgs e)
        {
            string emailPattern = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";
            string userEMail = emailEditor.Text;
            string myEMail = "agstudiodev@gmail.com";
            string userName = nameEditor.Text;
            string message = messageEditor.Text;
            var emailMessenger = CrossMessaging.Current.EmailMessenger;
            if (userEMail == null || userName == null || message == null)
                textError.Text = "Заполните, пожалуста, все поля";
            else if (!Regex.IsMatch(userEMail, emailPattern))
            {
                emailEditor.Text = null;
                textError.Text = "Проверьте корректность ввода email";
            }
            else
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    mail.From = new MailAddress(userEMail);
                    mail.To.Add(myEMail);
                    mail.Subject = userName+" "+"<"+userEMail+">";
                    mail.Body = message;
                    mail.Priority = MailPriority.High;

                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new NetworkCredential(myEMail, "***********");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.UseDefaultCredentials = false;
                    ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                    {
                        return true;
                    };
                    SmtpServer.Send(mail);
                    DisplayAlert("", "Спасибо, " + userName + "!\nВаше письмо отправлено", "ОК");
                    messageEditor.Text = null;

                }
                catch (Exception)
                {
                    DisplayAlert("Сбой отправки", "Сбой отправки, попробуйте еще раз", "ОК");
                }
               
            }
        }
        /// <summary>
        /// send mail to developer by link in text
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MailToDev(object sender, EventArgs e)
        {
            string myEMail = "agstudiodev@gmail.com";
            var emailMessenger = CrossMessaging.Current.EmailMessenger;

            if (emailMessenger.CanSendEmail)
                emailMessenger.SendEmail(myEMail, "Калькулятор кредитов", "");
        }
    }
}
