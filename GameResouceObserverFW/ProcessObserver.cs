using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Diagnostics;

namespace GameResouceObserver
{
    class ProcessObserver
    {
        private List<System.Diagnostics.Process> pslist;
        private List<String> pNmaeList;
        private List<System.Diagnostics.Process> gameProcessList;

        private String[] gameList = new String[] {
            "Doukyonin",
            "monochrom",
            "VRAUN",
            "BRAUN",
            "SofumeRemix",
            "BalloonGhost",
            "Cat_Ch",
            "CraneGame",
            "MEMORY_NOTES_interlude",
            "SushiVR_Build20190426",
            "3M",
            "生き残れミドリムシ",
            "sunset_of_ironrust",
            "MINEUCHI",
            "main",
            "Yuru",
            "Puzzle",
            "EscapeBox",
            "CubeLabyrinth_190502",
            "Bob\'s Music School",
            "蚊スタムキャスト",
            "WS_arakan_1",
            "yagiyagi",
            "bomber!!!!!",
            "おたキジ" };


        private const String catCpu = "Process";
        private const String countCpu = "% Processor Time";

        private PerformanceCounter playingProcess;
        private String playingProcessName = "";
        public Double playingProcessCpu { private set; get; }

        public ProcessObserver() {
            pslist = new List<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcesses());
            pNmaeList = new List<string>(gameList);
        }

        public void Update()
        {
            pslist = new List<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcesses());
            this.gameProcessList = pslist.FindAll(n => pNmaeList.Any(p => p == n.ProcessName));
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
