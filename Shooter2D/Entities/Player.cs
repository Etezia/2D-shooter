using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using Object_components.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooter2D.InputManager;

namespace Shooter2D
{
	internal class Player : ISprite, IDynamicObject, IDamageableObject
	{
		public const float VelX = 15f;
		public const float VelY = 15f;

		public Transform2D Transform { get; private set; }
		public Texture2D Texture { get; private set; }
		public ICollider Collider { get; private set; }
		public Physics ObjectPhysics { get; private set; }
		public HealthState Health { get; private set; }

		public Player(Texture2D texture, Transform2D transform, 
			RectCollider collider, int maxHealth)
		{
			Texture = texture;
			Transform = transform;
			Collider = collider;
			ObjectPhysics = new Physics(new Vector2(VelX, VelY), collider);
			Health = new HealthState(maxHealth, this);

			SetDescriptions();
		}

		private void SetDescriptions()
		{
			var move = (Vector2 vector) =>
			{
				if (SceneManager.IsGodModeOn)
					ObjectPhysics.Translate(vector, Transform);
				else
					ObjectPhysics.TryTranslatePhysically(vector, Transform);
			};

			InputManager.OnKeyUp +=
				() => move(UpVector);
			InputManager.OnKeyDown +=
				() => move(DownVector);
			InputManager.OnKeyLeft +=
				() => move(LeftVector);
			InputManager.OnKeyRight +=
				() => move(RightVector);
		}
	}
}
