﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.Menus
{
    /* Russell Phillips
     * 3/6/2025
     * class that manages communication between UI and game logic
     */
    public class UIManager
    {
        internal Func<String, UIManager> Func;

        internal String Display;

        internal bool Continue;

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

        public virtual UIManager Call(String inp)
        {
            return Func(inp);
        }

        public virtual UIManager InputTerritory(Territory terr)
        {
            Display = "Sorry invalid action";
            return this;
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
