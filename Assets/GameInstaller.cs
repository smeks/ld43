using Assets.Scripts;
using Assets.Scripts.Managers;
using Assets.Scripts.Signals;
using Assets.Scripts.UI.Inventory;
using UnityEngine;
using Zenject;

namespace Assets
{
    public class GameInstaller : MonoInstaller
    {
        public Transform RootContainer;


        public GameObject CarPrefab;

        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);

            Container.BindInterfacesAndSelfTo<GameManager>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerManager>().AsSingle().NonLazy();

            Container.DeclareSignal<UseItemSignal>();
            Container.DeclareSignal<UpdateStatSignal>();
            Container.DeclareSignal<UIToggleSignal>();
            Container.DeclareSignal<UIHideSignal>();
            Container.DeclareSignal<UIShowSignal>();
            Container.DeclareSignal<EnteredLocationSignal>();
            Container.DeclareSignal<LeaveLocationSignal>();
            Container.DeclareSignal<ChangeDirectionSignal>();

            Container.DeclareSignal<GetInCarSignal>();
            Container.DeclareSignal<GetOutCarSignal>();

            Container.DeclareSignal<EnterRadiationSignal>();
            Container.DeclareSignal<ExitRadiationSignal>();

            Container.DeclareSignal<EndGameSignal>();

            Container.BindFactory<Car, Car.Factory>()
                .FromComponentInNewPrefab(CarPrefab)
                .WithGameObjectName("Car");


            Container.BindFactory<Item, Item.Factory>()
                .FromComponentInNewPrefab(CarPrefab)
                .WithGameObjectName("Car");
        }
    }
}