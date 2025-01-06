using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerDefense.Logic;

namespace Assets.Scripts.Logic
{
	public class Enemy
	{

		public (int x, int y)? Position { get; private set; }

		public event EventHandler<Enemy>? EnemySpawned;

		public List<MapTile> VisitedTiles = new List<MapTile>();

		private Map _validMap;

		public Enemy(Map map)
		{
			_validMap = map;
			Spawn();
		}


		public void Spawn()
		{
			Position = _validMap.StartPoint;
			EnemySpawned?.Invoke(this, this);
		}

		public void FindNextSquare()
		{
			(int x, int y)? validSquareToMoveTo = null;

			if (!Position.HasValue)
				Console.WriteLine("Positon is null");

				(int currentX, int currentY) currentPosition = Position.Value;


			// List of all the squares next to the current position
			var neighbours = new List<(int x, int y)>
			{
				(currentPosition.currentX -1, currentPosition.currentY), // left
				(currentPosition.currentX +1, currentPosition.currentY), // right
				(currentPosition.currentX, currentPosition.currentY -1), // down
				(currentPosition.currentX, currentPosition.currentY +1) // up

			};

			foreach ((int neighbourX, int neighbourY) neighbourSquare in neighbours)
			{
				MapTile? currentTile = _validMap.GetBoardContents(neighbourSquare.neighbourY, neighbourSquare.neighbourY);
				bool wasAlreadyVisited = VisitedTiles.Any(s => s == currentTile);

				if (wasAlreadyVisited)
					continue;

				else
				{

					if (!currentTile.IsWalkable)
					{
						VisitedTiles.Add(currentTile);
						continue;
					}
					else if (currentTile.IsWalkable)
					{
						validSquareToMoveTo = (neighbourSquare.neighbourX, neighbourSquare.neighbourY);
						Position = validSquareToMoveTo;
					}
				}

			}
		}
	}
}
