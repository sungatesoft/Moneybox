using Moneybox.App;
using Moneybox.App.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Moneybox.UnitTest.TestServices
{
    public class TestAccountRepository : IAccountRepository
    {
        List<Account> testAccountList;
        public TestAccountRepository()
        {
            testAccountList = new List<Account>();
            testAccountList.Add
                (
                new Account
                {
                    Id = new Guid("ed817038-feff-4e09-a517-d70166c5c995"),
                    Balance = 0,
                    User = new User { Email = "asdf@1234.test" }
                });
            testAccountList.Add
                (
                new Account
                {
                    Id = new Guid("c7b7f0db-4a4f-4f56-ac65-c64e4fe08337"),
                    PaidIn = 0,
                    User = new User { Email = "asdf@12345.test" }
                });

            testAccountList.Add
                (
                new Account
                {
                    Id = new Guid("99662f03-2d84-44c1-9282-7363ee9d9776"),
                    Balance = 5000m,
                    User = new User { Email = "asdf@1234.test" }
                });
            testAccountList.Add
                (
                new Account
                {
                    Id = new Guid("8e15fd8d-6dc2-4415-a5fa-93de494322bf"),
                    PaidIn = 0,
                    User = new User { Email = "asdf@12345.test" }
                });
        }

        public Account GetAccountById(Guid accountId)
        {
            return testAccountList.FirstOrDefault(x => x.Id == accountId);
        }

        public void Update(Account account)
        {

        }
    }
}
