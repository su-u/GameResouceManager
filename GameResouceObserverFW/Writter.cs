using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameResouceManagerFW
{
    class Writter
    {
        static void Write(String text, List<System.Diagnostics.Process> ps)
        {
            try
            {
                // appendをtrueにすると，既存のファイルに追記
                // falseにすると，ファイルを新規作成する
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
