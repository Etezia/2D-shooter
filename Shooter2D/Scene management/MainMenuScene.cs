using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;
using Microsoft.Xna.Framework.Content;

namespace Shooter2D
{
	internal class MainMenuScene : IScene
	{
		private Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

		public List<object> GameObjects { get; private set; }
		public ContentManager GameContent { get; init; }

		public MainMenuScene(ContentManager content)
		{
			GameObjects = new List<object>();
			GameContent = content;

			InitializeTextures();
		}

		private void InitializeTextures()
		{
			GameContent.RootDirectory = "Content/Sprites";

			textures["MenuButton"] = GameContent.Load<Texture2D>("MenuButton");
		}

		public void Draw(SpriteBatch spriteBatch)
		{
			spriteBatch.Begin();

			spriteBatch.Draw(textures["MenuButton"],
				new Rectangle(350, 150, 180, 70), Color.White);

			spriteBatch.End();
		}

		public void Update(GameTime gameTime)
		{
			
		}
	}
}
