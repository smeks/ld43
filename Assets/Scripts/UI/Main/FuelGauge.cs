using Assets.Scripts.Managers;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Main
{
    public class FuelGauge : MonoBehaviour
    {
        public StatType StatType;

        private SignalBus _signalBus;
        private Slider _slider;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<UpdateStatSignal>(OnStatUpdated);
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        private void OnStatUpdated(UpdateStatSignal statUpdated)
        {
            if (StatType == statUpdated.StatType)
            {
                _slider.value = statUpdated.Value;
            }
        }
    }
}
