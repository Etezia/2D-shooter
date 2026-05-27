using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Object_components;
using System;
using System.Threading;
using static Shooter2D.ConfigurationManager.Render;

namespace Shooter2D
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private const int DeltaTimeMS = 40;

		private const int InitialSceneId = 1;

		private readonly Thread inputThread = 
			new Thread(() => new InputManager().Update(25));

		public Game1()
		{
			graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			graphics.PreferredBackBufferWidth = 1280;
			graphics.PreferredBackBufferHeight = 720;
			graphics.ApplyChanges();

			//graphics.ToggleFullScreen();

			Window.Title = "GameMaker.Import().MakeCoolGame(Graphics = ultra, " +
				"Bugs = no, Architecture = cool and optimized)";

			ConfigurationManager.Initialize();
			base.Initialize();

			inputThread.Start();
			this.Exiting += (object sender, ExitingEventArgs e) => inputThread.Abort();
		}

		protected override void LoadContent()
		{
			var windowWidth = Window.ClientBounds.Width;
			var windowHeight = Window.ClientBounds.Height;

			spriteBatch = new SpriteBatch(GraphicsDevice);

			Content.RootDirectory = "Content/Sprites";

			SceneManager.Initialize(Content, Window, InitialSceneId);

			TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / DeltaTimeMS);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
				|| Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			Window.Title = (1000000000 / gameTime.ElapsedGameTime.TotalNanoseconds).ToString();

			SceneManager.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.Black);
			SceneManager.Draw(spriteBatch);
			base.Draw(gameTime);
		}
	}
}
