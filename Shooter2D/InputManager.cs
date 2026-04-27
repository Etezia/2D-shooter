using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal class InputManager
	{
		public static readonly Vector2 UpVector = new Vector2(0, -1);
		public static readonly Vector2 DownVector = new Vector2(0, 1);
		public static readonly Vector2 LeftVector = new Vector2(-1, 0);
		public static readonly Vector2 RightVector = new Vector2(1, 0);

		public static event Action OnKeyUp;
		public static event Action OnKeyDown;
		public static event Action OnKeyLeft;
		public static event Action OnKeyRight;

		public void Update(int dt)
		{
			while (true)
			{
				var currKeyState = Keyboard.GetState();

				if (currKeyState.IsKeyDown(Keys.W) || currKeyState.IsKeyDown(Keys.Up))
					OnKeyUp?.Invoke();
				else if (currKeyState.IsKeyDown(Keys.S) || currKeyState.IsKeyDown(Keys.Down))
					OnKeyDown?.Invoke();
				if (currKeyState.IsKeyDown(Keys.D) || currKeyState.IsKeyDown(Keys.Right))
					OnKeyRight?.Invoke();
				else if (currKeyState.IsKeyDown(Keys.A) || currKeyState.IsKeyDown(Keys.Left))
					OnKeyLeft?.Invoke();

				Thread.Sleep(dt);
			}
		}
	}
}
