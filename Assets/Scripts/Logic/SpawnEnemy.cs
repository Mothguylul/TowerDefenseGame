using Assets.Scripts.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SpawnEnemy : MonoBehaviour
{
	[SerializeField] private GameObject _enemyPrefab;
	private Tilemap _tilemap;

	private Map _map;

	private Level _level;

	private int _boardHeight, _boardWidth;

	private Vector3 _startpoint;

	// Start is called before the first frame update
	void Start()
	{
		MapTile[,] maptiles = _level.GetMapTiles();
		_tilemap = GetComponent<Tilemap>();
		_boardHeight = _map.Height;
		_boardWidth = _map.Width;
		 _startpoint = FindStartPointInTilemap();

	}

	// Update is called once per frame
	void Update()
	{
		Console.WriteLine($"enemyprefab position: {_enemyPrefab.transform.position} startpoint: {_startpoint.y}{_startpoint.x} ");

		if (Input.GetKeyDown(KeyCode.E))
		{
			if (_enemyPrefab != null)
			{
				Instantiate(_enemyPrefab, _startpoint, Quaternion.identity);
				Console.WriteLine("Trying to set enemy to startpos");
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
		BoundsInt bounds = _tilemap.cellBounds;

		for (int x = bounds.xMin; x < bounds.xMax; x++)
		{
			for (int y = bounds.yMin; y < bounds.yMax; y++)
			{
				Vector3Int cellPosition = new Vector3Int(x, y, 0);

				MapTile maptile = _map.GetBoardContents(x, y);

				TileBase tile = _tilemap.GetTile(cellPosition);
				if (tile != null && maptile.IsStartPoint)
				{
					Vector3 worldPosition = _tilemap.CellToWorld(cellPosition);
					return worldPosition;
				}
			}
		}

		throw new Exception("No start point tile found in the tilemap.");
	}
}
