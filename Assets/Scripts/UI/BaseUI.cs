using Assets.Scripts.Signals;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class BaseUI : MonoBehaviour
    {
        public UIPanelType PanelType;

        public virtual void Toggle()
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        public virtual void Show()
        {
            gameObject.SetActive(true);
        }
    }
}
