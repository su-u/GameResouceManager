using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace GameResouceObserver
{
    class Observer
    {
        private Boolean isLoop = true;
        private SystemObserver systemObserver;
        private ProcessObserver processObserver;

        static void Main(String[] args)
        {
            
        }

        public Observer()
        {
            this.systemObserver = new SystemObserver();
            this.processObserver = new ProcessObserver();
        }

        public void Observe()
        {
            var _ = Task.Run(() =>
            {
                while (this.isLoop)
                {
                    try
                    {
                        this.
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("エラー: {0}", e.Message);
                    }
                }
            });
        }

    }
}
