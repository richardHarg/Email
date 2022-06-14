using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email.MimeKit.ASPNETCore
{
    public class OptionsMikeKitEmailService : MimeKitEmailService
    {
        public OptionsMikeKitEmailService(IOptions<EmailOptions> options) : base(options.Value)
        {
        }
    }
}
