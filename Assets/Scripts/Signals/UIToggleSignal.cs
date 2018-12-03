using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Signals
{
    public class UIToggleSignal
    {
        public UIPanelType PanelType { get; }

        public UIToggleSignal(UIPanelType panelType)
        {
            PanelType = panelType;
        }
    }
}
