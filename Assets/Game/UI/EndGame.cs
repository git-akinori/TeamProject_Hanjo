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

    [SerializeField]
    private AudioClip win_audio;

    [SerializeField]
    private AudioClip[] lose_audio;
    
    private AudioSource _audio;

	public enum eEndGame
	{
		WIN = 0,
		LOSE = 1,
		NONE = 99,
	}

	private void Start()
	{
        _audio = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (AppUtil.GetTouchInfo() == TouchInfo.Ended && GetComponent<SpriteRenderer>().enabled == true && !_audio.isPlaying)
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

                _audio.clip = win_audio;
                _audio.Play();
			}
			else if (eEnd == eEndGame.LOSE)
			{
				endGameSR.sprite = lose;
				name = "Lose";

                if (select_manager.GetEChapter == EChapter.chapter_one)
                    _audio.clip = lose_audio[0];
                else if (select_manager.GetEChapter == EChapter.chapter_two)
                    _audio.clip = lose_audio[1];
                else
                    _audio.clip = lose_audio[2];

                _audio.Play();
            }
			Time.timeScale = 0;
			endGameSR.enabled = true;
		}
	}

	// デバッグ用
	[SerializeField]
	eEndGame _eEndGame;
	
}
