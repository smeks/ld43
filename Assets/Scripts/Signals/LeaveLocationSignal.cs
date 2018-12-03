using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UI.Inventory;

namespace Assets.Scripts.Signals
{
    public class LeaveLocationSignal
    {
        public Location Location { get; }

        public LeaveLocationSignal(Location location)
        {
            Location = location;
        }
    }
}
