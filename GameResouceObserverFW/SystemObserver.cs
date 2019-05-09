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

        public Double usingProcesor { private set; get; }
        public Double usingMemory { private set; get; }

        public readonly Double totalVisibleMemorySize;


        public SystemObserver()
        {
            this.pcCpu = new PerformanceCounter(catCpu, countCpu, instanceName);
            this.pcMem = new PerformanceCounter(catMem, countMem);

            ManagementClass mc = new ManagementClass("Win32_OperatingSystem");
            System.Management.ManagementObjectCollection moc = mc.GetInstances();

            foreach (System.Management.ManagementObject mo in moc)
            {
                this.totalVisibleMemorySize = Convert.ToDouble(mo["TotalVisibleMemorySize"]);
                mo.Dispose();
            }

            moc.Dispose();
            mc.Dispose();
        }

        public void Measurement()
        {
            //1秒おきに値を取得する
            for (int i = 0; i < 10; i++)
            {
                //計算された値を取得し、表示する
                this.Update();
                Console.WriteLine($"{this.usingProcesor}%");
                Console.WriteLine($"{this.usingMemory}MB");
                //1秒待機する
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Update()
        {
            this.usingProcesor = this.pcCpu.NextValue();
            this.usingMemory = this.pcMem.NextValue();
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
