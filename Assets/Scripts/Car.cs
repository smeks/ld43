using UnityEngine;
using System.Collections;
using Assets.Scripts.Signals;
using UnityEngine.UI;
using Zenject;

public class Car : MonoBehaviour
{
    public GameObject GetInCarButtonPrefab;

    private GameObject _getInButton;
    private RectTransform _getInButtonRectTransform;
    private SignalBus _signalBus;

    [Inject]
    void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    // Update is called once per frame
    void Update()
    {
        if (_getInButton && _getInButtonRectTransform)
        {
            Vector3 locationScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            _getInButtonRectTransform.position = locationScreenPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _getInButton = Instantiate(GetInCarButtonPrefab, GameObject.Find("Game").transform);
        _getInButtonRectTransform = _getInButton.GetComponent<RectTransform>();

        _getInButton.GetComponent<Button>().onClick.AddListener(() =>
        {
            _signalBus.Fire(new GetInCarSignal());
            Destroy(_getInButton);
            Destroy(this.gameObject);
        });
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Destroy(_getInButton);
    }

    public class Factory : PlaceholderFactory<Car>
    {

    }
}
