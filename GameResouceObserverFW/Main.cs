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

            ManagementClass mc = new System.Management.ManagementClass("Win32_OperatingSystem");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            foreach (System.Management.ManagementObject mo in moc)
            {
                //合計物理メモリ
                Console.WriteLine("合計物理メモリ:{0:#.##}MB", Convert.ToDouble(mo["TotalVisibleMemorySize"]) / 1024.0);
                //利用可能な物理メモリ
                Console.WriteLine("利用可能物理メモリ:{0:#.##}MB", Convert.ToDouble(mo["FreePhysicalMemory"]) / 1024.0);
                //合計仮想メモリ
                Console.WriteLine("合計仮想メモリ:{0:#.##}MB", Convert.ToDouble(mo["TotalVirtualMemorySize"]) / 1024.0);
                //利用可能な仮想メモリ
                Console.WriteLine("利用可能仮想メモリ:{0:#.##}MB", Convert.ToDouble(mo["FreeVirtualMemory"]) / 1024.0);

                //他のページをスワップアウトせずにページングファイルにマップできるサイズ
                Console.WriteLine("FreeSpaceInPagingFiles:{0}KB", mo["FreeSpaceInPagingFiles"]);
                //ページングファイルに保存できる合計サイズ
                Console.WriteLine("SizeStoredInPagingFiles:{0}KB", mo["SizeStoredInPagingFiles"]);
                //スワップスペースの合計サイズ
                //スワップスペースとページングファイルが区別されていなければ、NULL
                Console.WriteLine("TotalSwapSpaceSize:{0}KB", mo["TotalSwapSpaceSize"]);

                mo.Dispose();
            }

            moc.Dispose();
            mc.Dispose();
        }
        
    }
}
