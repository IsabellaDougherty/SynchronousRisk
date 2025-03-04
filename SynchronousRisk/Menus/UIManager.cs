using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.Menus
{
    public class UIManager
    {
        Func<String, UIManager> Func;

        String Display;

        bool Continue;

        public UIManager()
        {
            Continue = false;
        }
        public UIManager(string d, Func<String, UIManager> start)
        {
            Display = d;
            Func = start;
            Continue = true;
        }

        public UIManager Call(String inp)
        {
            return Func(inp);
        }

        public string GetDisplay()
        {
            return Display;
        }

        public bool CanContinue()
        {
            return Continue;
        }
    }
}
