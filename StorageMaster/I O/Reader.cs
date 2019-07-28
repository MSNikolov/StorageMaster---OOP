using StorageMaster.I_O.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace StorageMaster.I_O
{
    public class Reader : IReader
    {
        public string Read()
        {
            return Console.ReadLine();
        }
    }
}
