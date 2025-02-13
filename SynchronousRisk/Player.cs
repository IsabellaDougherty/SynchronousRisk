using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    internal class Player
    {
        Hand PlayerHand = new Hand();

        List<Territory> OwnedTerritories { get; set; } = new List<Territory>();
        public Player() { }
    }
}
