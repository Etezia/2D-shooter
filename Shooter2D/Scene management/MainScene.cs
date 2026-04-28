using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal class MainScene : IScene, IDynamicObject
	{
		private const int CellSize = 64;

		public Physics Physics { get; private set; } = new Physics(new Vector2(0, 0));

		public Transform2D Transform { get; private set; }

		public readonly List<object> GameObjects = new List<object>();

		public MainScene()
		{
			Transform = new Transform2D(Vector2.Zero, Quaternion.Identity, Point.Zero);
		}

		public MainScene(float xPos, float yPos)
		{
			Transform = 
				new Transform2D(new Vector2(xPos, yPos), Quaternion.Identity, Point.Zero);
		}

		public void AddPlayerOnScene(Player player)
		{
			GameObjects.Add(player);
			Physics.SetVelocity(new Vector2(player.VelX, player.VelY));

			InputManager.OnKeyUp +=
				() => Physics.Translate(InputManager.DownVector, Transform);
			InputManager.OnKeyDown +=
				() => Physics.Translate(InputManager.UpVector, Transform);
			InputManager.OnKeyLeft +=
				() => Physics.Translate(InputManager.RightVector, Transform);
			InputManager.OnKeyRight +=
				() => Physics.Translate(InputManager.LeftVector, Transform);
		}

		public void CreateLevel(int height, int width, int offset, int step, double chance, Texture2D texture)
		{
			var xSize = width / step - offset;
			var ySize = height / step - offset;
			var random = new Random();

			for (var i = offset; i < xSize; i++)
			{
				for (var j = offset; j <= ySize; j++)
				{
					if (random.NextDouble() < chance)
					{
						GameObjects.Add(new Enemy(texture, 
							new Transform2D(new Vector2(i * step, j * step), 
							Quaternion.Identity, new Point(CellSize, CellSize)),
							new RectCollider(CellSize, CellSize / 2)));
					}
				}
			}
		}

		public void Update(GameTime gameTime)
		{

		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin(samplerState: SamplerState.PointWrap);

			foreach (ISprite gameObject in GameObjects)
				spriteBatch.Draw(gameObject.Texture, new Rectangle(
					(int)gameObject.Transform.Position.X + (int)Transform.Position.X, 
					(int)gameObject.Transform.Position.Y + (int)Transform.Position.Y,
					gameObject.Transform.Scale.X, 
					gameObject.Transform.Scale.Y), Color.White);

			spriteBatch.End();
		}
	}
}
