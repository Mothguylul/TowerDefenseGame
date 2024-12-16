using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using UnityEngine;

public class TileMap : MonoBehaviour
{

    private Map _map;

    [SerializeField] private Level _level;

    // Start is called before the first frame update
    void Start()
    {
        var maptiles = _level.GetMapTiles();
		_map = new Map(maptiles);
	}

}
