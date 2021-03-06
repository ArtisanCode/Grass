//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ArtisanCode.UsingGrass
{
    using Microsoft.Win32;
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.AccessControl;
    using System.Text;
    
    
    [GeneratedCode("0.0.0.1", "ArtisanCode.Grass")]
    public interface IFile
    {
        
        void AppendAllLines(string path, System.Collections.Generic.IEnumerable<string> contents);
        
        void AppendAllLines(string path, System.Collections.Generic.IEnumerable<string> contents, System.Text.Encoding encoding);
        
        void AppendAllText(string path, string contents);
        
        void AppendAllText(string path, string contents, System.Text.Encoding encoding);
        
        System.IO.StreamWriter AppendText(string path);
        
        void Copy(string sourceFileName, string destFileName);
        
        void Copy(string sourceFileName, string destFileName, bool overwrite);
        
        System.IO.FileStream Create(string path);
        
        System.IO.FileStream Create(string path, int bufferSize);
        
        System.IO.FileStream Create(string path, int bufferSize, System.IO.FileOptions options);
        
        System.IO.FileStream Create(string path, int bufferSize, System.IO.FileOptions options, System.Security.AccessControl.FileSecurity fileSecurity);
        
        System.IO.StreamWriter CreateText(string path);
        
        void Decrypt(string path);
        
        void Delete(string path);
        
        void Encrypt(string path);
        
        bool Exists(string path);
        
        System.Security.AccessControl.FileSecurity GetAccessControl(string path);
        
        System.Security.AccessControl.FileSecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections);
        
        System.IO.FileAttributes GetAttributes(string path);
        
        System.DateTime GetCreationTime(string path);
        
        System.DateTime GetCreationTimeUtc(string path);
        
        System.DateTime GetLastAccessTime(string path);
        
        System.DateTime GetLastAccessTimeUtc(string path);
        
        System.DateTime GetLastWriteTime(string path);
        
        System.DateTime GetLastWriteTimeUtc(string path);
        
        void Move(string sourceFileName, string destFileName);
        
        System.IO.FileStream Open(string path, System.IO.FileMode mode);
        
        System.IO.FileStream Open(string path, System.IO.FileMode mode, System.IO.FileAccess access);
        
        System.IO.FileStream Open(string path, System.IO.FileMode mode, System.IO.FileAccess access, System.IO.FileShare share);
        
        System.IO.FileStream OpenRead(string path);
        
        System.IO.StreamReader OpenText(string path);
        
        System.IO.FileStream OpenWrite(string path);
        
        byte[] ReadAllBytes(string path);
        
        string[] ReadAllLines(string path);
        
        string[] ReadAllLines(string path, System.Text.Encoding encoding);
        
        string ReadAllText(string path);
        
        string ReadAllText(string path, System.Text.Encoding encoding);
        
        System.Collections.Generic.IEnumerable<string> ReadLines(string path);
        
        System.Collections.Generic.IEnumerable<string> ReadLines(string path, System.Text.Encoding encoding);
        
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName);
        
        void Replace(string sourceFileName, string destinationFileName, string destinationBackupFileName, bool ignoreMetadataErrors);
        
        void SetAccessControl(string path, System.Security.AccessControl.FileSecurity fileSecurity);
        
        void SetAttributes(string path, System.IO.FileAttributes fileAttributes);
        
        void SetCreationTime(string path, System.DateTime creationTime);
        
        void SetCreationTimeUtc(string path, System.DateTime creationTimeUtc);
        
        void SetLastAccessTime(string path, System.DateTime lastAccessTime);
        
        void SetLastAccessTimeUtc(string path, System.DateTime lastAccessTimeUtc);
        
        void SetLastWriteTime(string path, System.DateTime lastWriteTime);
        
        void SetLastWriteTimeUtc(string path, System.DateTime lastWriteTimeUtc);
        
        void WriteAllBytes(string path, byte[] bytes);
        
        void WriteAllLines(string path, string[] contents);
        
        void WriteAllLines(string path, string[] contents, System.Text.Encoding encoding);
        
        void WriteAllLines(string path, System.Collections.Generic.IEnumerable<string> contents);
        
        void WriteAllLines(string path, System.Collections.Generic.IEnumerable<string> contents, System.Text.Encoding encoding);
        
        void WriteAllText(string path, string contents);
        
        void WriteAllText(string path, string contents, System.Text.Encoding encoding);
    }
}
