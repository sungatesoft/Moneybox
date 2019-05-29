using Moneybox.App.Domain.Constants;
using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Validators
{
    public class WithdrawValidator : IValidator
    {
        private INotificationService notificationService;
        public WithdrawValidator(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void Validate(Account account)
        {
            if (account.Balance < 0m)
            {
                throw new InvalidOperationException("Insufficient funds to make this transaction");
            }

            if (account.Balance < Limits.FUNDSLOWNOTIFICATIONLIMIT)
            {
                this.notificationService.NotifyFundsLow(account.User.Email);
            }
        }
    }
}
