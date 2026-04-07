using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Object_components;

namespace Shooter2D
{
	public class Game1 : Game
	{
		private GraphicsDeviceManager _graphics;
		private SpriteBatch _spriteBatch;
		private const int DeltaTimeMS = 40;

		private Player _player;
		private Sprite _floor;

		public Game1()
		{
			_graphics = new GraphicsDeviceManager(this);
			Content.RootDirectory = "Content";
			IsMouseVisible = true;
		}

		protected override void Initialize()
		{
			// TODO: Add your initialization logic here

			base.Initialize();
		}

		protected override void LoadContent()
		{
			_spriteBatch = new SpriteBatch(GraphicsDevice);

			Content.RootDirectory = "Content/Sprites";
			_player = new Player(Content.Load<Texture2D>("dummy1"),
				new Vector2(Window.ClientBounds.Width / 2, Window.ClientBounds.Height / 2), ObjectTag.Player);
			//_floor = new Sprite(Content.Load<Texture2D>("floor-dummy1"), new Vector2(100, 0));

			TargetElapsedTime = new System.TimeSpan(0, 0, 0, 0, 1000 / DeltaTimeMS);
		}

		protected override void Update(GameTime gameTime)
		{
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed 
				|| Keyboard.GetState().IsKeyDown(Keys.Escape))
				Exit();

			var dt = gameTime.ElapsedGameTime.TotalSeconds;

			_player.Move((float)dt);

			base.Update(gameTime);
		}

		protected override void Draw(GameTime gameTime)
		{
			GraphicsDevice.Clear(Color.BurlyWood);

			_spriteBatch.Begin(samplerState: SamplerState.PointWrap);

			//_spriteBatch.Draw(_floor.Texture, _floor.Position, Color.White);
			_spriteBatch.Draw(_player.Texture, _player.Position, Color.White);

			_spriteBatch.End();

			base.Draw(gameTime);
		}
	}
}
