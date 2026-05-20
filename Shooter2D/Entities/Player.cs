using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooter2D.InputManager;

namespace Shooter2D
{
	internal class Player : ISprite, IDynamicObject, ICollidingObject
	{
		public readonly float VelX = 5f;
		public readonly float VelY = 5f;

		public Transform2D Transform { get; private set; }
		public Texture2D Texture { get; private set; }
		public ICollider Collider { get; private set; }
		public Physics ObjectPhysics { get; private set; }

		public Player(Texture2D texture, Transform2D transform, 
			RectCollider collider)
		{
			Texture = texture;
			Transform = transform;
			Collider = collider;
			ObjectPhysics = new Physics(new Vector2(VelX, VelY), collider);

			SetDescriptions();
		}

		private void SetDescriptions()
		{
			InputManager.OnKeyUp +=
				() => ObjectPhysics.Translate(InputManager.UpVector, Transform);
			InputManager.OnKeyDown +=
				() => ObjectPhysics.Translate(InputManager.DownVector, Transform);
			InputManager.OnKeyLeft +=
				() => ObjectPhysics.Translate(InputManager.LeftVector, Transform);
			InputManager.OnKeyRight +=
				() => ObjectPhysics.Translate(InputManager.RightVector, Transform);
		}
	}
}
