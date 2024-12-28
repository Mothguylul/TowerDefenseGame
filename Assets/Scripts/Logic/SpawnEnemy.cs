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

	private Vector3 _startpoint = new Vector3();

	private MapTile[,] mapTiles;

	private int boardheight, boardwidth;

	private LevelMap _levelMap;

	private Map _map;

	// Start is called before the first frame update
	void Start()
	{

		_levelMap = FindObjectOfType<LevelMap>();
		_map = _levelMap.Map;
		boardheight = _map.Height;
		boardwidth = _map.Width;
		mapTiles = new MapTile[boardwidth, boardheight];
		mapTiles = _levelMap.Level.GetMapTiles();
		_startpoint = FindStartPointInTilemap();

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetKeyDown(KeyCode.Space))
		{
			if (_enemyPrefab != null)
			{
				Console.WriteLine($"enemyprefab position: {_enemyPrefab.transform.position} startpoint: {_startpoint.y}{_startpoint.x} ");
				Instantiate(_enemyPrefab, _startpoint, Quaternion.identity);
			}
			else
			{
				Debug.LogError("Enemy Prefab is not assigned.");
			}
		}

	}

	/// <summary>
	/// Finds the maptile which is the startpoint and returns it
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	private Vector3 FindStartPointInTilemap()
	{
		for (int x = 0; x < mapTiles.GetLength(0); x++)
		{
			for (int y = 0; y < mapTiles.GetLength(1); y++)
			{
				if (mapTiles[x, y].IsStartPoint)
				{
					Vector3Int cellPosition = new Vector3Int(x, -y, 0);
					return _levelMap.TileMap.CellToWorld(cellPosition);
				}
			}
		}

		throw new Exception("Start point not found in map tiles.");
	}
}
