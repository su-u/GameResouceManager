using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace GameResouceManagerFW
{
    class Observer
    {
        

        static void Main(String[] args)
        {
            SystemObserver so = new SystemObserver();
            so.Measurement();
        }

        public void Communicate()
        {
            _communicateLoop = true;

            var _ = Task.Run(() =>
            {
                while (_communicateLoop)
                {
                    try
                    {
                        // クライアントとの送信・受信処理
                    }
                    catch (Exception e)
                    {
                        Debug.LogWarning("[Server] " + e);
                    }
                }
            });
        }

    }
}
