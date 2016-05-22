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

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Net;
using Utilities.DataTypes;
using Utilities.IO.Enums;
using Utilities.IO.FileSystem.BaseClasses;
using Utilities.IO.FileSystem.Interfaces;

namespace Utilities.IO.FileSystem.Default
{
    /// <summary>
    /// Directory class
    /// </summary>
    public class FtpDirectory : DirectoryBase<Uri, FtpDirectory>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FtpDirectory()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Path">Path to the directory</param>
        /// <param name="Domain">Domain of the user (optional)</param>
        /// <param name="Password">Password to be used to access the directory (optional)</param>
        /// <param name="UserName">User name to be used to access the directory (optional)</param>
        public FtpDirectory(string Path, string UserName = "", string Password = "", string Domain = "")
            : this(string.IsNullOrEmpty(Path) ? null : new Uri(Path), UserName, Password, Domain)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Directory">Internal directory</param>
        /// <param name="Domain">Domain of the user (optional)</param>
        /// <param name="Password">Password to be used to access the directory (optional)</param>
        /// <param name="UserName">User name to be used to access the directory (optional)</param>
        public FtpDirectory(Uri Directory, string UserName = "", string Password = "", string Domain = "")
            : base(Directory, UserName, Password, Domain)
        {
        }

        /// <summary>
        /// returns now
        /// </summary>
        public override DateTime Accessed
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// returns now
        /// </summary>
        public override DateTime Created
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// returns true
        /// </summary>
        public override bool Exists
        {
            get { return true; }
        }

        /// <summary>
        /// Full path
        /// </summary>
        public override string FullName
        {
            get { return InternalDirectory == null ? "" : InternalDirectory.AbsolutePath; }
        }

        /// <summary>
        /// returns now
        /// </summary>
        public override DateTime Modified
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// Full path
        /// </summary>
        public override string Name
        {
            get { return InternalDirectory == null ? "" : InternalDirectory.AbsolutePath; }
        }

        /// <summary>
        /// Full path
        /// </summary>
        public override IDirectory Parent
        {
            get { return InternalDirectory == null ? null : new FtpDirectory((string)InternalDirectory.AbsolutePath.Take(InternalDirectory.AbsolutePath.LastIndexOf("/", StringComparison.OrdinalIgnoreCase) - 1), UserName, Password, Domain); }
        }

        /// <summary>
        /// Root
        /// </summary>
        public override IDirectory Root
        {
            get { return InternalDirectory == null ? null : new FtpDirectory(InternalDirectory.Scheme + "://" + InternalDirectory.Host, UserName, Password, Domain); }
        }

        /// <summary>
        /// Size (returns 0)
        /// </summary>
        public override long Size
        {
            get { return 0; }
        }

        /// <summary>
        /// Not used
        /// </summary>
        public override void Create()
        {
            var Request = WebRequest.Create(InternalDirectory) as FtpWebRequest;
            Request.Method = WebRequestMethods.Ftp.MakeDirectory;
            SetupData(Request, null);
            SetupCredentials(Request);
            SendRequest(Request);
        }

        /// <summary>
        /// Not used
        /// </summary>
        public override void Delete()
        {
            var Request = WebRequest.Create(InternalDirectory) as FtpWebRequest;
            Request.Method = WebRequestMethods.Ftp.RemoveDirectory;
            SetupData(Request, null);
            SetupCredentials(Request);
            SendRequest(Request);
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="SearchPattern"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public override IEnumerable<IDirectory> EnumerateDirectories(string SearchPattern, SearchOption Options = SearchOption.TopDirectoryOnly)
        {
            var Request = WebRequest.Create(InternalDirectory) as FtpWebRequest;
            Request.Method = WebRequestMethods.Ftp.ListDirectory;
            SetupData(Request, null);
            SetupCredentials(Request);
            string Data = SendRequest(Request);
            string[] Folders = Data.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            SetupData(Request, null);
            SetupCredentials(Request);
            Data = SendRequest(Request);
            string[] DetailedFolders = Data.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var Directories = new List<IDirectory>();
            foreach (string Folder in Folders)
            {
                string DetailedFolder = DetailedFolders.FirstOrDefault(x => x.EndsWith(Folder, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(DetailedFolder))
                {
                    if (DetailedFolder.StartsWith("d", StringComparison.OrdinalIgnoreCase) && !DetailedFolder.EndsWith(".", StringComparison.OrdinalIgnoreCase))
                    {
                        Directories.Add(new DirectoryInfo(FullName + "/" + Folder, UserName, Password, Domain));
                    }
                }
            }
            return Directories;
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="SearchPattern"></param>
        /// <param name="Options"></param>
        /// <returns></returns>
        public override IEnumerable<IFile> EnumerateFiles(string SearchPattern = "*", SearchOption Options = SearchOption.TopDirectoryOnly)
        {
            var Request = WebRequest.Create(InternalDirectory) as FtpWebRequest;
            Request.Method = WebRequestMethods.Ftp.ListDirectory;
            SetupData(Request, null);
            SetupCredentials(Request);
            string Data = SendRequest(Request);
            string[] Folders = Data.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            Request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            SetupData(Request, null);
            SetupCredentials(Request);
            Data = SendRequest(Request);
            string[] DetailedFolders = Data.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            var Directories = new List<IFile>();
            foreach (string Folder in Folders)
            {
                string DetailedFolder = DetailedFolders.FirstOrDefault(x => x.EndsWith(Folder, StringComparison.OrdinalIgnoreCase));
                if (!string.IsNullOrEmpty(DetailedFolder))
                {
                    if (!DetailedFolder.StartsWith("d", StringComparison.OrdinalIgnoreCase))
                    {
                        Directories.Add(new FileInfo(FullName + "/" + Folder, UserName, Password, Domain));
                    }
                }
            }
            return Directories;
        }

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="Name"></param>
        public override void Rename(string Name)
        {
            var Request = WebRequest.Create(InternalDirectory) as FtpWebRequest;
            Request.Method = WebRequestMethods.Ftp.Rename;
            Request.RenameTo = Name;
            SetupData(Request, null);
            SetupCredentials(Request);
            SendRequest(Request);
            InternalDirectory = new Uri(FullName + "/" + Name);
        }

        /// <summary>
        /// Sends the request to the URL specified
        /// </summary>
        /// <param name="Request">The web request object</param>
        /// <returns>The string returned by the service</returns>
        private static string SendRequest(FtpWebRequest Request)
        {
            Contract.Requires<ArgumentNullException>(Request != null, "Request");
            using (FtpWebResponse Response = Request.GetResponse() as FtpWebResponse)
            {
                using (StreamReader Reader = new StreamReader(Response.GetResponseStream()))
                {
                    return Reader.ReadToEnd();
                }
            }
        }

        /// <summary>
        /// Sets up any credentials (basic authentication, for OAuth, please use the OAuth class to
        /// create the
        /// URL)
        /// </summary>
        /// <param name="Request">The web request object</param>
        private void SetupCredentials(FtpWebRequest Request)
        {
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                Request.Credentials = new NetworkCredential(UserName, Password);
            }
        }

        /// <summary>
        /// Sets up any data that needs to be sent
        /// </summary>
        /// <param name="Request">The web request object</param>
        /// <param name="Data">Data to send with the request</param>
        private void SetupData(FtpWebRequest Request, byte[] Data)
        {
            Contract.Requires<ArgumentNullException>(Request != null, "Request");
            Contract.Requires<NullReferenceException>(!string.IsNullOrEmpty(Name), "Name");
            Request.UsePassive = true;
            Request.KeepAlive = false;
            Request.UseBinary = true;
            Request.EnableSsl = Name.ToUpperInvariant().StartsWith("FTPS", StringComparison.OrdinalIgnoreCase);
            if (Data == null)
            {
                Request.ContentLength = 0;
                return;
            }
            Request.ContentLength = Data.Length;
            using (Stream RequestStream = Request.GetRequestStream())
            {
                RequestStream.Write(Data, 0, Data.Length);
            }
        }
    }
}