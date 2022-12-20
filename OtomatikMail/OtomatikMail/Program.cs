using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OtomatikMail
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cs = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True";
            string sql = "select * from orders where OrderDate <= DATEADD(DAY,-1080,convert(date,sysdatetime()))";
            SqlDataAdapter sda = new SqlDataAdapter(sql,cs);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            //Customer ID , OrderDate olacak
            string mailBody = "";
            foreach (DataRow herhangi in dt.Rows)
            {
                mailBody += herhangi["OrderDate"] + " " + herhangi["CustomerID"];
            }
            MailGonder(mailBody);
        }

        private static void MailGonder(string mailBody)
        {
            MailMessage ePosta = new MailMessage();
            ePosta.From = new MailAddress("mailadresim@gmail.com"); // Kimden gidecek
            ePosta.To.Add("gonderilecekmailadresi@gmail.com");
            ePosta.Subject = "Son Siparişler";
            ePosta.Body = mailBody;

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new System.Net.NetworkCredential("mailadresim@gmail.com","mailsifrem");
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            smtp.Send(ePosta);
        }
    }
}
