using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ProjectWebTest.Helpers
{
    static public class Helper
    {
        public static TransactionScope CreateTransactionScope(int second = 99999999)
        {
            return new TransactionScope(
                TransactionScopeOption.Required,
                new TimeSpan(0, 0, second),
                TransactionScopeAsyncFlowOption.Enabled
                );
        }

    }
}
