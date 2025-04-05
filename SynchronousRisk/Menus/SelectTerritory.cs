using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.Menus
{
    internal class SelectTerritory : UIManager
    {
        private Func<Territory, UIManager> TerritoryFunction;
        public SelectTerritory(string d, Func<Territory, UIManager> TerrFunc)
        {
            Display = d;
            TerritoryFunction = TerrFunc;
            Continue = true;
        }

        public override UIManager InputTerritory(Territory terr)
        {
            return TerritoryFunction(terr);
        }

        public override UIManager Call(string inp)
        {
            Display = "sorry invalid action";
            return this;
        }

    }
}
