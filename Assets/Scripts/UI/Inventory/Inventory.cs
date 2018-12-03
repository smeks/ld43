using UnityEngine;
using System.Collections;
using Assets.Scripts.Signals;
using Assets.Scripts.UI;
using Zenject;

public class Inventory : BaseUI
{
    [Inject]
    public void Init(SignalBus signalBus)
    {
        signalBus.Subscribe<UIToggleSignal>((signal) =>
        {
            if(signal.PanelType == PanelType)
                Toggle();
        });

        signalBus.Subscribe<UIHideSignal>((signal) =>
        {
            if (signal.PanelType == PanelType)
                Hide();
        });


        signalBus.Subscribe<UIShowSignal>((signal) =>
        {
            if (signal.PanelType == PanelType)
                Show();
        });

        signalBus.Subscribe<GetInCarSignal>(() =>
        {
            if (PanelType == UIPanelType.CarLoot)
                Show();
        });

        signalBus.Subscribe<GetOutCarSignal>(() =>
        {
            if (PanelType == UIPanelType.CarLoot)
                Hide();
        });
    }
}
