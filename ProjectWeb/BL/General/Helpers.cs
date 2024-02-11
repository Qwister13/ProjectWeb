using System.Transactions;

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
        public static Guid? StringToGuidDef(string str) //Парсинг строки
        {
            Guid value;
            if (Guid.TryParse(str, out value))
                return value;
            return null;
        }
        public static TransactionScope CreateTransactionScope(int second = 60)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, second),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }
    }
}
