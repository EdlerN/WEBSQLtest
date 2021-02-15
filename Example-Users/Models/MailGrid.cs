using maildb.Domain;
using System.Collections.Generic;

namespace maildb.Models
{
    public class MailGrid
    {
        public IEnumerable<MailClass> Mail { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public SortingInfo SortingInfo { get; set; }
    }
}