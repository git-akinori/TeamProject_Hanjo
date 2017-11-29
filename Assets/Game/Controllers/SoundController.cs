using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
	[SerializeField]
	GameObject bgm;

	void Start()
	{
		Instantiate(bgm, transform);
	}

	void Update()
	{

	}
}
