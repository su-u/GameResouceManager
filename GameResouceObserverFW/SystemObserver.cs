using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Double Procesor { private set; get; }


        SystemObserver()
        {
            this.pcCpu = new System.Diagnostics.PerformanceCounter(catCpu, countCpu, instanceName);
            this.pcMem = new System.Diagnostics.PerformanceCounter(catMem, countMem);
        }

        void Main(string[] args)
        {
            //1秒おきに値を取得する
            for (int i = 0; i < 10; i++)
            {
                //計算された値を取得し、表示する
                Console.WriteLine(this.pcCpu.NextValue());
                Console.WriteLine(this.pcMem.NextValue());
                //1秒待機する
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void Update()
        {
            this.pcCpu.NextValue();
        }
    }   
}
