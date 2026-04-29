using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using Shooter2D.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal class LevelScene : IScene, IDynamicObject
	{
		private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

		private const int CellSize = 64;
		private const int initPosX = 0 * CellSize;
		private const int initPosY = 0 * CellSize;
		private CellState[,] levelMap;
		
		public Physics CameraPhysics { get; private set; } = new Physics(new Vector2(0, 0));
		public Transform2D Transform { get; private set; }
		public ContentManager GameContent { get; init; }
		public List<object> GameObjects { get; private set; } = new List<object>();

		public LevelScene(ContentManager content)
		{
			GameContent = content;
			Transform = new Transform2D(Vector2.Zero, 0f, Point.Zero);
			levelMap = LevelGenerator.GenerateMap(5);
			InitializeSprites();
			ConvertMapToSprites();
		}

		public LevelScene(ContentManager content, float xPos, float yPos) : this(content)
		{
			Transform = 
				new Transform2D(new Vector2(xPos, yPos), 0f, Point.Zero);
		}

		private void InitializeSprites()
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

			for (var i = 0; i < width; i++) 
				for (var j = 0; j < height; j++)
				{
					var currCell = levelMap[i, j];

					GameObjects.Add(new DecorativeEnvironment(textures["Floor"],
						new Transform2D(new Vector2(i - initPosX, j - initPosY) * CellSize, 
						0f, new Point(CellSize, CellSize))));

					if (currCell == CellState.Wall)
					{
						GameObjects.Add(new Obstacle(textures["Wall"],
								new Transform2D(new Vector2(i - initPosX, j - initPosY) * CellSize, 
								0f, new Point(CellSize, CellSize)),
								new RectCollider(CellSize, CellSize)));
					}
					else if (currCell == CellState.Enemy)
					{
						GameObjects.Add(new Enemy(textures["Enemy"],
								new Transform2D(new Vector2(i - initPosX, j - initPosY) * CellSize, 
								0f, new Point(CellSize, CellSize)),
								new RectCollider(CellSize, CellSize)));
					}
				}
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			var cameraX = (int)Transform.Position.X;
			var cameraY = (int)Transform.Position.Y;
			spriteBatch.Begin(samplerState: SamplerState.PointWrap);

			foreach (ISprite gameObject in GameObjects)
			{


				spriteBatch.Draw(gameObject.Texture, new Rectangle(
					(int)gameObject.Transform.Position.X + cameraX,
					(int)gameObject.Transform.Position.Y + cameraY,
					gameObject.Transform.Scale.X,
					gameObject.Transform.Scale.Y), Color.White);
			}
			
			spriteBatch.End();
		}
	}
}
