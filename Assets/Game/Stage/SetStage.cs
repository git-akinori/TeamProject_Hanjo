using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	[SerializeField]
	Sprite st1_north;
	[SerializeField]
	Sprite st1_east;
	[SerializeField]
	Sprite st1_south;
	[SerializeField]
	Sprite st1_west;

	[SerializeField]
	Sprite st2_north;
	[SerializeField]
	Sprite st2_east;
	[SerializeField]
	Sprite st2_south;
	[SerializeField]
	Sprite st2_west;

	[SerializeField]
	Sprite st3_north;
	[SerializeField]
	Sprite st3_east;
	[SerializeField]
	Sprite st3_south;
	[SerializeField]
	Sprite st3_west;
	
	Sprite north;
	Sprite east;
	Sprite south;
	Sprite west;

	[SerializeField]
	bool onDebug = false;
	[SerializeField]
	EChapter debug_chapter = EChapter.chapter_one;

	void Start()
	{
		var chapter = select_manager.GetEChapter;

#if UNITY_EDITOR
		if (onDebug) chapter = debug_chapter;
#endif
		if (chapter == EChapter.chapter_one || chapter == EChapter.none)
		{
			north = st1_north;
			east = st1_east;
			south = st1_south;
			west = st1_west;
		}
		if (chapter == EChapter.chapter_two)
		{
			north = st2_north;
			east = st2_east;
			south = st2_south;
			west = st2_west;
		}
		else if (chapter == EChapter.chapter_three)
		{
			north = st3_north;
			east = st3_east;
			south = st3_south;
			west = st3_west;
		}

		northObj.GetComponent<SpriteRenderer>().sprite = north;
		eastObj.GetComponent<SpriteRenderer>().sprite = east;
		southObj.GetComponent<SpriteRenderer>().sprite = south;
		westObj.GetComponent<SpriteRenderer>().sprite = west;
	}
}

public struct SpawnLimit
{
	float spawn_time;    // spawn_time (second)
	int max_spawn_num;   // max_spawn_num

	public SpawnLimit(float _spawn_time, int _max_spawn_num)
	{
		spawn_time = _spawn_time;
		max_spawn_num = _max_spawn_num;
	}

	public float SpawnTime { get { return spawn_time; } }
	public int MaxSpawnNum { get { return max_spawn_num; } }
}

public struct SpawnLimits
{
	SpawnLimit soldier;
	SpawnLimit tower;
	SpawnLimit roller;

	public SpawnLimits(SpawnLimit _soldier, SpawnLimit _tower, SpawnLimit _roller)
	{
		soldier = _soldier;
		tower = _tower;
		roller = _roller;
	}

	public SpawnLimit Soldier { get { return soldier; } }
	public SpawnLimit Tower { get { return tower; } }
	public SpawnLimit Roller { get { return roller; } }
}
