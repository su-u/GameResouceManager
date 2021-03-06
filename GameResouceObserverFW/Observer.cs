﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Threading;

namespace GameResouceObserver
{
    class Observer
    {
        const Int32 TIME_INTERVAL = 5000;

        private SystemObserver systemObserver;
        private ProcessObserver processObserver;

        static void Main(String[] args)
        {
            Observer observer = new Observer();
            observer.Run();

        }

        public Observer()
        {
            this.systemObserver = new SystemObserver();
            this.processObserver = new ProcessObserver();
        }

        public void Run()
        {
            TimerCallback timerDelegate = new TimerCallback(Observe);
            Timer timer = new Timer(timerDelegate, null, 0, TIME_INTERVAL);

            Console.ReadKey();

            timer.Change(Timeout.Infinite, Timeout.Infinite);
            Console.WriteLine("タイマー停止");

            timer.Dispose();
        }

        private void Observe(Object o)
        {
            try
            {
                this.systemObserver.Update();
                String outS = $"{this.systemObserver.usingProcesor:0.00}, {this.systemObserver.usingMemory:0.000}\n";
                Writter.Write("system.csv", outS);
                Console.Write(outS);
            }
            catch (Exception e)
            {
                Console.WriteLine("エラー: {0}", e.Message);
            }

            try
            {
                this.processObserver.Update();

                List<System.Diagnostics.Process> gameProcessList = this.processObserver.GetGameProcess();
                String outS = $"{this.systemObserver.usingProcesor:0.00}, {this.systemObserver.usingMemory:0.00}\n";
                foreach (System.Diagnostics.Process p in gameProcessList)
                {
                    try
                    {
                        String outP = $"{this.processObserver.playingProcessCpu:0.00,0}, {p.WorkingSet64 / 1024.0 / 1024.0:0.00}\n";

                        Console.WriteLine($"CPU: {this.processObserver.playingProcessCpu}%");
                        Console.WriteLine($"プロセス名:{p.ProcessName}");
                        Console.WriteLine($"ID: {p.Id}");
                        Console.WriteLine("ファイル名: {0}", p.MainModule.FileName);
                        Console.WriteLine("合計プロセッサ時間: {0}", p.TotalProcessorTime);
                        Console.WriteLine("物理メモリ使用量: {0} MB", p.WorkingSet64 / 1024.0 / 1024.0);
                        Console.WriteLine();

                        Writter.Write($"{p.ProcessName}.csv", outP);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("エラー: {0}", ex.Message);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("エラー: {0}", e.Message);
            }
        }


    }
}
