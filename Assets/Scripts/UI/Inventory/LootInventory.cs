using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI.Inventory
{
    public class LootInventory : BaseUI
    {
        public List<Item> AllItemPrefabs;

        private SignalBus _signalBus;
        [Inject]
        public void Init(SignalBus signalBus)
        {
            _signalBus = signalBus;
            signalBus.Subscribe<UIToggleSignal>((signal) =>
            {
                if (signal.PanelType == PanelType)
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

            signalBus.Subscribe<EnteredLocationSignal>((signal) =>
            {
                ClearSlots();
                AddLocationItems(signal.Items);
            });

            signalBus.Subscribe<LeaveLocationSignal>((signal) =>
            {
                var itemsToAdd = new List<Item>();

                var itemsRemaining = GetComponentsInChildren<Item>();

                var foodCount = itemsRemaining.Count(x => x.ItemType == ItemType.Food);
                var waterCount = itemsRemaining.Count(x => x.ItemType == ItemType.Water);
                var fuelCount = itemsRemaining.Count(x => x.ItemType == ItemType.FuelCan);
                var medCount = itemsRemaining.Count(x => x.ItemType == ItemType.Medicine);
                var partCount = itemsRemaining.Count(x => x.ItemType == ItemType.SparePart);

                for (int i = 0; i < foodCount; i++)
                {
                    var foodPrefab = AllItemPrefabs.FirstOrDefault(x => x.ItemType == ItemType.Food);
                    itemsToAdd.Add(foodPrefab);    
                }
                for (int i = 0; i < waterCount; i++)
                {
                    var prefab = AllItemPrefabs.FirstOrDefault(x => x.ItemType == ItemType.Water);
                    itemsToAdd.Add(prefab);
                }
                for (int i = 0; i < fuelCount; i++)
                {
                    var prefab = AllItemPrefabs.FirstOrDefault(x => x.ItemType == ItemType.FuelCan);
                    itemsToAdd.Add(prefab);
                }
                for (int i = 0; i < medCount; i++)
                {
                    var prefab = AllItemPrefabs.FirstOrDefault(x => x.ItemType == ItemType.Medicine);
                    itemsToAdd.Add(prefab);
                }
                for (int i = 0; i < partCount; i++)
                {
                    var prefab = AllItemPrefabs.FirstOrDefault(x => x.ItemType == ItemType.SparePart);
                    itemsToAdd.Add(prefab);
                }

                signal.Location.Items = itemsToAdd;

            });
        }

        private void ClearSlots()
        {
            var slots = GetComponentsInChildren<Slot>();

            foreach (var slot in slots)
            {
                foreach (Transform child in slot.transform)
                {
                    child.SetParent(null);
                }
            }
        }

        private void AddLocationItems(List<Item> items)
        {
            var slots = GetComponentsInChildren<Slot>();
            int slotIndex = 0;

            foreach (var item in items)
            {
                var newItem = Instantiate(item);
                newItem.GetComponent<Item>().SignalBus = _signalBus;

                newItem.transform.SetParent(slots[slotIndex].transform);
                newItem.transform.localScale = Vector3.one;
                slotIndex++;
            }
        }
    }
}
