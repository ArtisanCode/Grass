using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UsingGrass
{
    public partial class FileWrapper: IFile
    {
        public FileStream Create(string path)
        {
            return File.Create(path);
        }
    }
}
