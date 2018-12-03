using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Signals;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class EndGame : MonoBehaviour
{
    public GameObject GameUI;
    public Text EndText;
    public Button RestartButton;
    private SignalBus _signalBus;

    [Inject]
    void Init(SignalBus signalBus)
    {
        _signalBus = signalBus;

        _signalBus.Subscribe<EndGameSignal>((signal) =>
        {
            this.gameObject.SetActive(true);

            if (signal.EndGameType == EndGameType.Won)
            {
                EndText.text = "You Escaped";
            }
            else
            {
                EndText.text = "You Died";
            }

            _signalBus.Fire(new UIHideSignal(UIPanelType.Inventory));
            GameUI.SetActive(false);
        });

        RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }


	// Use this for initialization
	void Start () {

        

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
