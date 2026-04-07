using Microsoft.Xna.Framework;
using System;

namespace Shooter2D
{
	internal interface IDynamicObject
	{
		public void Move(float dt);
		public void Move(Vector2 vector, float dt);
	}
}
