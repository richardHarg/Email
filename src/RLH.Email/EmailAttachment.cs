using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email
{
    public class EmailAttachment
    {
        internal EmailAttachment(byte[] bytes,string fileName,string extension)
        {
            if (bytes is null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }

            if (string.IsNullOrEmpty(fileName))
            {
                throw new ArgumentException($"'{nameof(fileName)}' cannot be null or empty.", nameof(fileName));
            }

            if (string.IsNullOrEmpty(extension))
            {
                throw new ArgumentException($"'{nameof(extension)}' cannot be null or empty.", nameof(extension));
            }
            Bytes = bytes;
            FileName = fileName;
            Extension = extension;
        }


        public byte[] Bytes { get; private set; }
        public string FileName { get; private set; }
        public string Extension { get; private set; }
    }
}
