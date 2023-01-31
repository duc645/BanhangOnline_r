using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace BanHangOnline.Common
{
    public class Common
    {


        private static string password = ConfigurationManager.AppSettings["PasswordEmail"];
        private static string Email = ConfigurationManager.AppSettings["Email"];
        //tieu de thu , tieu de noi dung, noi dung , guitoimailnao
        public static bool SendMail(string name, string subject, string content,
            string toMail)
        {
        bool rs = false;
            try
            {
                //tao moi MailMessage
                MailMessage message = new MailMessage();
                var smtp = new SmtpClient();
                {
                    smtp.Host = "smtp.gmail.com"; //host name
                    smtp.Port = 587; //port number
                    smtp.EnableSsl = true; //whether your smtp server requires SSL
                    smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;

                    smtp.UseDefaultCredentials = false;
                    //thong tin mail quan tri
                    smtp.Credentials = new NetworkCredential() { 
                        UserName=Email,
                        Password=password
                    };
                }
                //thong tin ng gui
                MailAddress fromAddress = new MailAddress(Email, name);
                message.From = fromAddress;
                //gui toi email nao "toMail"
                message.To.Add(toMail);
                //tieu de noi dung
                message.Subject = subject;
                //cho phep dung html
                message.IsBodyHtml = true;
                //noi dung
                message.Body = content;
                //gui mail bang smtp
                smtp.Send(message);
                rs = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                rs = false;
            }
            return rs;
        }
        public static string FormatNumber(object value, int SoSauDauPhay = 2)
        {
            bool isNumber = IsNumeric(value);
            decimal GT = 0;
            if (isNumber)
            {
                GT = Convert.ToDecimal(value);
            }
            string str = "";
            string thapPhan = "";
            for (int i = 0; i < SoSauDauPhay; i++)
            {
                thapPhan += "#";
            }
            if (thapPhan.Length > 0) thapPhan = "." + thapPhan;
            string snumformat = string.Format("0:#,##0{0}", thapPhan);
            str = String.Format("{" + snumformat + "}", GT);

            return str;
        }
        private static bool IsNumeric(object value)
        {
            return value is sbyte
                       || value is byte
                       || value is short
                       || value is ushort
                       || value is int
                       || value is uint
                       || value is long
                       || value is ulong
                       || value is float
                       || value is double
                       || value is decimal;
        }
    }
}