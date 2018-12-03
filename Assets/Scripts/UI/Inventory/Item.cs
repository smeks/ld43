using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Inventory
{
    public enum ItemType
    {
        Food,
        Water,
        Medicine,
        FuelCan,
        SparePart
    }

    public class Item : MonoBehaviour
    {
        public ItemType ItemType;
        public float Amount;
        public SignalBus SignalBus;

        private Image _image;
        private Button _useButton;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            SignalBus = signalBus;
        }

        public void Awake()
        {
            _image = GetComponent<Image>();
            _useButton = GetComponentInChildren<Button>();
            _useButton.onClick.AddListener(OnUseItem);
        }

        public void OnUseItem()
        {
            SignalBus.Fire(new UseItemSignal(this));
        }

        public class Factory : PlaceholderFactory<Item>
        {

        }
    }
}
