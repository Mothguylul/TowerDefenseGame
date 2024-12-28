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

	private LevelMap _levelMap;

	// Start is called before the first frame update
	void Start()
	{
		_levelMap = FindObjectOfType<LevelMap>();
	}

	void Update()
	{

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
}