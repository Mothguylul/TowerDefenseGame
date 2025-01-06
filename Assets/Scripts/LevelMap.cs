using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

/// <summary>
/// Looks at the Level it gets and creates the visuals for it
/// </summary>
public class LevelMap : MonoBehaviour
{

	private Grid _grid;

	private Vector3 startpointCords;


	private Camera _camera;

	private Vector3 centerPoint;

	private MapTile[,] MapTiles;

	[SerializeField] public Level Level;

	public Tilemap TileMap;

	public Map Map;
	public Vector2 StartPoint => _grid.LocalToWorld(startpointCords);

	public Grid Grid => _grid;

	// Start is called before the first frame update
	void Start()
	{
		MapTiles = Level.GetMapTiles();
		Map = new Map(MapTiles);
		startpointCords = new Vector3(Map.StartPoint.X, -Map.StartPoint.Y, 0);
		TileMap = GetComponent<Tilemap>();
		PaintTiles();
		centerPoint = CalculateCenterPoint();
		_grid = FindObjectOfType<Grid>();
		_camera = Camera.main;
		_camera.transform.position = centerPoint;

	}

	private void PaintTiles()
	{
		for (int y = 0; y < Map.Height; y++)
		{
			for (int x = 0; x < Map.Width; x++)
			{
				MapTile currentMaptile = Map.GetBoardContents(y, x);
				TileBase tileToPlace = currentMaptile.IsWalkable
					? Level.GetRandomWalkableTile()
					: Level.BuildableTile;
				TileMap.SetTile(new Vector3Int(x, -y), tileToPlace);
			}
		}
		TileMap.RefreshAllTiles();
	}

	private Vector3 CalculateCenterPoint()
	{
		BoundsInt bounds = TileMap.cellBounds;

		Vector3 min = TileMap.CellToWorld(bounds.min);
		Vector3 max = TileMap.CellToWorld(bounds.max);

		Vector3 center = (min + max) / 2;
		center.z = -10;

		return center;
	}
}
