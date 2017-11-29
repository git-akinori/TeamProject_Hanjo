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
	[SerializeField]
	private GameObject enemy_soldier = null;
	[SerializeField]
	private GameObject enemy_tower = null;
	[SerializeField]
	private GameObject enemy_roller = null;
	[SerializeField]
	private GameObject enemy_boss = null;

	[SerializeField]
	private Vector3 soldier_offset = new Vector3(0, -94, 800);
	[SerializeField]
	private Vector3 tower_offset = new Vector3(-100, -90, 400);
	[SerializeField]
	private Vector3 roller_offset = new Vector3(0, -88, 800);
	[SerializeField]
	private Vector3 boss_offset = new Vector3(0, -88, 800);

	[SerializeField]
	private float enemy_spawn_width = 30;
	[SerializeField]
	private float tower_spawn_width = 100;

	[SerializeField]
	private float spawnTime = 2;
	private float timeElapsed = 0;
	private float timeElapsed_sum = 0;
	private int createdTimes = 0;

	[SerializeField]
	private bool isSpawning = true;

	private Dir direction = Dir.NORTH;

	// for debug
	[SerializeField]
	private bool northOnly = true;
	[SerializeField]
	private int enemyNum = 0;

	private Vector3 camera_pos;

	void Start()
	{
		camera_pos = Camera.main.transform.position;
	}

	void FixedUpdate()
	{
		timeElapsed += Time.deltaTime;

		if (isSpawning)
		{
			if (timeElapsed_sum < 120)
			{
				if (timeElapsed >= spawnTime)
				{
					direction = (Dir)Random.Range(0, 4);
					if (northOnly) direction = Dir.NORTH;

					if (enemyNum > 0)
					{
						createdTimes = enemyNum % 3;
					}

					if (createdTimes % 3 == 0)
						CreateEnemy(enemy_roller, direction);
					else if (createdTimes % 3 == 2)
						CreateEnemy(enemy_tower, direction);
					else if (createdTimes % 3 == 1)
						CreateEnemy(enemy_soldier, direction);


					timeElapsed_sum += timeElapsed;
					timeElapsed = 0.0f;
					++createdTimes;
				}
			}
			else
			{
				CreateEnemy(enemy_boss, Dir.NORTH);
				isSpawning = false;
			}
		}
	}

	void CreateEnemy(GameObject _obj, Dir _direction)
	{
		var obj = Instantiate(_obj, _obj.transform.position, _obj.transform.rotation, transform);

		if (_obj == enemy_soldier)
		{
			obj.transform.position = new Vector3(soldier_offset.x + enemy_spawn_width * Random.value - enemy_spawn_width / 2, soldier_offset.y, soldier_offset.z);
		}
		else if (_obj == enemy_tower)
		{
			obj.transform.position = new Vector3(tower_offset.x, tower_offset.y, tower_offset.z + tower_spawn_width * Random.value);
		}
		else if (_obj == enemy_roller)
		{
			obj.transform.position = new Vector3(roller_offset.x + enemy_spawn_width * Random.value - enemy_spawn_width / 2, roller_offset.y, roller_offset.z);
		}
		else if (_obj == enemy_boss)
		{
			obj.transform.position = new Vector3(boss_offset.x, boss_offset.y, boss_offset.z);
		}

		obj.transform.position += camera_pos;


		var degree = 0;
		switch (_direction)
		{
			default:
			case Dir.NORTH: degree = 0; break;
			case Dir.EAST: degree = 90; break;
			case Dir.SOUTH: degree = 180; break;
			case Dir.WEST: degree = 270; break;
		}

		obj.transform.RotateAround(Vector3.zero, Vector3.up, -degree);
	}

	public int SpawnDirection { get { return (int)direction; } }
}
