using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email
{
    public interface IEmailService : IDisposable
    {
        public Task<Result.Result> SendAsync(EmailBuilder emailBuilder);
    }
}
