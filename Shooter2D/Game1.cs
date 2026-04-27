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

		private Scene currScene;

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

			player = new Player(Content.Load<Texture2D>("soldier_player_dummy"),
				new Transform2D(new Vector2(windowWidth / 2, windowHeight / 2), 
				Quaternion.Identity, new Point(64, 64)),
				new RectCollider(64, 32));

			currScene = new Scene();
			currScene.AddPlayerOnScene(player);
			currScene.CreateLevel(windowHeight, windowWidth, 0, 100, 0.1,
				Content.Load<Texture2D>("enemy_dummy1"));
			
			TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / DeltaTimeMS);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
				|| Keyboard.GetState().IsKeyDown(Keys.Escape))
			{
				Exit();
			}

			currScene.Update(gameTime);
			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.BurlyWood);
			currScene.Draw(spriteBatch);
			base.Draw(gameTime);
		}
	}
}
