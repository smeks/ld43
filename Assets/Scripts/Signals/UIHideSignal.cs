using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Signals
{
    public class UIHideSignal
    {
        public UIPanelType PanelType { get; }

        public UIHideSignal(UIPanelType panelType)
        {
            PanelType = panelType;
        }
    }
}
