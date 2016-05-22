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
using System.Diagnostics.Contracts;
using System.IO;
using System.Text;
using Utilities.IO.FileSystem.Interfaces.Contracts;

namespace Utilities.IO.FileSystem.Interfaces
{
    /// <summary>
    /// Represents an individual file
    /// </summary>
    [ContractClass(typeof(IFileContract))]
    public interface IFile : IComparable<IFile>, IComparable, IEquatable<IFile>, ICloneable
    {
        /// <summary>
        /// Last time the file was accessed
        /// </summary>
        DateTime Accessed { get; }

        /// <summary>
        /// When the file was created
        /// </summary>
        DateTime Created { get; }

        /// <summary>
        /// Directory the file is in
        /// </summary>
        IDirectory Directory { get; }

        /// <summary>
        /// Does the file exist currently
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// File extension
        /// </summary>
        string Extension { get; }

        /// <summary>
        /// Full path to the file
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Size of the file in bytes
        /// </summary>
        long Length { get; }

        /// <summary>
        /// When the file was last modified
        /// </summary>
        DateTime Modified { get; }

        /// <summary>
        /// File name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Copies the file to another directory
        /// </summary>
        /// <param name="Directory">Directory to copy the file to</param>
        /// <param name="Overwrite">Should the file overwrite another file if found</param>
        /// <returns>The newly created file</returns>
        IFile CopyTo(IDirectory Directory, bool Overwrite);

        /// <summary>
        /// Deletes the file
        /// </summary>
        /// <returns>Any response for deleting the resource (usually FTP, HTTP, etc)</returns>
        string Delete();

        /// <summary>
        /// Moves the file to another directory
        /// </summary>
        /// <param name="Directory">Directory to move the file to</param>
        void MoveTo(IDirectory Directory);

        /// <summary>
        /// Reads the file to the end as a string
        /// </summary>
        /// <returns>A string containing the contents of the file</returns>
        string Read();

        /// <summary>
        /// Reads the file to the end as a byte array
        /// </summary>
        /// <returns>A byte array containing the contents of the file</returns>
        byte[] ReadBinary();

        /// <summary>
        /// Renames the file
        /// </summary>
        /// <param name="NewName">New file name</param>
        void Rename(string NewName);

        /// <summary>
        /// Writes content to the file
        /// </summary>
        /// <param name="Content">Content to write</param>
        /// <param name="Mode">File mode</param>
        /// <param name="Encoding">Encoding that the content should be saved as (default is UTF8)</param>
        /// <returns>The result of the write or original content</returns>
        string Write(string Content, FileMode Mode = FileMode.Create, Encoding Encoding = null);

        /// <summary>
        /// Writes content to the file
        /// </summary>
        /// <param name="Content">Content to write</param>
        /// <param name="Mode">File mode</param>
        /// <returns>The result of the write or original content</returns>
        byte[] Write(byte[] Content, FileMode Mode = FileMode.Create);
    }
}