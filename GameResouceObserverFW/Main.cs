using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace GameResouceManagerFW
{
    class Run
    {

        static void Main(String[] args)
        {
            SystemObserver so = new SystemObserver();
            so.Measurement();
        }
        
    }
}
