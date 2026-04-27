using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal class Scene : IScene
	{
		public readonly List<object> GameObjects = new List<object>();

		public Scene() { }

		public void AddPlayerOnScene(Player player) => GameObjects.Add(player);

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
						//Sprites.Add(new Enemy(texture, new Vector2(i * step, j * step)));
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

			foreach (var gameObject in GameObjects)
				if (gameObject is ISprite)
					spriteBatch.Draw((gameObject as ISprite).Texture, new Rectangle(
						(int)(gameObject as ISprite).Transform.Position.X, 
						(int)(gameObject as ISprite).Transform.Position.Y,
						(gameObject as ISprite).Transform.Scale.X, 
						(gameObject as ISprite).Transform.Scale.Y), Color.White);

			spriteBatch.End();
		}
	}
}
