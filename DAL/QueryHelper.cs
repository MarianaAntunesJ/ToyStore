namespace ToyStore.DAL
{
    class QueryHelper
    {
        private static string _insertInto = "INSERT INTO";
        private static string _selectFrom = "SELECT * FROM";
        private static string _update = "UPDATE";
        public static string _values = "VALUES";
        public static string _set = "SET";

        public static string GetInsertInto(string table) => $"{_insertInto} {table} OUTPUT INSERTED.Id {_values}";
        public static string GetSelectFrom(string table) => $"{_selectFrom} {table}";
        public static string GetUpdateSet(string table) => $"{_update} {table} {_set}";
    }
}
