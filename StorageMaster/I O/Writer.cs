using StorageMaster.I_O.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.I_O
{
    public class Writer : IWriter
    {
        public void Write(string args)
        {
            Console.WriteLine(args);
        }
    }
}
