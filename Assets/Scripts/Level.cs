using System;
using System.Collections;
using System.Collections.Generic;
using TowerDefense.Logic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine.Tilemaps;

[CreateAssetMenu(fileName = "level", menuName = "Level")]
public class Level : ScriptableObject
{
	[TextArea(minLines: 5, maxLines: 20)]
	[SerializeField] private string _layout;

	[SerializeField] private TileBase _walkableTile;
	[SerializeField] private TileBase _buildableTile;

	public TileBase WalkableTile => _walkableTile;
	public TileBase BuildableTile => _buildableTile;


	/// <summary>
	/// Populates the Maptile[,] with the right Maptiles and returns all Maptiles by the string
	/// </summary>
	/// <returns></returns>
	/// <exception cref="Exception"></exception>
	public MapTile[,] GetMapTiles()
	{
		string[] lines = _layout.Split('\n');
		int layoutHeight = lines.Length;
		int layoutWidth = lines[0].Length;

		Debug.Log(_layout);
		Debug.Log($"Width: {layoutWidth} Height: {layoutHeight}");
		MapTile[,] tiles = new MapTile[layoutHeight, layoutWidth];

		for (int h = 0; h < layoutHeight; h++)
		{
			for (int w = 0; w < layoutWidth; w++)
			{
				tiles[h, w] = lines[h][w] switch
				{
					'x' => new MapTile(isWalkable: false),
					'o' => new MapTile(isWalkable: true),
					's' => new MapTile(isWalkable: true, isStartPoint: true),
					'e' => new MapTile(isWalkable: true, isEndPoint: true),
					_ => throw new Exception()

				};
			}
		}
		return tiles;
	}
}
