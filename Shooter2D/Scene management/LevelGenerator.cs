using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;

namespace Shooter2D;

internal static class LevelGenerator
{
	private const int MapWidth = 40;
	private const int MapHeight = 30;

	private const int MinNodeWidth = 5;
	private const int MinNodeHeight = 4;

	private class BSPNode
	{
		public int X {  get; private set; }
		public int Y { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }

		public BSPNode Left;
		public BSPNode Right;

		private static Random rng = new Random();

		public BSPNode(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}

		public Room ToRoom() => new Room(X, Y, Width, Height);
	}

	private class Room
	{
		public readonly int X;
		public readonly int Y;
		public readonly int Width;
		public readonly int Height;

		public Room(int x, int y, int width, int height)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
		}
	}

	private static CellState[,] GenerateMapWithBSP()
	{
		var result = new CellState[MapWidth, MapHeight];
		
		return result;
	}

	private static void SplitBSPNode(List<Room> rooms, BSPNode node)
	{
		if (node.Width / 2 <= MinNodeWidth || node.Height / 2 <= MinNodeHeight)
		{
			rooms.Add(node.ToRoom());
			return;
		}


	}

	public static CellState[,] GenerateMap(int probability)
	{
		var result = new CellState[MapWidth, MapHeight];
		var random = new Random();

		for (var i = 0; i < MapWidth; i++) 
			for (var j = 0; j < MapHeight; j++)
			{
				var randNum = random.Next(100);
				if (i == 0 || j == 0 || i == MapWidth - 1 || j == MapHeight - 1)
					result[i, j] = CellState.Wall;
				else
					result[i, j] = randNum < probability ? CellState.Enemy : CellState.Empty;
				//randNum < probability ? CellState.Enemy : CellState.Empty;
			}


		return result;
	}
}

public enum CellState
{
	Empty,
	Wall,
	Enemy
}
