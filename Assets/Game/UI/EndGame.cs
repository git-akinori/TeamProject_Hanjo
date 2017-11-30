using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
	[SerializeField]
	Sprite win;
	[SerializeField]
	Sprite lose;

	public enum eEndGame
	{
		WIN = 0,
		LOSE = 1,
		NONE = 99,
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (AppUtil.GetTouchInfo() == TouchInfo.Ended && GetComponent<SpriteRenderer>().enabled == true)
		{
			LoadTitle();
		}
	}

	private void LoadTitle()
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadScene("Title");
	}

	public void DisplayEndGame(eEndGame eEnd)
	{
		var endGameSR = GetComponent<SpriteRenderer>();

		if (endGameSR.enabled == false)
		{
			if (eEnd == eEndGame.NONE) return;
			else if (eEnd == eEndGame.WIN)
			{
				endGameSR.sprite = win;
				name = "Win";
			}
			else if (eEnd == eEndGame.LOSE)
			{
				endGameSR.sprite = lose;
				name = "Lose";
			}
			Time.timeScale = 0;
			endGameSR.enabled = true;
		}
	}

	// デバッグ用
	[SerializeField]
	eEndGame _eEndGame;
	
}
