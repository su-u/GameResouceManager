using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Diagnostics;
using System.IO;

namespace GameResouceObserver
{
    class ProcessObserver
    {
        private List<System.Diagnostics.Process> pslist;
        private List<String> pNmaeList = null;
        private List<System.Diagnostics.Process> gameProcessList;

        private const String catCpu = "Process";
        private const String countCpu = "% Processor Time";

        private PerformanceCounter playingProcess;
        private String playingProcessName = "";
        public Double playingProcessCpu { private set; get; }

        private const String CSV_LIST = "ProcessList.csv";

        public ProcessObserver() {
            this.pslist = new List<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcesses());
            this.pNmaeList = new List<string>();

            try { 
                var utf8_encoding = new System.Text.UTF8Encoding(false);
                using (StreamReader sr = new StreamReader(CSV_LIST, utf8_encoding))
                {
                    while (!sr.EndOfStream)
                    {
                        String line = sr.ReadLine();
                        this.pNmaeList.Add(line);
                     }
                }
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.Message);
            }

            if (this.pNmaeList != null)
            {
                foreach (var i in this.pNmaeList)
                {
                    Console.WriteLine(i.ToString());
                }
            }


        }

        public void Update()
        {
            pslist = new List<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcesses());
            this.gameProcessList = pslist.FindAll(n => pNmaeList.Any(p => p == n.ProcessName));
            if (this.gameProcessList != null)
            {
                foreach (var i in this.gameProcessList)
                {
                    Console.WriteLine(i.ToString());
                }
            }
            this.ProcessUpdate();
        }

        public List<System.Diagnostics.Process> GetGameProcess(){
            return this.gameProcessList;
        }

        private void ProcessUpdate()
        {
            if (gameProcessList.Count > 0)
            {
                this.playingProcessName = gameProcessList[0].ProcessName;
                if (this.playingProcess == null)
                {
                    this.playingProcess = new PerformanceCounter(catCpu, countCpu, this.playingProcessName, ".");
                }
                else
                {
                    this.playingProcessCpu = this.playingProcess.NextValue();
                }
            }
            else
            {
                this.playingProcessName = "";
                this.playingProcess = null;
            }
        }
    }
}
