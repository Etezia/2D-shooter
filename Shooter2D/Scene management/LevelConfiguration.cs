using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;

namespace Shooter2D
{
	internal static class LevelConfiguration
	{
		public const int UnitSize = 22;
		public const int MapCellSize = 25;
		public const int InitLevelPosX = 0 * MapCellSize;
		public const int InitLevelPosY = 0 * MapCellSize;
		public static CellState[,] MapCellsState { get; set; }
	}
}
