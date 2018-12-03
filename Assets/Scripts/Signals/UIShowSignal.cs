using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Signals
{
    public class UIShowSignal
    {
        public UIPanelType PanelType { get; }

        public UIShowSignal(UIPanelType panelType)
        {
            PanelType = panelType;
        }
    }
}
