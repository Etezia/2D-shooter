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

	public static int CurrSceneId { get; private set; } = 0;

	private static LevelScene mainScene;

	public static void Initialize(ContentManager content, GameWindow window, Player player = null)
	{
		contentManager = content;
		gameWindow = window;

		mainScene = new LevelScene(content);
		if (player != null) 
			mainScene.AddPlayerOnScene(player);

		AddScenesToList();
	}

	private static void AddScenesToList()
	{
		scenes.Add(mainScene);
	}

	public static void Update(GameTime gameTime)
	{
		var a = scenes[CurrSceneId];
		//a.GetHashCode();
		a.Update(gameTime);
	}

	public static void Draw(SpriteBatch spriteBatch) =>
		scenes[CurrSceneId].Draw(spriteBatch);

	public static void AddPlayerOnScene(this LevelScene scene, Player player)
	{
		player.Collider.SetCollidingObjects(scene.GameObjectDict[typeof(ICollidingObject)]);
		scene.GameObjects.Add(player);
		scene.ObjectPhysics.SetVelocity(new Vector2(player.VelX, player.VelY));

		InputManager.OnKeyUp +=
			() => scene.ObjectPhysics.Translate(InputManager.DownVector, scene.Transform);
		InputManager.OnKeyDown +=
			() => scene.ObjectPhysics.Translate(InputManager.UpVector, scene.Transform);
		InputManager.OnKeyLeft +=
			() => scene.ObjectPhysics.Translate(InputManager.RightVector, scene.Transform);
		InputManager.OnKeyRight +=
			() => scene.ObjectPhysics.Translate(InputManager.LeftVector, scene.Transform);
	}
}
