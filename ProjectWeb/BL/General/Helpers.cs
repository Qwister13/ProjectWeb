namespace ProjectWeb.BL.General
{
    public static class Helpers
    {
        public static int? StringToIntDef(string str, int? def) //Парсинг строки
        {
            int value;
            if(int.TryParse(str, out value))
                return value;
            return def;
        }
    }
}
