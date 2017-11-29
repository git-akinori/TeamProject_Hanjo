using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreLoad : MonoBehaviour
{
	[SerializeField]
	private GameObject uiController;
	[SerializeField]
	private GameObject weaponsDealer;
	[SerializeField]
	private GameObject enemySpawner;
	[SerializeField]
	private GameObject hpController;
	[SerializeField]
	private GameObject soundController;

	[SerializeField]
	private GameObject endGame;

	void Awake()
	{
		Instantiate(uiController, transform);
		Instantiate(weaponsDealer, transform);
		Instantiate(enemySpawner, transform);
		Instantiate(hpController, transform);
		Instantiate(soundController, transform);
	}
	
	public UIController UIController { get { return uiController.GetComponent<UIController>(); } }
	public WeaponsDealer WeaponsDealer { get { return weaponsDealer.GetComponent<WeaponsDealer>(); } }
	public EnemySpawner EnemySpawner { get { return enemySpawner.GetComponent<EnemySpawner>(); } }
	public HPController HPController { get { return hpController.GetComponent<HPController>(); } }
	public SoundController SoundController { get { return soundController.GetComponent<SoundController>(); } }

	public EndGame EndGame { get { return endGame.GetComponent<EndGame>(); } }

	private void Start()
	{
		
	}
}
