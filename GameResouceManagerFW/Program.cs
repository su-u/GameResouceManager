using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace GameResouceManager
{
    class Program
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
            //    System.Threading.Thread.Sleep(1000);
            //}
            Write("", gameProcessList);

            //コンピュータ名
            //"."はローカルコンピュータを表す
            //コンピュータ名は省略可能（省略時は"."）
            string machineName = ".";
            //カテゴリ名
            string categoryName = "Processor";
            //カウンタ名
            string counterName = "% Processor Time";
            //インスタンス名
            string instanceName = "_Total";

            //カテゴリが存在するか確かめる
            if (!System.Diagnostics.PerformanceCounterCategory.Exists(
                categoryName, machineName))
            {
                Console.WriteLine("登録されていないカテゴリです。");
                return;
            }

            //カウンタが存在するか確かめる
            if (!System.Diagnostics.PerformanceCounterCategory.CounterExists(
                counterName, categoryName, machineName))
            {
                Console.WriteLine("登録されていないカウンタです。");
                return;
            }

            //PerformanceCounterオブジェクトの作成
            System.Diagnostics.PerformanceCounter pc =
                new System.Diagnostics.PerformanceCounter(
                categoryName, counterName, instanceName, machineName);

            //1秒おきに値を取得する
            for (int i = 0; i < 10; i++)
            {
                //計算された値を取得し、表示する
                Console.WriteLine(pc.NextValue());
                //1秒待機する
                System.Threading.Thread.Sleep(1000);
            }

        }

        static void Write(String text, List<System.Diagnostics.Process> ps)
        {
            try
            {
                // appendをtrueにすると，既存のファイルに追記
                //         falseにすると，ファイルを新規作成する
                var append = true;
                // 出力用のファイルを開く
                var utf8_encoding = new System.Text.UTF8Encoding(false);
                using (var sw = new System.IO.StreamWriter(@"test.csv", append, System.Text.Encoding.UTF8))
                {
                    foreach (System.Diagnostics.Process p in ps)
                    {
                        sw.Write($"{p.ProcessName},{p.Id},{p.WorkingSet64}\n");
                    }
                }
            }
            catch (System.Exception e)
            {
                // ファイルを開くのに失敗したときエラーメッセージを表示
                System.Console.WriteLine(e.Message);
            }
        }
    }
}
