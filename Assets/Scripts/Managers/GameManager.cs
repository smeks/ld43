using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Scripts.Managers
{
    public class GameManager : IInitializable
    {
        private readonly SignalBus _signalBus;

        private Button _goWestButton;
        private Button _goEastButton;

        public GameManager(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _goWestButton = GameObject.Find("GoWestButton").GetComponent<Button>();
            _goEastButton = GameObject.Find("GoEastButton").GetComponent<Button>();


            _goWestButton.onClick.AddListener(() =>
            {
                signalBus.Fire(new ChangeDirectionSignal(DirectionType.West));
            });

            _goEastButton.onClick.AddListener(() =>
            {
                signalBus.Fire(new ChangeDirectionSignal(DirectionType.East));
            });
        }

        public void Initialize()
        {
            //SignalBus.Fire(new UIHideSignal(UIPanelType.CarLoot));
            //SignalBus.Fire(new UIHideSignal(UIPanelType.Loot));
            _signalBus.Fire(new UIHideSignal(UIPanelType.Inventory));
        }
    }
}
