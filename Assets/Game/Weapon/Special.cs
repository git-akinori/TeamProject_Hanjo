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
	float incSpeed = 0.1f;
	[SerializeField]
	float decSpeed = 0.5f;


	[SerializeField]
	float fireInterval = 0.1f;

	bool isOnSpecial;


	[SerializeField]
	GameObject adjutant;
	private Animator adjutant_animator;
	private readonly int _isSpecial = Animator.StringToHash("special");
	private readonly int _isAttack = Animator.StringToHash("attack");

	void Start()
	{
		score = 0;
		sr = GetComponent<SpriteRenderer>();
		sr.color = new Color(1, 1, 1);
		glow.SetActive(false);
		
		adjutant_animator = adjutant.transform.GetChild(0).GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (!isOnSpecial)
		{
			if (score < max_score)
			{
				score += incSpeed;
			}
			else
			{
				sr.color = new Color(1, 1, 100f / 255f);
				glow.SetActive(true);
				adjutant_animator.SetBool(_isSpecial, true);
			}
		}
		else
		{
			PreLoad.Scripts.WeaponsDealer.TapArrow();

			score -= decSpeed;

			if (score < 0)
			{
				isOnSpecial = false;
				sr.color = new Color(1, 1, 1);
				glow.SetActive(false);
				
				adjutant_animator.SetBool(_isSpecial, false);
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
		{
			isOnSpecial = true;

			adjutant_animator.SetBool(_isAttack, true);
		}
	}

	public bool IsOnSpecial { get { return isOnSpecial; } }
	public float FireInterval { get { return fireInterval; } }
}
