﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System.Collections.Generic;
using System.Net.Mail;
using Utilities.IO.Messaging;
using Utilities.IO.Messaging.BaseClasses;
using Utilities.IO.Messaging.Interfaces;

namespace Utilities.IO
{
    /// <summary>
    /// Email message class
    /// </summary>
    public class EmailMessage : MessageBase, IMessage
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EmailMessage()
            : base(IoC.Manager.Bootstrapper.Resolve<Manager>().MessagingSystems[typeof(EmailMessage)])
        {
            Attachments = new List<Attachment>();
            EmbeddedResources = new List<LinkedResource>();
            Priority = MailPriority.Normal;
            Port = 25;
        }

        /// <summary>
        /// Attachments
        /// </summary>
        public ICollection<Attachment> Attachments { get; private set; }

        /// <summary>
        /// BCC
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// CC
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Embedded resource
        /// </summary>
        public ICollection<LinkedResource> EmbeddedResources { get; private set; }

        /// <summary>
        /// Password for the user
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Port to use
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Priority
        /// </summary>
        public MailPriority Priority { get; set; }

        /// <summary>
        /// Server
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// User name for the user
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Use SSL?
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Disposes of the objects
        /// </summary>
        /// <param name="Managed">
        /// True to dispose of all resources, false only disposes of native resources
        /// </param>
        protected override void Dispose(bool Managed)
        {
            if (Attachments != null)
            {
                foreach (Attachment Attachment in Attachments)
                {
                    Attachment.Dispose();
                }
                Attachments = null;
            }
            if (EmbeddedResources != null)
            {
                foreach (LinkedResource Resource in EmbeddedResources)
                {
                    Resource.Dispose();
                }
                EmbeddedResources = null;
            }
        }
    }
}