using SynchronousRisk.PhaseProcessing;
using System;
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

        internal Phase NextPhase;

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// Blank UIManager, signaling end of a turn
        /// </summary>
        public UIManager()
        {
            Continue = true;
        }

        public UIManager(string d, bool cont)
        {
            Display = d;
            Continue = cont;
        }

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// UImanager expecting a string input (should stay unused)
        /// </summary>
        public UIManager(string d, Func<String, UIManager> start)
        {
            Display = d;
            Func = start;
            Continue = false;
        }

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// String input
        /// </summary>
        public virtual UIManager Call(String inp)
        {
            return Func(inp);
        }

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// Virtual method for inputting a territory
        /// </summary>
        /// <param name="terr">territory to input</param>
        /// <returns>next UIManager</returns>
        public virtual UIManager InputTerritory(Territory terr)
        {
            Display = "Sorry invalid action";
            return this;
        }

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// Virtual method for inputting an integer
        /// </summary>
        /// <param name="i">integer to input</param>
        /// <returns>next UImanager</returns>
        public virtual UIManager InputInt(int i)
        {
            Display = "Sorry invalid action";
            return this;
        }

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// Gets the next phase manager if possible
        /// </summary>
        /// <returns>new UIManager</returns>
        public virtual UIManager NextPhaseManager()
        {
            return this;
        }

        /// Russell Phillips 3/6/2025
        /// <summary>
        /// Gets the string to display to the user
        /// </summary>
        /// <returns>string to display</returns>
        public string GetDisplay()
        {
            return Display;
        }
    }
}
