using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI
{
    public class GameUI : MonoBehaviour
    {
        private SignalBus _signalBus;
        private Button _toggleInventoryButton;
        private Button _getOutCarButton;


        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _toggleInventoryButton = GameObject.Find("InventoryToggleButton").GetComponent<Button>();
            _toggleInventoryButton.onClick.AddListener(() =>
            {
                _signalBus.Fire(new UIToggleSignal(UIPanelType.Inventory));
            });


            _getOutCarButton = GameObject.Find("GetOutCarButton").GetComponent<Button>();
            _getOutCarButton.onClick.AddListener(() =>
            {
                _getOutCarButton.gameObject.SetActive(false);
                Debug.Log("Get Out signal fired");
                _signalBus.Fire(new GetOutCarSignal());
                
            });

            _signalBus.Subscribe<GetInCarSignal>((signal) =>
            {
                _getOutCarButton.gameObject.SetActive(true);
            });
        }
    }
}
