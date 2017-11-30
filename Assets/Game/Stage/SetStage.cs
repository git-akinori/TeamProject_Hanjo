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
	bool debug = false;
	[SerializeField]
	EChapter debug_chapter = EChapter.chapter_one;

	// soldier_limit_st1	: (3, no limit) no limit is sepecial number 0 
	// tower_limit_st1		: (8, 2)
	// roller_limit_st1		: (30, 1)
	SpawnLimitEachEnemy st1_limit
		= new SpawnLimitEachEnemy(
			new SpawnLimit(3, 0),
			new SpawnLimit(8, 2),
			new SpawnLimit(30, 1));

	// soldier_limit_st2	: (3, no limit)
	// tower_limit_st2		: (8, 2)
	// roller_limit_st2		: (30, 1)
	SpawnLimitEachEnemy st2_limit
		= new SpawnLimitEachEnemy(
			new SpawnLimit(3, 0),
			new SpawnLimit(8, 2),
			new SpawnLimit(30, 1));

	// soldier_limit_st3	: (3, no limit)
	// tower_limit_st3		: (8, 2)
	// roller_limit_st3		: (15, 1)
	SpawnLimitEachEnemy st3_limit
		= new SpawnLimitEachEnemy(
			new SpawnLimit(3, 0),
			new SpawnLimit(8, 2),
			new SpawnLimit(15, 1));

	SpawnLimitEachEnemy spawn_limit_each_enemy;

	[SerializeField]
	Vector2 soldier_spawn_limit;
	[SerializeField]
	Vector2 tower_spawn_limit;
	[SerializeField]
	Vector2 roller_spawn_limit;

	void Awake()
	{
		var chapter = select_manager.GetEChapter;

		// for debug
		if (debug)
		{
			chapter = debug_chapter;
		}
		
		spawn_limit_each_enemy = new SpawnLimitEachEnemy(
			new SpawnLimit(soldier_spawn_limit.x, (int)soldier_spawn_limit.y),
			new SpawnLimit(tower_spawn_limit.x, (int)tower_spawn_limit.y),
			new SpawnLimit(roller_spawn_limit.x, (int)roller_spawn_limit.y));

		if (chapter == EChapter.chapter_one)
		{
			north = st1_north;
			east = st1_east;
			south = st1_south;
			west = st1_west;

			spawn_limit_each_enemy = st1_limit;
		}
		if (chapter == EChapter.chapter_two)
		{
			north = st2_north;
			east = st2_east;
			south = st2_south;
			west = st2_west;

			spawn_limit_each_enemy = st2_limit;
		}
		if (chapter == EChapter.chapter_three)
		{
			north = st3_north;
			east = st3_east;
			south = st3_south;
			west = st3_west;

			spawn_limit_each_enemy = st3_limit;
		}

		northObj.GetComponent<SpriteRenderer>().sprite = north;
		eastObj.GetComponent<SpriteRenderer>().sprite = east;
		southObj.GetComponent<SpriteRenderer>().sprite = south;
		westObj.GetComponent<SpriteRenderer>().sprite = west;

		Debug.Log(spawn_limit_each_enemy.Soldier.SpawnTime + " " + spawn_limit_each_enemy.Soldier.MaxSpawnNum);
		Debug.Log(spawn_limit_each_enemy.Tower.SpawnTime + " " + spawn_limit_each_enemy.Tower.MaxSpawnNum);
		Debug.Log(spawn_limit_each_enemy.Roller.SpawnTime + " " + spawn_limit_each_enemy.Roller.MaxSpawnNum);
	}

	public SpawnLimitEachEnemy SpawnLimitEachEnemy { get { return spawn_limit_each_enemy; } }
}
