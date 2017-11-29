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
	
	private void Update()
	{
		if (AppUtil.GetTouchInfo() == TouchInfo.Ended) LoadTitle();
	}

	private void LoadTitle()
	{
		//DontDestroyOnLoad(PreLoad.Controllers);
		SceneManager.LoadScene("Title");
	}

	public void DisplayEndGame(eEndGame eEnd)
	{
		var endGameSR = GetComponent<SpriteRenderer>();

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
	}

	// デバッグ用
	[SerializeField]
	eEndGame _eEndGame;

	private void Start()
	{
		if (_eEndGame == eEndGame.WIN) DisplayEndGame(eEndGame.WIN);
		if (_eEndGame == eEndGame.LOSE) DisplayEndGame(eEndGame.LOSE);
	}
}
