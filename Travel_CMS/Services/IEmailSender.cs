using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mpower.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(EmailBase emailBase);
    }
}
