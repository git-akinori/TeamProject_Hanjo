using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{

	[SerializeField]
	GameObject glow;

	[SerializeField]
	float max_score = 500;

	SpriteRenderer sr;
	[SerializeField]
	float score;

	[SerializeField]
	float fireInterval = 0.1f;

	bool isOnSpecial;

	void Start()
	{
		score = 0;
		sr = GetComponent<SpriteRenderer>();
		sr.color = new Color(1, 1, 1);
		glow.SetActive(false);
	}

	void Update()
	{
		if (!isOnSpecial)
		{
			if (score < max_score)
			{
				score += 0.1f;
			}
			else
			{
				sr.color = new Color(1, 1, 100f / 255f);
				glow.SetActive(true);
			}
		}
		else
		{
			PreLoad.Scripts.WeaponsDealer.TapArrow();
			score -= 1f;

			if (score < 0)
			{
				isOnSpecial = false;
				sr.color = new Color(1, 1, 1);
				glow.SetActive(false);
			}
		}
	}

	public void AddScore(float _score)
	{
		score += _score;
	}

	public void TurnOnSpecial()
	{
		if (score >= max_score)
			isOnSpecial = true;
	}

	public bool IsOnSpecial { get { return isOnSpecial; } }
	public float FireInterval { get { return fireInterval; } }
}
