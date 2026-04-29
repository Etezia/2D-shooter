using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
		scene.GameObjects.Add(player);
		scene.CameraPhysics.SetVelocity(new Vector2(player.VelX, player.VelY));

		InputManager.OnKeyUp +=
			() => scene.CameraPhysics.Translate(InputManager.DownVector, scene.Transform);
		InputManager.OnKeyDown +=
			() => scene.CameraPhysics.Translate(InputManager.UpVector, scene.Transform);
		InputManager.OnKeyLeft +=
			() => scene.CameraPhysics.Translate(InputManager.RightVector, scene.Transform);
		InputManager.OnKeyRight +=
			() => scene.CameraPhysics.Translate(InputManager.LeftVector, scene.Transform);
	}
}
