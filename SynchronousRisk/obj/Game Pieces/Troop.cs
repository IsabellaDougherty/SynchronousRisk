using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    //IAD 2/19/2025
    /// <summary>
    /// Class to manipulate troop objects and store information regarding them.
    /// Note the following key: 
    /// -1 indicates a game has yet to start in the location of this troop
    /// 0 indicates all troops have been defeated in the location of this troop
    /// Any other number above 0 indicates the number of troops present in the location of this troop
    /// Any other number below -1 indicates an error in the number of troops present in the location of this troop
    /// </summary>
    public class Troops
    {
        private int totalTroops;
        public Troops(int total) { totalTroops = total; }
        public int getTroops() { return totalTroops; }
        public void setTroops(int newTotal) { totalTroops = newTotal; }
    }
}
