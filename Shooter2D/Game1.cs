using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Object_components;
using System;
using System.Threading;

namespace Shooter2D
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager graphics;
		private SpriteBatch spriteBatch;
		private const int DeltaTimeMS = 40;
		
		private Player player;
		private ISprite floor;

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
			graphics.PreferredBackBufferHeight = 720;
			graphics.PreferredBackBufferWidth = 1280;
			graphics.ApplyChanges();

			Window.Title = "GameMaker.Import().MakeCoolGame(Graphics = ultra, " +
				"Bugs = no, Architecture = cool and optimized)";

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

			player = new Player(Content.Load<Texture2D>("dummy1"),
				new Transform2D(new Vector2(windowWidth / 2, windowHeight / 2), 
				0f, new Point(50, 50)), new RectCollider(50, 25));

			SceneManager.Initialize(Content, Window, player);

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
			GraphicsDevice.Clear(Color.BurlyWood);
			SceneManager.Draw(spriteBatch);
			base.Draw(gameTime);
		}
	}
}
