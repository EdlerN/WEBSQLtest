using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using maildb.Domain;

namespace maildb.Models
{
    public class MailRepository
    {
        public bool AddMail(MailClass mail)
        {
            mail.idmail = AddMail(Title: mail.title, Regdate: mail.regdate, AdressEmail: mail.adr, SenderEmail: mail.snd, Tags: mail.tags, MailText: mail.text);
            return mail.idmail > 0;
        }

        public int AddMail(string Title, DateTime? Regdate, string AdressEmail, string SenderEmail, string Tags, string MailText)
        {
            int ID = 0;
            using (MySqlConnection connect = new MySqlConnection(Base.strConnect))
            {
                string sql = "INSERT INTO `maildb` (`Title`, `Regdate`, `AdressEmail`, `SenderEmail`, `Tags`, `MailText`) VALUES (@Title, @Regdate, @AdressEmail, @SenderEmail, @Tags, @MailText)";
                using (MySqlCommand cmd = new MySqlCommand(sql, connect))
                {
                    cmd.Parameters.Add("Title", MySqlDbType.String).Value = Title;
                    cmd.Parameters.Add("Regdate", MySqlDbType.DateTime).Value = Regdate;
                    cmd.Parameters.Add("AdressEmail", MySqlDbType.String).Value = AdressEmail;
                    cmd.Parameters.Add("SenderEmail", MySqlDbType.String).Value = SenderEmail;
                    cmd.Parameters.Add("Tags", MySqlDbType.String).Value = Tags;
                    cmd.Parameters.Add("MailText", MySqlDbType.String).Value = MailText;
                    
                    connect.Open();
                    if (cmd.ExecuteNonQuery() >= 0)
                    {
                        sql = "SELECT LAST_INSERT_ID() AS ID";
                        cmd.CommandText = sql;
                        int.TryParse(cmd.ExecuteScalar().ToString(), out ID);
                    }
                }
            }
            return ID;
        }

        public bool ChangeMail(MailClass mail)
        {
            return ChangeMail(ID: mail.idmail, Title: mail.title, Regdate: mail.regdate, AdressEmail: mail.adr, SenderEmail: mail.snd, Tags: mail.tags, MailText: mail.text);
        }

        public bool ChangeMail(int ID, string Title, DateTime? Regdate, string AdressEmail, string SenderEmail, string Tags, string MailText)
        {
            bool result = false;
            if (ID > 0)
            {
                using (MySqlConnection connect = new MySqlConnection(Base.strConnect))
                {
                    string sql = "UPDATE `maildb` SET `Title`=@Title, `Regdate`=@Regdate, `AdressEmail`=@AdressEmail, `SenderEmail`=@SenderEmail, `Tags`=@Tags, `MailText`=@MailText,  WHERE Idmail=@Idmail";
                    using (MySqlCommand cmd = new MySqlCommand(sql, connect))
                    {
                        cmd.Parameters.Add("Idmail", MySqlDbType.Int32).Value = ID;
                        cmd.Parameters.Add("Title", MySqlDbType.String).Value = Title;
                        cmd.Parameters.Add("Regdate", MySqlDbType.DateTime).Value = Regdate;
                        cmd.Parameters.Add("AdressEmail", MySqlDbType.String).Value = AdressEmail;
                        cmd.Parameters.Add("SenderEmail", MySqlDbType.String).Value = SenderEmail;
                        cmd.Parameters.Add("Tags", MySqlDbType.String).Value = Tags;
                        cmd.Parameters.Add("MailText", MySqlDbType.String).Value = MailText;
                        connect.Open();
                        result = cmd.ExecuteNonQuery() >= 0;
                    }
                }
            }
            return result;
        }

        public bool RemoveUser(MailClass mail)
        {
            return RemoveMail(mail.idmail);
        }

        public bool RemoveMail(int ID)
        {
            using (MySqlConnection connect = new MySqlConnection(Base.strConnect))
            {
                string sql = "DELETE FROM `maildb` WHERE `Idmail`=@Idmail";
                using (MySqlCommand cmd = new MySqlCommand(sql, connect))
                {
                    cmd.Parameters.Add("Idmail", MySqlDbType.Int32).Value = ID;
                    connect.Open();
                    return cmd.ExecuteNonQuery() >= 0;
                }
            }
        }

        public MailClass FetchByID(int ID)
        {
            MailClass mail = null;
            using (MySqlConnection objConnect = new MySqlConnection(Base.strConnect))
            {
                string strSQL = "SELECT u.`Idmail`, u.`Title`, u.`Regdate`, u.`AdressEmail`, u.`SenderEmail`, u.`Tags`, u.`MailText`, FROM `maildb` AS u WHERE `idmail`=@idmail";
                using (MySqlCommand cmd = new MySqlCommand(strSQL, objConnect))
                {
                    objConnect.Open();
                    int Idmail = 0;
                    string Title = string.Empty;
                    DateTime? Regdate = null;
                    string AdressEmail = string.Empty;
                    string SenderEmail = string.Empty;
                    string Tags = string.Empty;
                    string MailText = string.Empty;
                    
                    cmd.Parameters.Add("Idmail", MySqlDbType.Int32).Value = ID;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Idmail = dr.GetInt32("Idmail");
                            Title = dr.GetString("Title").ToString();
                            Regdate = dr.GetDateTime("Regdate");
                            AdressEmail = dr.GetString("AdressEmail").ToString();
                            SenderEmail = dr.GetString("SenderEmail").ToString();
                            MailText = dr.GetString("MailText").ToString();

                            if (!dr.IsDBNull(dr.GetOrdinal("Tags"))) Tags = dr.GetString("Tags").ToString();
           
                        }
                       
                      
                        if (Idmail > 0) mail = new MailClass(mailid: Idmail, title: Title, regdate: Regdate, snd: SenderEmail, adr: AdressEmail, tags: Tags, text: MailText);
                    }
                }
            }
            return mail;
        }

        public MailClass FetchByLoginname(string Name)
        {
            MailClass mail = null;
            using (MySqlConnection objConnect = new MySqlConnection(Base.strConnect))
            {
                string strSQL = "SELECT u.`Idmail`, u.`Title`, u.`Regdate`, u.`AdressEmail`, u.`SenderEmail`, u.`Tags`, u.`MailText`, FROM `maildb` AS u WHERE `Title`=@Title";
                using (MySqlCommand cmd = new MySqlCommand(strSQL, objConnect))
                {
                    objConnect.Open();
                    int Idmail = 0;
                    string Title = string.Empty;
                    DateTime? Regdate = null;
                    string AdressEmail = string.Empty;
                    string SenderEmail = string.Empty;
                    string Tags = string.Empty;
                    string MailText = string.Empty;
                    cmd.Parameters.Add("Loginname", MySqlDbType.Int32).Value = Name;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            Idmail = dr.GetInt32("Idmail");
                            Title = dr.GetString("Title").ToString();
                            Regdate = dr.GetDateTime("Regdate");
                            AdressEmail = dr.GetString("AdressEmail").ToString();
                            SenderEmail = dr.GetString("SenderEmail").ToString();
                            MailText = dr.GetString("MailText").ToString();

                            if (!dr.IsDBNull(dr.GetOrdinal("Tags"))) Tags = dr.GetString("Tags").ToString();
                        }
                       
                        if(Idmail > 0) mail = new MailClass(mailid: Idmail, title: Title, regdate: Regdate, snd: SenderEmail, adr: AdressEmail, tags: Tags, text: MailText);
                    }
                }
            }
            return mail;
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Проверка запросов SQL на уязвимости безопасности")]
        public IList<MailClass> List(string sortOrder, System.Web.Helpers.SortDirection sortDir, int page, int pagesize, out int count)
        {
            List<MailClass> mails = new List<MailClass>();
            using (MySqlConnection objConnect = new MySqlConnection(Base.strConnect))
            {
               
                string sort = " ORDER BY ";
                
                if (sortOrder != null && sortOrder != String.Empty)
                {
                    sort += "`" + sortOrder + "`";
                    if (sortDir == System.Web.Helpers.SortDirection.Descending) sort += " DESC";
                    sort += ",";
                }
                sort += "`Idmail`"; // по умолчанию
                
                string limit = "";
                if (pagesize > 0)
                {
                    int start = (page - 1) * pagesize;
                    limit = string.Concat(" LIMIT ", start.ToString(), ", ", pagesize.ToString());
                }
                string strSQL = "SELECT SQL_CALC_FOUND_ROWS u.`Idmail`, u.`Title`, u.`Regdate`, u.`AdressEmail`, u.`SenderEmail`, u.`Tags`, u.`MailText`, FROM `maildb` AS u" + sort + limit;
                using (MySqlCommand cmd = new MySqlCommand(strSQL, objConnect))
                {
                    objConnect.Open();
                    cmd.Parameters.Add("page", MySqlDbType.Int32).Value = page;
                    cmd.Parameters.Add("pagesize", MySqlDbType.Int32).Value = pagesize;
                    using (MySqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {


                            mails.Add(new MailClass(
                                mailid: dr.GetInt32("Idmail"),
                                title: dr.GetString("Title"),

                                snd: dr.IsDBNull(dr.GetOrdinal("SenderEmail")) ? String.Empty : dr.GetString("SenderEmail"),
                                adr: dr.IsDBNull(dr.GetOrdinal("AdressEmail")) ? String.Empty : dr.GetString("AdressEmail"),
                                regdate: dr.IsDBNull(dr.GetOrdinal("Regdate")) ? (DateTime?)null : dr.GetDateTime("Regdate"),
                                tags: dr.IsDBNull(dr.GetOrdinal("Tags")) ? String.Empty : dr.GetString("Tags"),
                                text: dr.IsDBNull(dr.GetOrdinal("MailText")) ? String.Empty : dr.GetString("MailText")));
                        }
                    }
                }
               
                using (MySqlCommand cmdrows = new MySqlCommand("SELECT FOUND_ROWS()", objConnect))
                {
                    int.TryParse(cmdrows.ExecuteScalar().ToString(), out count);
                }
            }
            return mails;
        }
    }
}