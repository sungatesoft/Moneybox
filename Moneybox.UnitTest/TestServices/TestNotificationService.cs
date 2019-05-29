using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.UnitTest
{
    public class TestNotificationService : INotificationService
    {
        public void NotifyApproachingPayInLimit(string emailAddress)
        {
            throw new NotImplementedException($"NotifyApproachingPayInLimit {emailAddress}");
        }

        public void NotifyFundsLow(string emailAddress)
        {
            throw new NotImplementedException($"NotifyFundsLow {emailAddress}");
        }
    }
}
