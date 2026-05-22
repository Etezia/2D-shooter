using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Object_components;

namespace Shooter2D;

internal static class SceneManager
{
	private static List<IScene> scenes = new List<IScene>();
	private static ContentManager contentManager;
	private static GameWindow gameWindow;

	private static int WindowWidth;
	private static int WindowHeight;

	public static int CurrSceneId { get; private set; } = 0;

	private static MainMenuScene mainMenuScene;
	private static LevelScene mainScene;

	public static void Initialize(ContentManager content, GameWindow window, Player player = null)
	{
		contentManager = content;
		gameWindow = window;
		WindowWidth = gameWindow.ClientBounds.Width;
		WindowHeight = gameWindow.ClientBounds.Height;

		mainMenuScene = new MainMenuScene(content);

		mainScene = new LevelScene(content);
		if (player != null) 
			mainScene.AddPlayerOnScene(player);

		AddScenesToList();
	}

	private static void AddScenesToList()
	{
		scenes.Add(mainMenuScene);
		scenes.Add(mainScene);
	}

	public static void Update(GameTime gameTime)
	{
		lock (scenes[CurrSceneId])
		{
			var a = scenes[CurrSceneId];
			a.Update(gameTime);
		}
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		lock (scenes[CurrSceneId])
		{
			scenes[CurrSceneId].Draw(spriteBatch);
		}
	}

	public static void AddPlayerOnScene(this LevelScene scene, Player player)
	{
		player.Collider.SetCollidingObjects(scene.GameObjectDict[typeof(ICollidingObject)]);
		scene.GameObjects.Add(player);
		scene.MainCamera.ObjectPhysics.SetVelocity(new Vector2(player.VelX, player.VelY));

		player.Transform.OnPositionReassigned +=
			() => scene.MainCamera.Transform
				.SetPosition(-player.Transform.Position + 
				new Vector2(WindowWidth / 2, WindowHeight / 2));
	}
}
