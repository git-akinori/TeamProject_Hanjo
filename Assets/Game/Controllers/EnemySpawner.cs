using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 北(0)から時計回りに西(3)まで
public enum Dir
{
	NORTH = 0,
	EAST = 1,
	SOUTH = 2,
	WEST = 3
};

public class EnemySpawner : MonoBehaviour
{
	// 暫定値
	// obj			: soldier
	// offset_pos	: (0, -94, 800)
	// spawn_range	: (30, 0)
	[SerializeField]
	GameObject soldier_obj;
	[SerializeField]
	Vector3 soldier_offset_pos = new Vector3(0, -94, 800);
	[SerializeField]
	Vector2 soldier_spawn_range = new Vector2(30, 0);
	[SerializeField]
	float soldier_spawn_time = 3;
	int soldier_max_num = 100;

	// obj			: tower
	// offset_pos	: (-30, -90, 400)
	// spawn_range	: (0, 100)
	[SerializeField]
	GameObject tower_obj;
	[SerializeField]
	Vector3 tower_offset_pos = new Vector3(-30, -90, 400);
	[SerializeField]
	Vector2 tower_spawn_range = new Vector2(0, 100);
	[SerializeField]
	float tower_spawn_time = 8;
	int tower_max_num = 2;

	// obj			: roller
	// offset_pos	: (0, -88, 800)
	// spawn_range	: (30, 0)
	[SerializeField]
	GameObject roller_obj;
	[SerializeField]
	Vector3 roller_offset_pos = new Vector3(0, -88, 800);
	[SerializeField]
	Vector2 roller_spawn_range = new Vector2(30, 0);
	[SerializeField]
	float roller_spawn_time = 30;
	int roller_max_num = 1;

	// obj			: boss
	// offset_pos	: (0, -88, 800)
	// spawn_range	: (0, 0)
	GameObject boss_obj;
	[SerializeField]
	GameObject boss1_obj;
	[SerializeField]
	GameObject boss2_obj;
	[SerializeField]
	GameObject boss3_obj;
	[SerializeField]
	Vector3 boss_offset_pos = new Vector3(0, -88, 800);
	[SerializeField]
	Vector2 boss_spawn_range = new Vector2(0, 0);
	[SerializeField]
	float boss_spawn_time = 60f;

	private EnemySpawnSystem[] enemySpawnSystems = new EnemySpawnSystem[3];
	private EnemySpawnSystem bossSpawnSystem = null;

	private float timeElapsed = 0;

	[SerializeField]
	private bool isSpawning = true;

	[SerializeField]
	private int base_turn_dir_time = 10;
	private int turn_dir_time;

	[SerializeField]
	bool onDebug = false;
	[SerializeField]
	EChapter debug_chapter = EChapter.chapter_one;

	// soldier_limit_st1	: (1, no limit) no limit is 100 
	// tower_limit_st1		: (8, 2)
	// roller_limit_st1		: (30, 1)
	SpawnLimits st1_limit
		= new SpawnLimits(
			new SpawnLimit(1, 100),
			new SpawnLimit(8, 2),
			new SpawnLimit(30, 1));

	// soldier_limit_st2	: (1, no limit)
	// tower_limit_st2		: (8, 2)
	// roller_limit_st2		: (30, 1)
	SpawnLimits st2_limit
		= new SpawnLimits(
			new SpawnLimit(1, 100),
			new SpawnLimit(8, 2),
			new SpawnLimit(30, 1));

	// soldier_limit_st3	: (1, no limit)
	// tower_limit_st3		: (8, 2)
	// roller_limit_st3		: (15, 1)
	SpawnLimits st3_limit
		= new SpawnLimits(
			new SpawnLimit(1, 100),
			new SpawnLimit(8, 2),
			new SpawnLimit(15, 1));

	SpawnLimits spawnLimits;

	[SerializeField]
	float soldier_SpawnTime;
	[SerializeField]
	int soldier_MaxNum;
	[SerializeField]
	float tower_SpawnTime;
	[SerializeField]
	int tower_MaxNum;
	[SerializeField]
	float roller_SpawnTime;
	[SerializeField]
	int roller_MaxNum;

	EChapter chapter;

	public enum EEnemy
	{
		SOLDIER,
		TOWER,
		ROLLER,
		BOSS,
		NONE,
	}

	void Start()
	{
		chapter = select_manager.GetEChapter;

#if UNITY_EDITOR
		// for debug
		if (onDebug)
		{
			chapter = debug_chapter;
		}
#endif
		// none is for debug
		if (chapter == EChapter.none)
		{
			spawnLimits = new SpawnLimits(
				new SpawnLimit(soldier_SpawnTime, soldier_MaxNum),
				new SpawnLimit(tower_SpawnTime, tower_MaxNum),
				new SpawnLimit(roller_SpawnTime, roller_MaxNum));
			boss_obj = boss1_obj;
		}
		else if (chapter == EChapter.chapter_one)
		{
			spawnLimits = st1_limit;
			boss_obj = boss1_obj;
		}
		else if (chapter == EChapter.chapter_two)
		{
			spawnLimits = st2_limit;
			boss_obj = boss2_obj;
		}
		else if (chapter == EChapter.chapter_three)
		{
			spawnLimits = st3_limit;
			boss_obj = boss3_obj;
		}

		enemySpawnSystems[(int)EEnemy.SOLDIER] = new EnemySpawnSystem(soldier_obj, soldier_offset_pos, soldier_spawn_range, spawnLimits.Soldier);
		enemySpawnSystems[(int)EEnemy.TOWER] = new EnemySpawnSystem(tower_obj, tower_offset_pos, tower_spawn_range, spawnLimits.Tower);
		enemySpawnSystems[(int)EEnemy.ROLLER] = new EnemySpawnSystem(roller_obj, roller_offset_pos, roller_spawn_range, spawnLimits.Roller);

		//enemySpawnSystems[(int)EEnemy.SOLDIER] = new EnemySpawnSystem(soldier_obj, soldier_offset_pos, soldier_spawn_range, soldier_spawn_time, soldier_max_num);
		//enemySpawnSystems[(int)EEnemy.TOWER] = new EnemySpawnSystem(tower_obj, tower_offset_pos, tower_spawn_range, tower_spawn_time, tower_max_num);
		//enemySpawnSystems[(int)EEnemy.ROLLER] = new EnemySpawnSystem(roller_obj, roller_offset_pos, roller_spawn_range, roller_spawn_time, roller_max_num);

		bossSpawnSystem = new EnemySpawnSystem(boss_obj, boss_offset_pos, boss_spawn_range);

		turn_dir_time = base_turn_dir_time;
	}

	[SerializeField]
	bool spawnBoss = false;

	void FixedUpdate()
	{
		timeElapsed += Time.deltaTime;

		if (spawnBoss)
		{
			bossSpawnSystem.CreateEnemy();
			spawnBoss = false;
			isSpawning = false;
		}

		if (isSpawning)
		{
			if (timeElapsed < boss_spawn_time)
			{
				var te = (int)timeElapsed;

				if (te > 0)
				{
					if (te % turn_dir_time == 0 && timeElapsed > base_turn_dir_time)
					{
						var dir = EnemySpawnSystem.Direction;
						var cur_dir = dir;

						while (dir == cur_dir || dir == (Dir)(((int)cur_dir + 2) % 4))
							dir = (Dir)Random.Range(0, 4);

						if (chapter == EChapter.chapter_one && dir == Dir.NORTH) dir = Dir.SOUTH;
						EnemySpawnSystem.Direction = dir;

						Debug.Log("changed spawning direction!" + ((int)EnemySpawnSystem.Direction).ToString());
						turn_dir_time += base_turn_dir_time;
					}
				}

				foreach (EnemySpawnSystem ess in enemySpawnSystems)
				{
					ess.Update();
				}
			}
			else
			{
				bossSpawnSystem.CreateEnemy();
				isSpawning = false;
			}
		}
	}

	class EnemySpawnSystem
	{
		GameObject obj;
		Vector3 offset_pos;
		float width;
		float depth;

		float spawn_time;
		int max_spawn_num;

		static Vector3 camera_pos = Camera.main.transform.position;
		static TowerMove towerMove;
		static RollerMove rollerMove;

		float elapsedTime = 0;

		int spawningNum = 0;

		static Dir direction = Dir.NORTH;

		List<GameObject> cloneList = new List<GameObject>();

		public EnemySpawnSystem(GameObject _obj, Vector3 _offset_pos, Vector2 _range)
		{
			obj = _obj;
			offset_pos = _offset_pos;
			width = _range.x;
			depth = _range.y;
		}

		public EnemySpawnSystem(GameObject _obj, Vector3 _offset_pos, Vector2 _range, float _spawn_time, int _max_spawn_num)
		{
			obj = _obj;
			offset_pos = _offset_pos;
			width = _range.x;
			depth = _range.y;
			spawn_time = _spawn_time;
			max_spawn_num = _max_spawn_num;
		}

		public EnemySpawnSystem(GameObject _obj, Vector3 _offset_pos, Vector2 _range, SpawnLimit _spawnLimit)
		{
			obj = _obj;
			offset_pos = _offset_pos;
			width = _range.x;
			depth = _range.y;
			spawn_time = _spawnLimit.SpawnTime;
			max_spawn_num = _spawnLimit.MaxSpawnNum;
		}


		public void Update()
		{
			elapsedTime += Time.deltaTime;

			spawningNum = cloneList.Count;

			if (elapsedTime >= spawn_time && spawningNum < max_spawn_num)
			{
				CreateEnemy();
				elapsedTime = 0;
			}
		}

		// Create Enemy Once
		public void CreateEnemy()
		{
			var clone = Instantiate(obj, obj.transform.position, obj.transform.rotation, PreLoad.Scripts.EnemySpawner.transform);

			clone.transform.position =
				new Vector3(offset_pos.x + Random.value * width - width / 2,
							offset_pos.y,
							offset_pos.z + Random.value * depth);

			clone.transform.position += camera_pos;

			var degree = 0;
			switch (direction)
			{
				default:
				case Dir.NORTH: degree = 0; break;
				case Dir.EAST: degree = 90; break;
				case Dir.SOUTH: degree = 180; break;
				case Dir.WEST: degree = 270; break;
			}

			clone.transform.RotateAround(Vector3.zero, Vector3.down, degree);

			cloneList.Add(clone);
		}



		public static Dir Direction { get { return direction; } set { direction = value; } }
		public int SpawningNum { get { return spawningNum; } set { spawningNum = value; } }
	}

	public int SpawnDirection { get { return (int)EnemySpawnSystem.Direction; } }
}


struct SpawnEnemyParam
{
	GameObject enemyObj; // enemy object
	Vector3 offset_pos;  // (x, y, z)
	Vector2 spawn_range; // (width, depth)

	public SpawnEnemyParam(GameObject _enemyObj, Vector3 _offset_pos, Vector2 _spawn_range)
	{
		enemyObj = _enemyObj;
		offset_pos = _offset_pos;
		spawn_range = _spawn_range;
	}

	public GameObject EnemyObj { get { return enemyObj; } }
	public Vector3 OffsetPos { get { return offset_pos; } }
	public float Width { get { return spawn_range.x; } }
	public float Depth { get { return spawn_range.y; } }
}
