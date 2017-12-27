using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1OlreansTestDemo_Interfaces
{
    public interface ITest:Orleans.IGrainWithIntegerKey
    {
        Task AddCount(string taskName);        
    }
}
