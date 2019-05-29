using Moneybox.App.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Moneybox.App.Domain.Validators
{
    public interface IValidator
    {
        void Validate(Account account);
    }
}
