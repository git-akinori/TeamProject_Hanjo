using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAdjutant : MonoBehaviour
{
	[SerializeField]
	GameObject male;
	[SerializeField]
	GameObject female;

	void Awake()
	{
		GameObject obj = null;
		//Debug.Log(select_manager.GetEChara);

		if (select_manager.GetEChara == EChara.boy) obj = male;
		if (select_manager.GetEChara == EChara.girl) obj = female;

		if (obj != null)
		{
			Instantiate(obj, transform, false);
		}
	}
}
