using SynchronousRisk.PhaseProcessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.Menus
{
    internal class SelectNumber : UIManager
    {
        public int Max;
        public int Min;
        Func<int, UIManager> IntFunc;
        public SelectNumber(string d, Func<int, UIManager> Func, int mi, int ma)
        {
            Display = d;
            Min = mi;
            Max = ma;
            IntFunc = Func;
            Continue = true;
        }

        public override UIManager InputInt(int i)
        {
            return IntFunc(i);
        }

        public override UIManager Call(string inp)
        {
            Display = "sorry invalid action";
            return this;
        }
    }
}
