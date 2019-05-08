using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace GameResouceObserver
{
    class ProcessObserver
    {
        private List<System.Diagnostics.Process> pslist;
        private List<String> pNmaeList;

        private String[] gameList = new String[] { "生き残れミドリムシ" };

        ProcessObserver() {
            pslist = new List<System.Diagnostics.Process>(System.Diagnostics.Process.GetProcesses());
            pNmaeList = new List<string>(gameList);
        }

        public void WriteGameProcess(){
            List<System.Diagnostics.Process> gameProcessList = pslist.FindAll(n => pNmaeList.Any(p => p == n.ProcessName));

            //while (true)
            //{
            //配列から1つずつ取り出す
            foreach (System.Diagnostics.Process p in gameProcessList)
            {
                try
                {
                    //プロセス名を出力する
                    Console.WriteLine($"プロセス名:{p.ProcessName}");
                    //ID
                    Console.WriteLine($"ID: {p.Id}");
                    //メインモジュールのパス
                    Console.WriteLine("ファイル名: {0}", p.MainModule.FileName);
                    //合計プロセッサ時間
                    Console.WriteLine("合計プロセッサ時間: {0}", p.TotalProcessorTime);
                    //物理メモリ使用量
                    Console.WriteLine("物理メモリ使用量: {0} MB", p.WorkingSet64 / 1024.0 / 1024.0);
                    //.NET Framework 1.1以前では次のようにする
                    //Console.WriteLine("物理メモリ使用量: {0}", p.WorkingSet);

                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("エラー: {0}", ex.Message);
                }
            }
        }
    }
}
