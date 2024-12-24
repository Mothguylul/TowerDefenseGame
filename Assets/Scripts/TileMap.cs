using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;

/// <summary>
/// Looks at the Level it gets and creates the visuals for it
/// </summary>
public class LevelMap : MonoBehaviour
{

    private Map _map;
    private Tilemap _tilemap;
	private Camera _camera;

    [SerializeField] private Level _level;

    // Start is called before the first frame update
    void Start()
    {
        MapTile[,] maptiles = _level.GetMapTiles();
		_map = new Map(maptiles);
        _tilemap = GetComponent<Tilemap>();
        PaintTiles();
		Vector3 centerpoint = CalculateCenterPoint();
		_camera = Camera.main;
		_camera.transform.position = centerpoint;
	}
	private void PaintTiles()
	{
        for(int y = 0; y < _map.Height; y++)
        {
            for (int x = 0; x < _map.Width; x++)
            {
                MapTile currentMaptile = _map.GetBoardContents(y,x);
				TileBase tileToPlace = currentMaptile.IsWalkable
					? _level.WalkableTile
					: _level.BuildableTile;
				_tilemap.SetTile(new Vector3Int(x, -y), tileToPlace);
			}
        }
		_tilemap.RefreshAllTiles();
	}

    private Vector3 CalculateCenterPoint()
    {
		BoundsInt bounds = _tilemap.cellBounds;

		Vector3 min = _tilemap.CellToWorld(bounds.min);
		Vector3 max = _tilemap.CellToWorld(bounds.max);

		Vector3 center = (min + max) / 2;
		center.z = -10;

		return center;
	}
}
 