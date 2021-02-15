using maildb.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace maildb.Models
{
    public class MailModel
    {
        public MailClass Mail { get; set; }

        public MailModel() {}
        public MailModel(MailClass mail)
        {
            this.Mail = mail;          
        }

    }
}