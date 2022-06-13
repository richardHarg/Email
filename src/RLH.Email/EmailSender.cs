﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RLH.Email
{
    public class EmailSender
    {
        internal EmailSender(string emailAddress,string smtpServer,int port,string userName,string password)
        {
            EmailAddress = emailAddress;
            SmtpServer = smtpServer;
            Port = port;
            Username = userName;
            Password = password;
        }

        /// <summary>
        /// Email address of outbound sender
        /// </summary>
        public string EmailAddress { get; private set; }
        /// <summary>
        /// SMTP server address
        /// </summary>
        public string SmtpServer { get; private set; }
        /// <summary>
        /// SMTP port 
        /// </summary>
        public int Port { get; private set; }
        /// <summary>
        /// SMTP Username
        /// </summary>
        public string Username { get; private set; }
        /// <summary>
        /// SMTP password
        /// </summary>
        public string Password { get; private set; }

    }
}