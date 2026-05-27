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
	public static bool IsGodModeOn = true;

	private static List<IScene> scenes = new List<IScene>();
	private static List<Func<IScene>> sceneLoaders = new List<Func<IScene>>()
	{
		() => new MainMenuScene(contentManager),
		() => new LevelScene(contentManager)
	};
	private static ContentManager contentManager;
	private static GameWindow gameWindow;
	public static int WindowWidth => gameWindow.ClientBounds.Width;
	public static int WindowHeight => gameWindow.ClientBounds.Height;

	private static int currSceneId;
	public static int CurrSceneId {
		get { return currSceneId; }
		set {
			if (value >= sceneLoaders.Count)
				throw new IndexOutOfRangeException("Scene with this index doesn't exist");

			currSceneId = value;
			currScene = sceneLoaders[value]();
		}
	}

	public static IScene currScene { get; private set; }

	public static void Initialize(ContentManager content, GameWindow window, int initialSceneId)
	{
		if (sceneLoaders.Count == 0)
			throw new ContentLoadException("No scene to load");

		contentManager = content;
		gameWindow = window;
		CurrSceneId = initialSceneId;
	}

	public static void Update(GameTime gameTime)
	{
		lock (currScene)
		{
			currScene.Update(gameTime);
		}
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		lock (currScene)
		{
			currScene.Draw(spriteBatch);
		}
	}
}