using Assets.Scripts.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class SpawnEnemy : MonoBehaviour
{
	[SerializeField] private GameObject _enemyPrefab;
 

	private TestEnemy _testEnemy;

	private LevelMap _levelMap;

	// Start is called before the first frame update
	void Start()
	{
		_levelMap = FindObjectOfType<LevelMap>();
		 _testEnemy = _enemyPrefab.GetComponent<TestEnemy>();
	}

	void Update()
	{
		if (_testEnemy is null)
			Console.WriteLine("test enemy is null");

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_enemyPrefab != null)
			{
				Instantiate(_enemyPrefab, _levelMap.StartPoint, Quaternion.identity);
			}
			else
			{
				Debug.LogError("Enemy Prefab is not assigned.");
			}
		}

	}

	public void UpdatePoistion()
	{
		Vector3 vectorPos = new Vector3(_testEnemy.Enemy.Position.Value.x, _testEnemy.Enemy.Position.Value.y, 0);

		Vector3Int gridPosition = _levelMap.Grid.WorldToCell(vectorPos);
		
		_enemyPrefab.transform.position = gridPosition;
	}
}