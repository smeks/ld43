using Assets.Scripts.UI;
using Assets.Scripts.UI.Inventory;

namespace Assets.Scripts.Signals
{
    public class UseItemSignal
    {
        public Item Item { get; }

        public UseItemSignal(Item item)
        {
            Item = item;
        }
    }
}
