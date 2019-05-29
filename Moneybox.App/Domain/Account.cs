using Moneybox.App.Domain.Constants;
using Moneybox.App.Domain.Services;
using Moneybox.App.Domain.Validators;
using System;

namespace Moneybox.App
{
    public class Account
    {
        public const decimal PayInLimit = Limits.PAYINLIMIT;

        public Guid Id { get; set; }

        public User User { get; set; }

        public decimal Balance { get; set; }

        public decimal Withdrawn { get; set; }

        public decimal PaidIn { get; set; }

        public void Validate(IValidator validator)
        {
            validator.Validate(this);
        }
    }
}
