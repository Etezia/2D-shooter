using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using Object_components.Interfaces;
using Shooter2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static Shooter2D.ConfigurationManager.Render;
using static Shooter2D.LevelConfiguration;
using static Shooter2D.LevelGenerator;

namespace Shooter2D
{
	internal class LevelScene : IScene
	{
		private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

		private Player player;
		public Camera MainCamera { get; private set; }

		public ContentManager GameContent { get; init; }
		private CellState[,] environmentMap;

		public Dictionary<Type, HashSet<object>> GameObjectDict { get; private set; }
			= new Dictionary<Type, HashSet<object>>()
		{
			{ typeof(ICollidingObject), new HashSet<object>() },
			{ typeof(IDynamicObject), new HashSet<object>() },
			{ typeof(IDamageableObject), new HashSet<object>() }
		};
		public List<object> GameObjects { get; private set; } = new List<object>();

		public static event Action<GameTime> OnUpdate;

		public LevelScene(ContentManager content, float xPos = 0, float yPos = 0)
		{
			GameContent = content;
			MainCamera = new Camera(new Transform2D(Vector2.Zero, 0f, Point.Zero, 0));
			(MapCellsState, CellState[,] entityMap, Point playerStartPos) = LevelGenerator.GenerateMapWithBSP();
			MainCamera = new Camera(new Transform2D(new Vector2(xPos, yPos), 0f, Point.Zero, 0));

			InitializeTextures();
			ConvertMapToGameObjects(MapCellsState);
			ConvertMapToGameObjects(entityMap);
			SetTypeToObjAccordance();
			AddPlayerOnScene(playerStartPos);
		}

		private void InitializeTextures()
		{
			GameContent.RootDirectory = "Content/Sprites";

			textures["Wall"] = GameContent.Load<Texture2D>("BrickWall1");
			textures["Enemy"] = GameContent.Load<Texture2D>("enemy_dummy1");
			textures["Floor"] = GameContent.Load<Texture2D>("floor-dummy1");
			textures["Player"] = GameContent.Load<Texture2D>("dummy1");
			textures["AK74M"] = GameContent.Load<Texture2D>("riffle1");
			textures["RiffleBullet"] = GameContent.Load<Texture2D>("bullet1");
		}

		private void AddPlayerOnScene(Point? startPos = null)
		{
			Vector2 startPosVector = new Vector2(SceneManager.WindowWidth / 2, SceneManager.WindowHeight / 2);
			if (startPos != null)
				startPosVector = new Vector2(startPos.Value.X * MapCellSize, startPos.Value.Y * MapCellSize);

			player = new Player(textures["Player"],
				new Transform2D(startPosVector, 0f, new Point(UnitSize, UnitSize), Layers.Player),
				new RectCollider(UnitSize, UnitSize / 2, y: UnitSize / 2), 100);

			player.Collider.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
			GameObjects.Add(player);
			MainCamera.Transform.SetPosition(new Vector2(
				SceneManager.WindowWidth / 2, SceneManager.WindowHeight / 2) - player.Transform.Position);
			MainCamera.ObjectPhysics.SetVelocity(new Vector2(player.VelX, player.VelY));

			player.Transform.OnPositionReassigned +=
				() => MainCamera.Transform
					.SetPosition(-player.Transform.Position +
					new Vector2(SceneManager.WindowWidth / 2, SceneManager.WindowHeight / 2));
		}

		public void ConvertMapToGameObjects(CellState[,] map)
		{
			var width = map.GetLength(0);
			var height = map.GetLength(1);

			for (var i = 0; i < width; i++) 
				for (var j = 0; j < height; j++)
				{
					var currCell = map[i, j];

					if (currCell == CellState.Floor)
					{
						GameObjects.Add(new DecorativeEnvironment(textures["Floor"],
							new Transform2D(new Vector2(i - InitLevelPosX, j - InitLevelPosY) * MapCellSize,
							0f, new Point(MapCellSize, MapCellSize), Layers.Floor)));
					}
					if (currCell == CellState.Wall)
					{
						var currObj = new Obstacle(textures["Wall"],
								new Transform2D(new Vector2(i - InitLevelPosX, j - InitLevelPosY) * MapCellSize,
								0f, new Point(MapCellSize, MapCellSize), Layers.Wall),
								new RectCollider(MapCellSize, MapCellSize));

						currObj.Collider
							.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
						GameObjects.Add(currObj);
						
					}
					else if (currCell == CellState.Enemy)
					{
						var currObj = new Enemy(textures["Enemy"],
								new Transform2D(new Vector2(i - InitLevelPosX, j - InitLevelPosY) * MapCellSize,
								0f, new Point(UnitSize, UnitSize), Layers.Enemy),
								new RectCollider(UnitSize, UnitSize / 2, y : UnitSize / 2), 200);

						currObj.Collider
							.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
						GameObjects.Add(currObj);
					}
				}
		}

		private void AddSomeFunnyStuff(int width, int height)
		{
			InputManager.OnLMBDown += () =>
			{
				lock (this)
				{
					Random rnd = new Random();

					AddEntityOnScene(new Obstacle(textures["Wall"],
							new Transform2D(
							new Vector2(rnd.Next(0, width) - InitLevelPosX, rnd.Next(0, height) - InitLevelPosY) * MapCellSize,
							0f, new Point(MapCellSize, MapCellSize), Layers.Wall),
							new RectCollider(MapCellSize, MapCellSize)));
				}
			};
		}

		private void SetTypeToObjAccordance()
		{
			foreach (var obj in GameObjects)
			{
				if (obj is ICollidingObject)
					GameObjectDict[typeof(ICollidingObject)].Add(obj);
				if (obj is IDynamicObject)
					GameObjectDict[typeof(IDynamicObject)].Add(obj);
				if (obj is IDamageableObject)
					GameObjectDict[typeof (IDamageableObject)].Add(obj);
			}
		}

		public void Update(GameTime gameTime)
		{
			foreach (var obj in GameObjectDict[typeof(IDamageableObject)].OfType<IDamageableObject>())
			{
				if (obj.Health.HealthAmount <= 0)
				{
					RemoveEntityFromScene(obj);
				}
			}

			OnUpdate?.Invoke(gameTime);
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			var cameraX = (int)MainCamera.Transform.Position.X;
			var cameraY = (int)MainCamera.Transform.Position.Y;
			spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront);

			foreach (ISprite sprite in GameObjects)
			{
				spriteBatch.Draw(sprite.Texture, 
					sprite.Transform.ToRectangleWithOffset(cameraX, cameraY), null, 
					Color.White, sprite.Transform.Rotation, Vector2.Zero, 
					SpriteEffects.None, sprite.Transform.Layer);
			}
			
			spriteBatch.End();
		}

		private void AddEntityOnScene(object entity)
		{
			GameObjects.Add(entity);

			if (entity is ICollidingObject)
			{
				GameObjectDict[typeof(ICollidingObject)].Add(entity);
				(entity as ICollidingObject).Collider.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
			}
			if (entity is IDynamicObject)
				GameObjectDict[typeof(IDynamicObject)].Add(entity);
			if (entity is IDamageableObject)
				GameObjectDict[typeof(IDamageableObject)].Add(entity);
		}

		private void RemoveEntityFromScene(object entity)
		{
			foreach (var gameObj in GameObjectDict)
			{
				if (gameObj.Value.Contains(entity))
					gameObj.Value.Remove(entity);
			}

			if (GameObjects.Contains(entity))
				GameObjects.Remove(entity);
		}
	}
}