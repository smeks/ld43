using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UI.Inventory;

namespace Assets.Scripts.Signals
{
    public class EnteredLocationSignal
    {
        public List<Item> Items { get; }

        public EnteredLocationSignal(List<Item> items)
        {
            Items = items;
        }
    }
}
