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
    using Microsoft.Win32.SafeHandles;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.AccessControl;
    
    
    [GeneratedCode("0.0.0.1", "ArtisanCode.Grass")]
    public interface IDirectory
    {
        
        System.IO.DirectoryInfo CreateDirectory(string path);
        
        System.IO.DirectoryInfo CreateDirectory(string path, System.Security.AccessControl.DirectorySecurity directorySecurity);
        
        void Delete(string path);
        
        void Delete(string path, bool recursive);
        
        System.Collections.Generic.IEnumerable<string> EnumerateDirectories(string path);
        
        System.Collections.Generic.IEnumerable<string> EnumerateDirectories(string path, string searchPattern);
        
        System.Collections.Generic.IEnumerable<string> EnumerateDirectories(string path, string searchPattern, System.IO.SearchOption searchOption);
        
        System.Collections.Generic.IEnumerable<string> EnumerateFiles(string path);
        
        System.Collections.Generic.IEnumerable<string> EnumerateFiles(string path, string searchPattern);
        
        System.Collections.Generic.IEnumerable<string> EnumerateFiles(string path, string searchPattern, System.IO.SearchOption searchOption);
        
        System.Collections.Generic.IEnumerable<string> EnumerateFileSystemEntries(string path);
        
        System.Collections.Generic.IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern);
        
        System.Collections.Generic.IEnumerable<string> EnumerateFileSystemEntries(string path, string searchPattern, System.IO.SearchOption searchOption);
        
        bool Exists(string path);
        
        System.Security.AccessControl.DirectorySecurity GetAccessControl(string path);
        
        System.Security.AccessControl.DirectorySecurity GetAccessControl(string path, System.Security.AccessControl.AccessControlSections includeSections);
        
        System.DateTime GetCreationTime(string path);
        
        System.DateTime GetCreationTimeUtc(string path);
        
        string GetCurrentDirectory();
        
        string[] GetDirectories(string path);
        
        string[] GetDirectories(string path, string searchPattern);
        
        string[] GetDirectories(string path, string searchPattern, System.IO.SearchOption searchOption);
        
        string GetDirectoryRoot(string path);
        
        string[] GetFiles(string path);
        
        string[] GetFiles(string path, string searchPattern);
        
        string[] GetFiles(string path, string searchPattern, System.IO.SearchOption searchOption);
        
        string[] GetFileSystemEntries(string path);
        
        string[] GetFileSystemEntries(string path, string searchPattern);
        
        string[] GetFileSystemEntries(string path, string searchPattern, System.IO.SearchOption searchOption);
        
        System.DateTime GetLastAccessTime(string path);
        
        System.DateTime GetLastAccessTimeUtc(string path);
        
        System.DateTime GetLastWriteTime(string path);
        
        System.DateTime GetLastWriteTimeUtc(string path);
        
        string[] GetLogicalDrives();
        
        System.IO.DirectoryInfo GetParent(string path);
        
        void Move(string sourceDirName, string destDirName);
        
        void SetAccessControl(string path, System.Security.AccessControl.DirectorySecurity directorySecurity);
        
        void SetCreationTime(string path, System.DateTime creationTime);
        
        void SetCreationTimeUtc(string path, System.DateTime creationTimeUtc);
        
        void SetCurrentDirectory(string path);
        
        void SetLastAccessTime(string path, System.DateTime lastAccessTime);
        
        void SetLastAccessTimeUtc(string path, System.DateTime lastAccessTimeUtc);
        
        void SetLastWriteTime(string path, System.DateTime lastWriteTime);
        
        void SetLastWriteTimeUtc(string path, System.DateTime lastWriteTimeUtc);
    }
}