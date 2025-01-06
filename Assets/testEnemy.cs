using Assets.Scripts.Logic;
using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using Unity.Burst.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.WSA;


public class TestEnemy : MonoBehaviour
{
    private Enemy _enemy;

    public Enemy Enemy => _enemy;

    [SerializeField]private LevelMap _levelMap;
    private Map _map;

    // Start is called before the first frame update
    void Start()
    {
        _map = _levelMap.Map;
        _enemy = new Enemy(_map);
        _enemy.FindNextSquare();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
