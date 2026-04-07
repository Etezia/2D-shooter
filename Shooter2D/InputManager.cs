using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal static class InputManager
	{
		public static Vector2 InputToVector2D()
		{
			var x = 0f;
			var y = 0f;
			var currKeyState = Keyboard.GetState();

			if (currKeyState.IsKeyDown(Keys.W) || currKeyState.IsKeyDown(Keys.Up))
				y = -1;
			else if (currKeyState.IsKeyDown(Keys.S) || currKeyState.IsKeyDown(Keys.Down))
				y = 1;

			if (currKeyState.IsKeyDown(Keys.D) || currKeyState.IsKeyDown(Keys.Right))
				x = 1;
			else if (currKeyState.IsKeyDown(Keys.A) || currKeyState.IsKeyDown(Keys.Left))
				x = -1;

			return new Vector2(x, y);
		}
	}
}
