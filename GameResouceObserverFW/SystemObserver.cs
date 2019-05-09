using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;

namespace GameResouceManagerFW
{
    class SystemObserver
    {
        private const String catCpu = "Processor";
        private const String countCpu = "% Processor Time";
        private const String instanceName = "_Total";

        private const String catMem = "Memory";
        private const String countMem = "Available MBytes";

        System.Diagnostics.PerformanceCounter pcCpu;
        System.Diagnostics.PerformanceCounter pcMem;

        public Double usingProcesor { private set; get; } = 0.0;
        public Double usingMemory { private set; get; } = 0.0;

        public readonly Double totalVisibleMemorySize;


        public SystemObserver()
        {
            this.pcCpu = new PerformanceCounter(catCpu, countCpu, instanceName);
            this.pcMem = new PerformanceCounter(catMem, countMem);

            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            foreach (System.Management.ManagementObject mo in moc)
            {
                this.totalVisibleMemorySize = Convert.ToDouble(mo["TotalVisibleMemorySize"]) / 1024.0;
                mo.Dispose();
            }

            moc.Dispose();
            mc.Dispose();
        }

        public void Measurement()
        {
            while(true)
            {
                this.Update();
                Console.WriteLine($"{this.usingProcesor:#.##}%");
                Console.WriteLine($"{this.usingMemory:#.###}MB");
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Update()
        {
            this.usingProcesor = this.pcCpu.NextValue();
            Double tmp = this.totalVisibleMemorySize - this.pcMem.NextValue();
            this.usingMemory = tmp;
        }

        private void PrintManagementObject(System.Management.ManagementObject mo)
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
    }   
}
