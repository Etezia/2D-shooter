using Assimp;
using Microsoft.Xna.Framework;
using Object_components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D;

internal static class LevelGenerator
{
	private const int MapWidth = 65;
	private const int MapHeight = 55;

	private const int MinRoomWidth = 6;
	private const int MinRoomHeight = 5;
	private const int MinRoomOffset = 3;
	private const int MaxRoomOffset = 5;
	private const float SplittingRandomOffset = 0.55f;

	private const int IterationsCount = 3;

	private const float EnemySpawnChance = 0.15f;

	private class BSPNode
	{
		public int Left {  get; private set; }
		public int Top { get; private set; }
		public int Right { get; private set; }
		public int Bottom { get; private set; }

		public int Width => Right - Left;
		public int Height => Bottom - Top;

		public BSPNode LeftNode;
		public BSPNode RightNode;

		public static Random Rng { get; } = new Random();

		public BSPNode(int left, int top, int right, int bottom)
		{
			Left = left;
			Top = top;
			Right = right;
			Bottom = bottom;
		}

		private int RandomOffset => Rng.Next(MinRoomOffset, MaxRoomOffset + 1);

		public Room ToRoom(RoomType type) => new Room(
			new Rectangle(Left + RandomOffset, Top + RandomOffset, Width - 2 * RandomOffset, Height - 2 * RandomOffset), type);
	}

	private class Room
	{
		private readonly Rectangle placementInfo;
		public int Left => placementInfo.Left;
		public int Right => placementInfo.Right;
		public int Top => placementInfo.Top;
		public int Bottom => placementInfo.Bottom;
		public int Width => placementInfo.Width;
		public int Height => placementInfo.Height;
		public Point Center => placementInfo.Center;

		public RoomType Type { get; set; }

		public Room(Rectangle placementInfo, RoomType type)
		{
			this.placementInfo = placementInfo;
			Type = type;
		}
	}

	public static (CellState[,] map, Point playerPos) GenerateMapWithBSP()
	{
		var result = new CellState[MapWidth, MapHeight];
		var rooms = new List<Room>();
		var headNode = new BSPNode(0, 0, MapWidth - MinRoomOffset - 1, MapHeight - MinRoomOffset - 1);

		SplitBSPNode(headNode, IterationsCount);
		CreateRooms(headNode, rooms);
		ApplyRoomsForMap(rooms, result);

		var playerRoom = rooms[new Random().Next(0, rooms.Count)];
		playerRoom.Type = RoomType.PlayerRoom;
		foreach (var room in rooms)
		{
			if (room.Type == RoomType.EnemyRoom)
				GenerateEnemiesInRoom(result, room.Left, room.Right, room.Top, room.Bottom);
		}

		var count = rooms.Count;
		for (var i = 1; i < count; i++)
			PaveCorridorBetweenTwoRoads(rooms[i - 1], rooms[i], result);
		for (var i = 2; i < count; i += 2)
			PaveCorridorBetweenTwoRoads(rooms[i - 2], rooms[i], result);

		return (result, playerRoom.Center);
	}

	private static void SplitBSPNode(BSPNode node, int iterationsLeft)
	{
		if (node.Width / 4 <= MinRoomWidth && node.Height / 4 <= MinRoomHeight || iterationsLeft <= 0)
			return;

		if (node.Height >= node.Width)
		{
			var splitRatio = (int)Math.Round(
				(float)(node.Height / 2) * SplittingRandomOffset * (-0.5 + BSPNode.Rng.NextSingle()));
			node.LeftNode = new BSPNode(node.Left, node.Top, node.Right, node.Bottom - node.Height / 2 - splitRatio - 1);
			node.RightNode = new BSPNode(node.Left, node.Bottom - node.Height / 2 - splitRatio, node.Right, node.Bottom);
		}
		else
		{
			var splitRatio = (int)Math.Round(
				(float)(node.Width / 2) * SplittingRandomOffset * (-0.5 + BSPNode.Rng.NextSingle()));
			node.LeftNode = new BSPNode(node.Left, node.Top, node.Right - node.Width / 2 - splitRatio - 1, node.Bottom);
			node.RightNode = new BSPNode(node.Right - node.Width / 2 - splitRatio, node.Top, node.Right, node.Bottom);
		}

		SplitBSPNode(node.RightNode, iterationsLeft - 1);
		SplitBSPNode(node.LeftNode, iterationsLeft - 1);
	}

	private static void CreateRooms(BSPNode node, List<Room> rooms)
	{
		if (node == null) return;

		if (node.LeftNode == null && node.RightNode == null)
		{
			var room = node.ToRoom(RoomType.EnemyRoom);
			if (room.Width >= MinRoomWidth && room.Height >= MinRoomHeight)
				rooms.Add(room);
		}
		else
		{
			CreateRooms(node.LeftNode, rooms);
			CreateRooms(node.RightNode, rooms);
		}
	}

	private static void ApplyRoomsForMap(List<Room> rooms, CellState[,] map)
	{
		foreach (var room in rooms)
		{
			for (var i = room.Left; i <= room.Right; i++)
			{
				map[i, room.Top] = CellState.Wall;
				map[i, room.Bottom] = CellState.Wall;
			}

			for (var i = room.Top; i <= room.Bottom; i++)
			{
				map[room.Left, i] = CellState.Wall;
				map[room.Right, i] = CellState.Wall;
			}
		}
	}

	private static void PaveCorridorBetweenTwoRoads(Room room1, Room room2, CellState[,] map)
	{
		var systemCenterPoint = (room1.Center + room2.Center) / new Point(2, 2);

		if (room1.Right - 1 > room2.Left && room1.Left + 1 < room2.Right)
		{
			var xCenter = (Math.Max(room1.Left, room2.Left) + Math.Min(room1.Right, room2.Right)) / 2;
			var bottomSide = Math.Max(room1.Top, room2.Top);
			for (var i = Math.Min(room1.Bottom, room2.Bottom); i <= bottomSide; i++)
			{
				map[xCenter + 1, i] = CellState.Wall;
				map[xCenter - 1, i] = CellState.Wall;
				map[xCenter, i] = CellState.Empty;
			}
		}
		else if (room1.Bottom - 1 > room2.Top && room1.Top + 1 < room2.Bottom)
		{
			var yCenter = (Math.Max(room1.Top, room2.Top) + Math.Min(room1.Bottom, room2.Bottom)) / 2;
			var rightSide = Math.Max(room1.Left, room2.Left);
			for (var i = Math.Min(room1.Right, room2.Right); i <= rightSide; i++)
			{
				map[i, yCenter + 1] = CellState.Wall;
				map[i, yCenter - 1] = CellState.Wall;
				map[i, yCenter] = CellState.Empty;
			}
		}
	}

	public static void GenerateEnemiesInRoom(
		CellState[,] map, int left, int right, int top, int bottom)
	{
		var random = new Random();

		for (var i = left + 1; i < right - 1; i++)
			for (var j = top + 1; j < bottom - 1; j++)
			{
				if (random.Next(100) < (int)(EnemySpawnChance * 100))
					map[i, j] = CellState.Enemy;
			}
	}

	public static CellState[,] GenerateRectangleMap(int probability)
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

public enum RoomType
{
	EmptyRoom,
	PlayerRoom,
	EnemyRoom,
	BossRoom
}