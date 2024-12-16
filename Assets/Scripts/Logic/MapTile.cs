using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TowerDefense.Logic
{
	/// <summary>
	/// Represents one Square at the Board
	/// </summary>
	public class MapTile
	{
		public bool EnemyCanWalk { get; private set; }

		public bool CanPlaceTower { get; private set; }

		public bool IsWalkable { get; private set; }

		public bool IsBuildable => !IsWalkable;

		public bool IsStartPoint { get; private set; } = false;

		public bool IsEndPoint { get; private set; } = false;


		public MapTile(bool isWalkable = true, bool isStartPoint = false, bool isEndPoint = false)
		{

			if (isEndPoint && isStartPoint)
				throw new Exception("A Start and an Endpoint cant be at the same point.");

			if (isWalkable is false && isStartPoint)
				throw new Exception("A Start and an Endpoint cant be at the same point.");
			else if (isWalkable is false && isEndPoint)
				throw new Exception("A Start and an Endpoint cant be at the same point.");

			IsWalkable = isWalkable;
			IsStartPoint = isStartPoint;
			IsEndPoint = isEndPoint;
		}
	}
}
