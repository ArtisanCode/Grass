'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.18444
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports Microsoft.Win32.SafeHandles
Imports System
Imports System.CodeDom.Compiler
Imports System.Collections.Generic
Imports System.IO
Imports System.Security.AccessControl


<GeneratedCode("0.0.0.1", "ArtisanCode.Grass")>  _
Partial Public Class DirectoryWrapper
    Inherits Object
    Implements IDirectory
    
    Public Overloads Overridable Function CreateDirectory(ByVal path As String) As System.IO.DirectoryInfo Implements IDirectory.CreateDirectory
        Return Directory.CreateDirectory(path)
    End Function
    
    Public Overloads Overridable Function CreateDirectory(ByVal path As String, ByVal directorySecurity As System.Security.AccessControl.DirectorySecurity) As System.IO.DirectoryInfo Implements IDirectory.CreateDirectory
        Return Directory.CreateDirectory(path, directorySecurity)
    End Function
    
    Public Overloads Overridable Sub Delete(ByVal path As String) Implements IDirectory.Delete
        Directory.Delete(path)
    End Sub
    
    Public Overloads Overridable Sub Delete(ByVal path As String, ByVal recursive As Boolean) Implements IDirectory.Delete
        Directory.Delete(path, recursive)
    End Sub
    
    Public Overloads Overridable Function EnumerateDirectories(ByVal path As String) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateDirectories
        Return Directory.EnumerateDirectories(path)
    End Function
    
    Public Overloads Overridable Function EnumerateDirectories(ByVal path As String, ByVal searchPattern As String) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateDirectories
        Return Directory.EnumerateDirectories(path, searchPattern)
    End Function
    
    Public Overloads Overridable Function EnumerateDirectories(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateDirectories
        Return Directory.EnumerateDirectories(path, searchPattern, searchOption)
    End Function
    
    Public Overloads Overridable Function EnumerateFiles(ByVal path As String) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateFiles
        Return Directory.EnumerateFiles(path)
    End Function
    
    Public Overloads Overridable Function EnumerateFiles(ByVal path As String, ByVal searchPattern As String) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateFiles
        Return Directory.EnumerateFiles(path, searchPattern)
    End Function
    
    Public Overloads Overridable Function EnumerateFiles(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateFiles
        Return Directory.EnumerateFiles(path, searchPattern, searchOption)
    End Function
    
    Public Overloads Overridable Function EnumerateFileSystemEntries(ByVal path As String) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateFileSystemEntries
        Return Directory.EnumerateFileSystemEntries(path)
    End Function
    
    Public Overloads Overridable Function EnumerateFileSystemEntries(ByVal path As String, ByVal searchPattern As String) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateFileSystemEntries
        Return Directory.EnumerateFileSystemEntries(path, searchPattern)
    End Function
    
    Public Overloads Overridable Function EnumerateFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As System.Collections.Generic.IEnumerable(Of String) Implements IDirectory.EnumerateFileSystemEntries
        Return Directory.EnumerateFileSystemEntries(path, searchPattern, searchOption)
    End Function
    
    Public Overridable Function Exists(ByVal path As String) As Boolean Implements IDirectory.Exists
        Return Directory.Exists(path)
    End Function
    
    Public Overloads Overridable Function GetAccessControl(ByVal path As String) As System.Security.AccessControl.DirectorySecurity Implements IDirectory.GetAccessControl
        Return Directory.GetAccessControl(path)
    End Function
    
    Public Overloads Overridable Function GetAccessControl(ByVal path As String, ByVal includeSections As System.Security.AccessControl.AccessControlSections) As System.Security.AccessControl.DirectorySecurity Implements IDirectory.GetAccessControl
        Return Directory.GetAccessControl(path, includeSections)
    End Function
    
    Public Overridable Function GetCreationTime(ByVal path As String) As Date Implements IDirectory.GetCreationTime
        Return Directory.GetCreationTime(path)
    End Function
    
    Public Overridable Function GetCreationTimeUtc(ByVal path As String) As Date Implements IDirectory.GetCreationTimeUtc
        Return Directory.GetCreationTimeUtc(path)
    End Function
    
    Public Overridable Function GetCurrentDirectory() As String Implements IDirectory.GetCurrentDirectory
        Return Directory.GetCurrentDirectory
    End Function
    
    Public Overloads Overridable Function GetDirectories(ByVal path As String) As String() Implements IDirectory.GetDirectories
        Return Directory.GetDirectories(path)
    End Function
    
    Public Overloads Overridable Function GetDirectories(ByVal path As String, ByVal searchPattern As String) As String() Implements IDirectory.GetDirectories
        Return Directory.GetDirectories(path, searchPattern)
    End Function
    
    Public Overloads Overridable Function GetDirectories(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As String() Implements IDirectory.GetDirectories
        Return Directory.GetDirectories(path, searchPattern, searchOption)
    End Function
    
    Public Overridable Function GetDirectoryRoot(ByVal path As String) As String Implements IDirectory.GetDirectoryRoot
        Return Directory.GetDirectoryRoot(path)
    End Function
    
    Public Overloads Overridable Function GetFiles(ByVal path As String) As String() Implements IDirectory.GetFiles
        Return Directory.GetFiles(path)
    End Function
    
    Public Overloads Overridable Function GetFiles(ByVal path As String, ByVal searchPattern As String) As String() Implements IDirectory.GetFiles
        Return Directory.GetFiles(path, searchPattern)
    End Function
    
    Public Overloads Overridable Function GetFiles(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As String() Implements IDirectory.GetFiles
        Return Directory.GetFiles(path, searchPattern, searchOption)
    End Function
    
    Public Overloads Overridable Function GetFileSystemEntries(ByVal path As String) As String() Implements IDirectory.GetFileSystemEntries
        Return Directory.GetFileSystemEntries(path)
    End Function
    
    Public Overloads Overridable Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String) As String() Implements IDirectory.GetFileSystemEntries
        Return Directory.GetFileSystemEntries(path, searchPattern)
    End Function
    
    Public Overloads Overridable Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As String() Implements IDirectory.GetFileSystemEntries
        Return Directory.GetFileSystemEntries(path, searchPattern, searchOption)
    End Function
    
    Public Overridable Function GetLastAccessTime(ByVal path As String) As Date Implements IDirectory.GetLastAccessTime
        Return Directory.GetLastAccessTime(path)
    End Function
    
    Public Overridable Function GetLastAccessTimeUtc(ByVal path As String) As Date Implements IDirectory.GetLastAccessTimeUtc
        Return Directory.GetLastAccessTimeUtc(path)
    End Function
    
    Public Overridable Function GetLastWriteTime(ByVal path As String) As Date Implements IDirectory.GetLastWriteTime
        Return Directory.GetLastWriteTime(path)
    End Function
    
    Public Overridable Function GetLastWriteTimeUtc(ByVal path As String) As Date Implements IDirectory.GetLastWriteTimeUtc
        Return Directory.GetLastWriteTimeUtc(path)
    End Function
    
    Public Overridable Function GetLogicalDrives() As String() Implements IDirectory.GetLogicalDrives
        Return Directory.GetLogicalDrives
    End Function
    
    Public Overridable Function GetParent(ByVal path As String) As System.IO.DirectoryInfo Implements IDirectory.GetParent
        Return Directory.GetParent(path)
    End Function
    
    Public Overridable Sub Move(ByVal sourceDirName As String, ByVal destDirName As String) Implements IDirectory.Move
        Directory.Move(sourceDirName, destDirName)
    End Sub
    
    Public Overridable Sub SetAccessControl(ByVal path As String, ByVal directorySecurity As System.Security.AccessControl.DirectorySecurity) Implements IDirectory.SetAccessControl
        Directory.SetAccessControl(path, directorySecurity)
    End Sub
    
    Public Overridable Sub SetCreationTime(ByVal path As String, ByVal creationTime As Date) Implements IDirectory.SetCreationTime
        Directory.SetCreationTime(path, creationTime)
    End Sub
    
    Public Overridable Sub SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date) Implements IDirectory.SetCreationTimeUtc
        Directory.SetCreationTimeUtc(path, creationTimeUtc)
    End Sub
    
    Public Overridable Sub SetCurrentDirectory(ByVal path As String) Implements IDirectory.SetCurrentDirectory
        Directory.SetCurrentDirectory(path)
    End Sub
    
    Public Overridable Sub SetLastAccessTime(ByVal path As String, ByVal lastAccessTime As Date) Implements IDirectory.SetLastAccessTime
        Directory.SetLastAccessTime(path, lastAccessTime)
    End Sub
    
    Public Overridable Sub SetLastAccessTimeUtc(ByVal path As String, ByVal lastAccessTimeUtc As Date) Implements IDirectory.SetLastAccessTimeUtc
        Directory.SetLastAccessTimeUtc(path, lastAccessTimeUtc)
    End Sub
    
    Public Overridable Sub SetLastWriteTime(ByVal path As String, ByVal lastWriteTime As Date) Implements IDirectory.SetLastWriteTime
        Directory.SetLastWriteTime(path, lastWriteTime)
    End Sub
    
    Public Overridable Sub SetLastWriteTimeUtc(ByVal path As String, ByVal lastWriteTimeUtc As Date) Implements IDirectory.SetLastWriteTimeUtc
        Directory.SetLastWriteTimeUtc(path, lastWriteTimeUtc)
    End Sub
End Class
