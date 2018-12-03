using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Signals
{
    public class UpdateStatSignal
    {
        public StatType StatType{ get; }
        public float Value { get; }

        public UpdateStatSignal(StatType statType, float value)
        {
            StatType = statType;
            Value = value;
        }
    }
}
