using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PreLoad : MonoBehaviour
{
	private static PreLoad preLoad;

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
	private GameObject special;
	[SerializeField]
	private GameObject endGame;

	void Start()
	{
		preLoad = gameObject.GetComponent<PreLoad>();
	}

	public static PreLoad Scripts { get { return preLoad; } }
	
	public UIController UIController { get { return uiController.GetComponent<UIController>(); } }
	public WeaponsDealer WeaponsDealer { get { return weaponsDealer.GetComponent<WeaponsDealer>(); } }
	public EnemySpawner EnemySpawner { get { return enemySpawner.GetComponent<EnemySpawner>(); } }
	public HPController HPController { get { return hpController.GetComponent<HPController>(); } }
	public SoundController SoundController { get { return soundController.GetComponent<SoundController>(); } }

	public Special Special { get { return special.GetComponent<Special>(); } }
	public EndGame EndGame { get { return endGame.GetComponent<EndGame>(); } }
}
