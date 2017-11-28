using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [Script]
// UIController
// WeaponsDealer
// EnemySpawner
// HPController
static class PreLoad
{
    private static UIController uiController;
    private static WeaponsDealer weaponsDealer;
    private static EnemySpawner enemySpawner;
    private static HPController hpController;

    static PreLoad()
    {
        uiController = GameObject.Find("UIController").GetComponent<UIController>();
        weaponsDealer = GameObject.Find("WeaponsDealer").GetComponent<WeaponsDealer>();
        enemySpawner = GameObject.Find("EnemySpawner").GetComponent<EnemySpawner>();
        hpController = GameObject.Find("HPController").GetComponent<HPController>();
    }

	public static UIController UIController { get { return uiController; } }
	public static WeaponsDealer WeaponsDealer { get { return weaponsDealer; } }
	public static EnemySpawner EnemySpawner { get { return enemySpawner; } }
	public static HPController HPController { get { return hpController; } }
}