using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SetStage : MonoBehaviour
{
	[SerializeField]
	GameObject northObj;
	[SerializeField]
	GameObject eastObj;
	[SerializeField]
	GameObject southObj;
	[SerializeField]
	GameObject westObj;

	void Awake()
	{
		string path = "Assets/Game/Sprites/st" + ((int)select_manager.GetEChapter + 1) + "_";

		Debug.Log(path);

		northObj.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path + "north.png");
		eastObj.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path + "east.png");
		southObj.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path + "south.png");
		westObj.GetComponent<SpriteRenderer>().sprite = AssetDatabase.LoadAssetAtPath<Sprite>(path + "west.png");
	}
}
