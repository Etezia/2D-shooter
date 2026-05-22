using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using Object_components.Interfaces;
using Shooter2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooter2D.ConfigurationManager.Render;

namespace Shooter2D
{
	internal class LevelScene : IScene
	{
		private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

		private const int CellSize = 64;
		private const int initPosX = 0 * CellSize;
		private const int initPosY = 0 * CellSize;
		private CellState[,] levelMap;
		
		public Camera MainCamera { get; private set; }
		public ContentManager GameContent { get; init; }
		public Dictionary<Type, HashSet<object>> GameObjectDict { get; private set; }
			= new Dictionary<Type, HashSet<object>>()
		{
			{ typeof(ISprite), new HashSet<object>() },
			{ typeof(ICollidingObject), new HashSet<object>() },
			{ typeof(IDynamicObject), new HashSet<object>() },
			{ typeof(IDamageableObject), new HashSet<object>() }
		};
		public List<object> GameObjects { get; private set; } = new List<object>();

		public LevelScene(ContentManager content)
		{
			GameContent = content;
			MainCamera = new Camera(new Transform2D(Vector2.Zero, 0f, Point.Zero, 0));
			levelMap = LevelGenerator.GenerateMap(5);
			InitializeTextures();
			ConvertMapToSprites();
		}

		public LevelScene(ContentManager content, float xPos, float yPos) : this(content)
		{
			MainCamera = new Camera(new Transform2D(new Vector2(xPos, yPos), 0f, Point.Zero, 0));
		}

		private void InitializeTextures()
		{
			GameContent.RootDirectory = "Content/Sprites";

			textures["Wall"] = GameContent.Load<Texture2D>("BrickWall1");
			textures["Enemy"] = GameContent.Load<Texture2D>("enemy_dummy1");
			textures["Floor"] = GameContent.Load<Texture2D>("floor-dummy1");
		}

		public void ConvertMapToSprites()
		{
			var width = levelMap.GetLength(0);
			var height = levelMap.GetLength(1);

			InputManager.OnLMBDown += () =>
			{
				lock (this)
				{
					Random rnd = new Random();

					var currObj = new Obstacle(textures["Wall"],
									new Transform2D(
									new Vector2(rnd.Next(0, width) - initPosX, rnd.Next(0, height) - initPosY) * CellSize,
									0f, new Point(CellSize, CellSize), Layers.Wall),
									new RectCollider(CellSize, CellSize));

					currObj.Collider
								.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
					GameObjects.Add(currObj);
					GameObjectDict[typeof(ICollidingObject)].Add(currObj);
				}
			};

			for (var i = 0; i < width; i++) 
				for (var j = 0; j < height; j++)
				{
					var currCell = levelMap[i, j];

					GameObjects.Add(new DecorativeEnvironment(textures["Floor"],
						new Transform2D(new Vector2(i - initPosX, j - initPosY) * CellSize, 
						0f, new Point(CellSize, CellSize), Layers.Floor)));

					if (currCell == CellState.Wall)
					{
						var currObj = new Obstacle(textures["Wall"],
								new Transform2D(new Vector2(i - initPosX, j - initPosY) * CellSize,
								0f, new Point(CellSize, CellSize), Layers.Wall),
								new RectCollider(CellSize, CellSize));

						currObj.Collider
							.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
						GameObjects.Add(currObj);
						
					}
					else if (currCell == CellState.Enemy)
					{
						var currObj = new Enemy(textures["Enemy"],
								new Transform2D(new Vector2(i - initPosX, j - initPosY) * CellSize,
								0f, new Point(CellSize, CellSize), Layers.Enemy),
								new RectCollider(CellSize, CellSize), 200);

						currObj.Collider
							.SetCollidingObjects(GameObjectDict[typeof(ICollidingObject)]);
						GameObjects.Add(currObj);
					}
				}

			SetTypeToObjAccordance();
		}

		private void SetTypeToObjAccordance()
		{
			foreach (var obj in GameObjects)
			{
				if (obj is ISprite)
					GameObjectDict[typeof(ISprite)].Add(obj);
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
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			var cameraX = (int)MainCamera.Transform.Position.X;
			var cameraY = (int)MainCamera.Transform.Position.Y;
			spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, 
				samplerState: SamplerState.PointWrap);

			foreach (ISprite sprite in GameObjects)
			{
				spriteBatch.Draw(sprite.Texture, 
					sprite.Transform.ToRectangleWithOffset(cameraX, cameraY), null, 
					Color.White, sprite.Transform.Rotation, Vector2.Zero, 
					SpriteEffects.None, sprite.Transform.Layer);
			}
			
			spriteBatch.End();
		}

		public void RemoveEntityFromScene(object entity)
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
