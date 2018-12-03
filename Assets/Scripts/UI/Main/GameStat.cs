using Assets.Scripts.Managers;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.UI.Main
{
    public class GameStat : MonoBehaviour
    {
        public StatType StatType;

        private SignalBus _signalBus;
        private Text _text;

        [Inject]
        private void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<UpdateStatSignal>(OnStatUpdated);
        }

        private void Awake()
        {
            _text = GetComponentsInChildren<Text>()[1];
        }

        private void OnStatUpdated(UpdateStatSignal statUpdated)
        {
            if (StatType == statUpdated.StatType)
            {
                _text.text = $"{(int)(statUpdated.Value * 100)}";
            }
        }
    }
}
