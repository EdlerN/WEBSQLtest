using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace maildb.Domain
{
    [DisplayName("Mail")]
    public class MailClass
    {
        [Key]
        [HiddenInput(DisplayValue=false)]
        public int idmail { get; set; }

        [Required(ErrorMessage="Please enter email title")]
        [Display(Name = "Title")]
        public string title { get; set; }

        [HiddenInput(DisplayValue = true)]
        [Display(Name = "Registration Date")]
        public DateTime? regdate { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string adr { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string snd { get; set; }

        public string tags { get; set; }

        public string text { get; set; }

        public MailClass() {}

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MailClass(int mailid, string title, DateTime? regdate, string adr, string snd, string tags, string text)
        {
            this.idmail = mailid;
            this.title = title;
            this.regdate = regdate;
            this.adr = adr;
            this.snd = snd;
            this.tags = tags;
            this.text = text;
        }
    }
}