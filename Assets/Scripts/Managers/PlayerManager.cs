using Assets.Scripts.Signals;
using Assets.Scripts.UI.Inventory;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Managers
{
    public enum StatType
    {
        Health,
        Water,
        Hunger,
        Sick,
        Car,
        Fuel
    }

    public class PlayerManager : IInitializable, ITickable
    {
        private readonly SignalBus _signalBus;
        public float Health = 1.0f;
        public float Water = 0.1f;
        public float Hunger = 0.9f;
        public float Sick = 0.4f;

        private bool _radiationDamage = false;

        public PlayerManager(SignalBus signalBus)
        {
            _signalBus = signalBus;
            signalBus.Subscribe<UseItemSignal>((signal) => OnUseItem(signal.Item));

            signalBus.Subscribe<EnterRadiationSignal>(() => { _radiationDamage = true; });

            signalBus.Subscribe<ExitRadiationSignal>(() => { _radiationDamage = false; });
        }

        public void OnUseItem(Item item)
        {
            if (item.ItemType == ItemType.Food && Hunger > 0.0f)
            {
                Hunger -= 0.25f;
                Hunger = Mathf.Clamp(Hunger, 0.0f, 1.0f);
                _signalBus.Fire(new UpdateStatSignal(StatType.Hunger, Hunger));
                Object.Destroy(item.gameObject);
                
                return;
            }

            if (item.ItemType == ItemType.Medicine && Sick > 0.0f)
            {
                Sick -= 0.25f;
                Sick = Mathf.Clamp(Sick, 0.0f, 1.0f);
                _signalBus.Fire(new UpdateStatSignal(StatType.Sick, Sick));
                Object.Destroy(item.gameObject);
                
                return;
            }

            if (item.ItemType == ItemType.Water && Water < 1.0f)
            {
                Water += 0.25f;
                Water = Mathf.Clamp(Water, 0.0f, 1.0f);
                _signalBus.Fire(new UpdateStatSignal(StatType.Water, Water));
                Object.Destroy(item.gameObject);
                return;
            }
        }

        public void Initialize()
        {
            _signalBus.Fire(new UpdateStatSignal(StatType.Hunger, Hunger));
            _signalBus.Fire(new UpdateStatSignal(StatType.Sick, Sick));
            _signalBus.Fire(new UpdateStatSignal(StatType.Water, Water));
        }

        public void Tick()
        {
            Water -= 0.005f * Time.deltaTime;
            Hunger += 0.002f * Time.deltaTime;
            Sick += 0.001f * Time.deltaTime;

            var waterDamage = Water <= 0.01f ? 1f : 0f;
            var hungerDamage = Hunger >= 0.99f ? 1f : 0f;
            var sickDamage = Sick >= 0.99f ? 1f : 0f;
            var radationDamage = _radiationDamage ? 3f : 0f;

            Health -= (waterDamage + hungerDamage + sickDamage + radationDamage) * 0.01f * Time.deltaTime;

            Water = Mathf.Clamp(Water, 0.0f, 1.0f);
            Sick = Mathf.Clamp(Sick, 0.0f, 1.0f);
            Hunger = Mathf.Clamp(Hunger, 0.0f, 1.0f);
            Health = Mathf.Clamp(Health, 0.0f, 1.0f);

            _signalBus.Fire(new UpdateStatSignal(StatType.Health, Health));
            _signalBus.Fire(new UpdateStatSignal(StatType.Hunger, Hunger));
            _signalBus.Fire(new UpdateStatSignal(StatType.Sick, Sick));
            _signalBus.Fire(new UpdateStatSignal(StatType.Water, Water));


            if (Health <= 0.0f)
            {
                _signalBus.Fire(new EndGameSignal(EndGameType.Lost));
            }
        }
    }
}
