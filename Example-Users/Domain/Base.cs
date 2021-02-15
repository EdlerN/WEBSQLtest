using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace maildb.Domain
{
    public static class Base
    {
        private static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];
            }
        }

        public static string strConnect
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionString].ConnectionString;
            }
        }

        public static string parseSortForDB(string sortOrder, out string sortName, out System.Web.Helpers.SortDirection sortDir)
        {// проверка и разбор параметра для сортировки
            sortName = null;
            sortDir = System.Web.Helpers.SortDirection.Ascending;
            if (sortOrder != null && sortOrder != string.Empty)
            {// оставляем только буквы/цифры/_
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("[^\\w]");
                bool a = regex.IsMatch(sortOrder);
                sortOrder = regex.Replace(sortOrder, "");
                // разбиваем на части - название столбца и направление
                string[] sort = sortOrder.Split('_');
                if (sort.Length > 0) sortName = sort[0];
                if (sort.Length > 1) if (sort[1] == "desc") sortDir = System.Web.Helpers.SortDirection.Descending;
            }
            if (sortName != null && sortName != string.Empty)
            {
                sortOrder = sortName;
                if (sortDir == System.Web.Helpers.SortDirection.Descending) sortOrder += " desc";
            }
            else
            {
                sortOrder = "";
                sortDir = System.Web.Helpers.SortDirection.Ascending;
            }
            return sortOrder;
        }
    }
}