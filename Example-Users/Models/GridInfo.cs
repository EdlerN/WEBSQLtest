using System;
using System.Web.Helpers;

namespace maildb.Models
{
    public class PagingInfo
    {
        // всего строк в выборке
        public int totalItems { get; set; }
        // сколько отображать на страницу
        public int itemsPerPage { get; set; }
        // текущая страница
        public int currentPage { get; set; }
        // сколько максимально можно отобразить ссылок на страницы таблицы
        public int showPages { get; set; }
        // всего страниц
        public int totalPages
        {
            get { return (int)Math.Ceiling((decimal)totalItems / itemsPerPage); }
        }
        // сколько отобразить ссылок на страницы слева и справа от текущей
        public int pagesSide
        {
            get { return (int)Math.Truncate((decimal)showPages / 2); }
        }
    }

    public class SortingInfo
    {
        // название поля, по которому идёт сортировка
        public string currentOrder { get; set; }
        // направление сортировки
        public SortDirection currentDirection { get; set; }
        // получение строки параметра для передачи
        public string currentSort
        {
            get { return currentDirection != SortDirection.Descending ? currentOrder : currentOrder + "_desc"; }
        }
        // генерация нового порядка сортировки для столбцов (если уже была сортировка по столбцу - делаем обратную сортировку)
        public string NewOrder(string columnName)
        {
            return columnName == currentOrder && currentDirection != SortDirection.Descending ? columnName + "_desc" : columnName;
        }
    }
}