using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace GameResouceObserver
{
    class ProcessObserver
    {
        static void Main(string[] args)
        {
            //ローカルコンピュータ上で実行されているすべてのプロセスを取得
            System.Diagnostics.Process[] ps =
                System.Diagnostics.Process.GetProcesses();
            List<System.Diagnostics.Process> pslist = new List<System.Diagnostics.Process>(ps);

            //"machinename"という名前のコンピュータで実行されている
            //すべてのプロセスを取得するには次のようにする。
            //System.Diagnostics.Process[] ps =
            //    System.Diagnostics.Process.GetProcesses("machinename");

            String[] gameList = new String[] { "生き残れミドリムシ" };
            List<String> pNmaeList = new List<string>(gameList);



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
