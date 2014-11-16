using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace UsingGrass
{
    public interface IFile
    {
        FileStream Create(string path);
    }
}
