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
Public Interface IDirectory
    
    Function CreateDirectory(ByVal path As String) As System.IO.DirectoryInfo
    
    Function CreateDirectory(ByVal path As String, ByVal directorySecurity As System.Security.AccessControl.DirectorySecurity) As System.IO.DirectoryInfo
    
    Sub Delete(ByVal path As String)
    
    Sub Delete(ByVal path As String, ByVal recursive As Boolean)
    
    Function EnumerateDirectories(ByVal path As String) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateDirectories(ByVal path As String, ByVal searchPattern As String) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateDirectories(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateFiles(ByVal path As String) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateFiles(ByVal path As String, ByVal searchPattern As String) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateFiles(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateFileSystemEntries(ByVal path As String) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateFileSystemEntries(ByVal path As String, ByVal searchPattern As String) As System.Collections.Generic.IEnumerable(Of String)
    
    Function EnumerateFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As System.Collections.Generic.IEnumerable(Of String)
    
    Function Exists(ByVal path As String) As Boolean
    
    Function GetAccessControl(ByVal path As String) As System.Security.AccessControl.DirectorySecurity
    
    Function GetAccessControl(ByVal path As String, ByVal includeSections As System.Security.AccessControl.AccessControlSections) As System.Security.AccessControl.DirectorySecurity
    
    Function GetCreationTime(ByVal path As String) As Date
    
    Function GetCreationTimeUtc(ByVal path As String) As Date
    
    Function GetCurrentDirectory() As String
    
    Function GetDirectories(ByVal path As String) As String()
    
    Function GetDirectories(ByVal path As String, ByVal searchPattern As String) As String()
    
    Function GetDirectories(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As String()
    
    Function GetDirectoryRoot(ByVal path As String) As String
    
    Function GetFiles(ByVal path As String) As String()
    
    Function GetFiles(ByVal path As String, ByVal searchPattern As String) As String()
    
    Function GetFiles(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As String()
    
    Function GetFileSystemEntries(ByVal path As String) As String()
    
    Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String) As String()
    
    Function GetFileSystemEntries(ByVal path As String, ByVal searchPattern As String, ByVal searchOption As System.IO.SearchOption) As String()
    
    Function GetLastAccessTime(ByVal path As String) As Date
    
    Function GetLastAccessTimeUtc(ByVal path As String) As Date
    
    Function GetLastWriteTime(ByVal path As String) As Date
    
    Function GetLastWriteTimeUtc(ByVal path As String) As Date
    
    Function GetLogicalDrives() As String()
    
    Function GetParent(ByVal path As String) As System.IO.DirectoryInfo
    
    Sub Move(ByVal sourceDirName As String, ByVal destDirName As String)
    
    Sub SetAccessControl(ByVal path As String, ByVal directorySecurity As System.Security.AccessControl.DirectorySecurity)
    
    Sub SetCreationTime(ByVal path As String, ByVal creationTime As Date)
    
    Sub SetCreationTimeUtc(ByVal path As String, ByVal creationTimeUtc As Date)
    
    Sub SetCurrentDirectory(ByVal path As String)
    
    Sub SetLastAccessTime(ByVal path As String, ByVal lastAccessTime As Date)
    
    Sub SetLastAccessTimeUtc(ByVal path As String, ByVal lastAccessTimeUtc As Date)
    
    Sub SetLastWriteTime(ByVal path As String, ByVal lastWriteTime As Date)
    
    Sub SetLastWriteTimeUtc(ByVal path As String, ByVal lastWriteTimeUtc As Date)
End Interface
