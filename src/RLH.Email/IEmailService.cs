using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLH.Results;

namespace RLH.Email
{
    public interface IEmailService : IDisposable
    {
        public Task<Result> SendAsync(EmailBuilder emailBuilder);

        public Result CheckEmailBuilderValues(EmailBuilder builder);
    }
}
