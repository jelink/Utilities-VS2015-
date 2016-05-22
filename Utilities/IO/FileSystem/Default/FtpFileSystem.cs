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

using Utilities.IO.FileSystem.BaseClasses;
using Utilities.IO.FileSystem.Interfaces;

namespace Utilities.IO.FileSystem.Default
{
    /// <summary>
    /// FTP file system
    /// </summary>
    public class FtpFileSystem : FileSystemBase
    {
        /// <summary>
        /// Name of the file system
        /// </summary>
        public override string Name { get { return "FTP"; } }

        /// <summary>
        /// Relative starter
        /// </summary>
        protected override string HandleRegexString { get { return @"^ftps?://"; } }

        /// <summary>
        /// Gets the directory representation for the directory
        /// </summary>
        /// <param name="Path">Path to the directory</param>
        /// <param name="Domain">Domain of the user (optional)</param>
        /// <param name="Password">Password to be used to access the directory (optional)</param>
        /// <param name="UserName">User name to be used to access the directory (optional)</param>
        /// <returns>The directory object</returns>
        public override IDirectory Directory(string Path, string UserName = "", string Password = "", string Domain = "")
        {
            Path = AbsolutePath(Path);
            return new FtpDirectory(Path, UserName, Password, Domain);
        }

        /// <summary>
        /// Gets the class representation for the file
        /// </summary>
        /// <param name="Path">Path to the file</param>
        /// <param name="Domain">Domain of the user (optional)</param>
        /// <param name="Password">Password to be used to access the file (optional)</param>
        /// <param name="UserName">User name to be used to access the file (optional)</param>
        /// <returns>The file object</returns>
        public override IFile File(string Path, string UserName = "", string Password = "", string Domain = "")
        {
            Path = AbsolutePath(Path);
            return new FtpFile(Path, UserName, Password, Domain);
        }

        /// <summary>
        /// Gets the absolute path of the variable passed in
        /// </summary>
        /// <param name="Path">Path to convert to absolute</param>
        /// <returns>The absolute path of the path passed in</returns>
        protected override string AbsolutePath(string Path)
        {
            return Path;
        }

        /// <summary>
        /// Used to dispose of any resources
        /// </summary>
        /// <param name="Managed">Should managed resources be disposed</param>
        protected override void Dispose(bool Managed)
        {
        }
    }
}