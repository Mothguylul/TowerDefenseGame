
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace TowerDefense.Logic
{

	public class Map
	{
		private readonly MapTile[,] _board;

		public int Width => _board.GetLength(0);

		public int Height => _board.GetLength(1);

		public (int X, int Y) StartPoint { get; private set; }

		public (int X, int Y) EndPoint { get; private set; }


		public Map(MapTile[,] validBoard)
		{
			if (validBoard is null)
				throw new ArgumentNullException();

			ValidateBoard(validBoard);

			_board = validBoard;
		}

		private void ValidateBoard(MapTile[,] board)
		{
			int startpoints = 0;
			int endpoints = 0;

			int height = board.GetLength(1);
			int width = board.GetLength(0);

			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					var tile = board[x, y];

					if (board[x, y] == null)
						throw new Exception();

					Debug.WriteLine($"Inspecting tile at ({x}, {y}): IsStartPoint={tile.IsStartPoint}, IsEndPoint={tile.IsEndPoint}");

					if (tile.IsStartPoint)
					{
						startpoints++;
						StartPoint = (x, y);
						Debug.WriteLine($"StartPoint found at ({x}, {y})");
					}

					if (tile.IsEndPoint)
					{
						endpoints++;
						EndPoint = (x, y);
						Debug.WriteLine($"EndPoint found at ({x}, {y})");
					}
				}
			}

			if (startpoints != 1)
				throw new Exception("Exactly one StartPoint must be defined.");
			if (endpoints != 1)
				throw new Exception("Exactly one EndPoint must be defined.");
		}


		/// <summary>
		/// Gets a Value on the Board by the given Y and X values
		/// </summary>
		/// <param name="y"></param>
		/// <param name="x"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException"></exception>
		public MapTile? GetBoardContents(int y, int x)
		{
			if (x < 0 || x >= Width || y < 0 || y >= Height)
				return null;
			else
				return _board[y, x];
		}

	}
}
