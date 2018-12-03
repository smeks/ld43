using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Signals;
using Assets.Scripts.UI.Inventory;
using UnityEngine.UI;
using Zenject;

public class Location : MonoBehaviour
{
    public GameObject LootUIParent;
    public GameObject LootButtonPrefab;
    public List<Item> Items;

    private SignalBus _signalBus;
    private GameObject _lootButton;
    private RectTransform _lootButtonTransform;

    [Inject]
    private void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    void Update()
    {
        if (_lootButton && _lootButtonTransform)
        {
            Vector3 locationScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            _lootButtonTransform.position = locationScreenPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _signalBus.Fire(new EnteredLocationSignal(Items));

        _lootButton = Instantiate(LootButtonPrefab, LootUIParent.transform);
        _lootButtonTransform = _lootButton.GetComponent<RectTransform>();
        _lootButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            
            _signalBus.Fire(new UIShowSignal(UIPanelType.Inventory));
            _signalBus.Fire(new UIShowSignal(UIPanelType.Loot));
        });
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _signalBus.Fire(new LeaveLocationSignal(this));
        _signalBus.Fire(new UIHideSignal(UIPanelType.Loot));

        Destroy(_lootButton);
        _lootButton = null;
        _lootButtonTransform = null;
    }
}
