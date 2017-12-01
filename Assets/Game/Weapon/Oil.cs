using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oil : MonoBehaviour
{

	[SerializeField]
	float speed = 1f;

	[SerializeField]
	AudioClip hitSE;

	void Start()
	{

	}

	void FixedUpdate()
	{
		if (transform.localScale.y < 2.0f)
		{
			//transform.position += new Vector3(0.0f, Time.deltaTime / 4, 0.0f);
			transform.localScale += new Vector3(0, Time.deltaTime * speed, 0);
			var collider = GetComponent<BoxCollider>();
			collider.size += new Vector3(0, Time.deltaTime * speed * 0.4f, 0);
		}
		else
		{
			GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 1 / 120f);
			Destroy(gameObject, 2f);
		}

		if (GetComponent<SpriteRenderer>().sprite.name == "nieyu_anime_5") GetComponent<Animator>().speed = 0;
	}

	private void OnTriggerEnter(Collider other)
	{
		// ヒット音再生
		GetComponent<AudioSource>().PlayOneShot(hitSE);

		if (other.tag == "Enemy")
		{
			// エフェクト生成
			//Destroy(Instantiate(effect, transform.position * 0.2f + other.transform.position * 0.8f, effect.transform.rotation, PreLoad.WeaponsDealer.transform), 1);

			// ソート
			var this_sr = GetComponent<SpriteRenderer>();
			var other_sr = other.GetComponent<SpriteRenderer>();
			this_sr.sortingOrder = other_sr.sortingOrder;
		}
	}
}
