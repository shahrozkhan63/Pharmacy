using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections;

namespace RegistrationAndLogin.Models
{
    public class Emailing
    {
        public string SendEmail(EmailContent ec)
        {
            try
            {
                SqlDataReader reader;
                string SendMessage = "select email_id from supplier where supplier_id = '" + ec.supplier_id + "'";
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-7LPLKRJ\\SQLEXPRESS;Initial Catalog=ALREHMANPHARMACY;Integrated Security=True"))
                {
                    con.Open();
                    SqlCommand MyCommand = new SqlCommand(SendMessage, con);
                    ArrayList emailArray = new ArrayList();
                    reader = MyCommand.ExecuteReader();
                    string a = "";
                    while (reader.Read())
                    {
                        a = reader["email_id"].ToString();
                    }
                    return a;
                }
            }
            catch
            {
                return null;
            }
        }

        //    public List<EmailAddress> SendEmailAll(EmailContent ec)
        //    {
        //        try
        //        {
        //            SqlDataReader reader;
        //            string SendMessage = "select StudentEmail from StudentsDetail where RollNumber like '" + ec.Project + "%'";
        //            using (SqlConnection con = new SqlConnection("Data Source=DAVID;Initial Catalog=DMS;Integrated Security=True"))
        //            {
        //                con.Open();
        //                SqlCommand MyCommand = new SqlCommand(SendMessage, con);
        //                ArrayList emailArray = new ArrayList();
        //                reader = MyCommand.ExecuteReader();
        //                var emails = new List<EmailAddress>();
        //                while (reader.Read())
        //                {
        //                    emails.Add(new EmailAddress
        //                    {
        //                        Email = Convert.ToString(reader["StudentEmail"]),
        //                    });
        //                }
        //                return emails;
        //            }
        //        }
        //        catch
        //        {
        //            return null;
        //        }
        //    }
    }
}