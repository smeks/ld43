using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.UI.Inventory;

namespace Assets.Scripts.Signals
{
    public enum DirectionType
    {
        West,
        East
    }

    public class ChangeDirectionSignal
    {
        public  DirectionType Direction;

        public ChangeDirectionSignal(DirectionType type)
        {
            Direction = type;
        }
    }
}
