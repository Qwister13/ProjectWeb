using ProjectWebTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;


namespace ProjectWebTest
{
    public class SessionTest : Helpers.BaseTest
    {
        [Test]
        public async Task CreateSessionTest()
        {
            using(TransactionScope scope = Helper.CreateTransactionScope())
            {
                var session = await this.dbSession.GetSession();
                var dbSession = await this.dbSessionDAL.Get(session.DbSessionId);

                Assert.NotNull(dbSession);
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session.DbSessionId));

                var session2 = await this.dbSession.GetSession();
                Assert.That(dbSession.DbSessionId, Is.EqualTo(session2.DbSessionId));
            }
        } 
    }
}
