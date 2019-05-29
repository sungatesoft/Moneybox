using Moneybox.App.Domain.Constants;
using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Validators
{
    public class TransferValidator : IValidator
    {
        private INotificationService notificationService;
        public TransferValidator(INotificationService notificationService)
        {
            this.notificationService = notificationService;
        }

        public void Validate(Account account)
        {
            if (account.PaidIn > Account.PayInLimit)
            {
                throw new InvalidOperationException("Account pay in limit reached");
            }

            if (Account.PayInLimit - account.PaidIn < Limits.APPROACHINGHPAYINNOTIFICATIONLIMIT)
            {
                notificationService.NotifyApproachingPayInLimit(account.User.Email);
            }
        }
    }
}
