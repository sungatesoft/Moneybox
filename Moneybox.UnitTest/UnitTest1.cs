using Moneybox.App;
using Moneybox.App.Domain.Constants;
using Moneybox.App.Domain.Services;
using Moneybox.App.Domain.Validators;
using Moneybox.App.Features;
using Moneybox.UnitTest.TestServices;
using System;
using Xunit;

namespace Moneybox.UnitTest
{
    public class UnitTest1
    {
        [Theory]
        [InlineData(-1.00)]
        [InlineData(-100.11)]
        [InlineData(-10000.11)]
        public void Test_Money_Withdraw_Validate_Insufficient_Funds(decimal balance)
        {
            Account account = new Account() { User = new User() { Email = string.Empty }, Balance = balance };
            Assert.Throws<InvalidOperationException>(() => account.Validate(new WithdrawValidator(new TestNotificationService())));
        }

        [Fact]
        public void Test_Money_Withdraw_Validate_Sufficient_Funds_Without_Notification()
        {
            Account account = new Account() { Balance = 500m };

            var ex = Record.Exception
                (
                    () => account.Validate(new WithdrawValidator(new TestNotificationService()))
                );
            Assert.Null(ex);
        }

        [Fact]
        public void Test_Money_Withdraw_Validate_Sufficient_Funds_With_Notification()
        {
            string testMail = "asdf@asdf.test";

            Account account = new Account()
            {
                User = new User { Email = testMail },
                Balance = 499m
            };

            var exception = Assert.Throws<NotImplementedException>(() => account.Validate(new WithdrawValidator(new TestNotificationService())));

            Assert.Equal($"NotifyFundsLow {testMail}", exception.Message);
        }


        [Fact]
        public void Test_Money_Transfer_Validate_Account_Pay_In_Limit_Reached()
        {
            Account account = new Account() { PaidIn = 40001m };
            Assert.Throws<InvalidOperationException>(() => account.Validate(new TransferValidator(new TestNotificationService())));
        }

        [Fact]
        public void Test_Money_Transfer_Validate_Success_Without_Notification()
        {
            Account account = new Account() { Id = new Guid(), PaidIn = 1000m };

            var ex = Record.Exception
                (
                    () => account.Validate(new TransferValidator(new TestNotificationService()))
                );
            Assert.Null(ex);
        }

        [Fact]
        public void Test_Money_Transfer_Validate_Success_With_Notification()
        {
            string testMail = "asdfx@asdf.test";

            Account account = new Account()
            {
                User = new User { Email = testMail },
                PaidIn = 3501m
            };

            var exception = Assert.Throws<NotImplementedException>(() => account.Validate(new TransferValidator(new TestNotificationService())));

            Assert.Equal($"NotifyApproachingPayInLimit {testMail}", exception.Message);
        }

        [Fact]
        public void Test_WithdrawMoney_Execute_Insufficient_Funds()
        {
            WithdrawMoney transferMoney = new WithdrawMoney(new TestAccountRepository(), new TestNotificationService());

            var exception = Assert.Throws<InvalidOperationException>(() =>
                transferMoney.Execute
                (
                    new Guid("ed817038-feff-4e09-a517-d70166c5c995"),
                    2
                )
            );

            Assert.Equal($"Insufficient funds to make this transaction", exception.Message);
        }

        [Fact]
        public void Test_WithdrawMoney_Execute_With_NotifyFundsLow()
        {
            WithdrawMoney transferMoney = new WithdrawMoney(new TestAccountRepository(), new TestNotificationService());

            var exception = Assert.Throws<NotImplementedException>(() =>
                transferMoney.Execute
                (
                    new Guid("99662f03-2d84-44c1-9282-7363ee9d9776"),
                    4501m
                )
            );

            Assert.Equal("NotifyFundsLow asdf@1234.test", exception.Message);
        }

        [Fact]
        public void Test_WithdrawMoney_Execute_Success_Without_Notification()
        {
            WithdrawMoney transferMoney = new WithdrawMoney(new TestAccountRepository(), new TestNotificationService());

            var ex = Record.Exception
                (
                    () =>
                            transferMoney.Execute
                            (
                                new Guid("99662f03-2d84-44c1-9282-7363ee9d9776"),
                                400m
                            )
                );
            Assert.Null(ex);
        }

        [Fact]
        public void Test_TransferMoney_Execute_Insufficient_Funds()
        {
            TransferMoney transferMoney = new TransferMoney(new TestAccountRepository(), new TestNotificationService());

            var exception = Assert.Throws<InvalidOperationException>(() =>
                transferMoney.Execute
                (
                    new Guid("ed817038-feff-4e09-a517-d70166c5c995"),
                    new Guid("c7b7f0db-4a4f-4f56-ac65-c64e4fe08337"),
                    2
                )
            );

            Assert.Equal($"Insufficient funds to make this transaction", exception.Message);
        }

        [Fact]
        public void Test_TransferMoney_Execute_Account_Pay_In_Limit_Reached()
        {
            TransferMoney transferMoney = new TransferMoney(new TestAccountRepository(), new TestNotificationService());

            var exception = Assert.Throws<InvalidOperationException>(() =>
                transferMoney.Execute
                (
                    new Guid("99662f03-2d84-44c1-9282-7363ee9d9776"),
                    new Guid("8e15fd8d-6dc2-4415-a5fa-93de494322bf"),
                    4001m
                )
            );

            Assert.Equal($"Account pay in limit reached", exception.Message);
        }

        [Fact]
        public void Test_TransferMoney_Execute_Success_Without_Notification()
        {
            TransferMoney transferMoney = new TransferMoney(new TestAccountRepository(), new TestNotificationService());

            var ex = Record.Exception
                (
                    () =>
                            transferMoney.Execute
                            (
                                new Guid("99662f03-2d84-44c1-9282-7363ee9d9776"),
                                new Guid("8e15fd8d-6dc2-4415-a5fa-93de494322bf"),
                                400m
                            )
                );
            Assert.Null(ex);
        }
    }
}
