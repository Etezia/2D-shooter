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
