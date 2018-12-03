using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Signals;
using UnityEngine;
using Zenject;

public class Radiation : MonoBehaviour
{
    private Vector3 _radiationScale = new Vector3(0.1523406f, 0.1523406f, 1.0f);

    private SignalBus _signalBus;

    [Inject]
    void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
	
	// Update is called once per frame
	void Update ()
	{
	    _radiationScale += (Vector3.one * 0.03f) * Time.deltaTime;

        transform.localScale = _radiationScale;
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player in radiation");
        _signalBus.Fire(new EnterRadiationSignal());
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player out of radiation");
        _signalBus.Fire(new ExitRadiationSignal());
    }
}
