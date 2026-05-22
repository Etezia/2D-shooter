using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Object_components;
using Object_components.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal class Enemy : ISprite, IDynamicObject, IDamageableObject
	{
		private const float velX = 2.0f;
		private const float velY = 2.0f;

		public Texture2D Texture { get; private set; }
		public Transform2D Transform { get; private set; }
		public ICollider Collider { get; private set; }
		public Physics ObjectPhysics { get; private set; }
		public HealthState Health { get; private set; }

		public Enemy(Texture2D texture, Transform2D transform, 
			RectCollider collider, int maxHealth) 
		{
			Texture = texture;
			Transform = transform;
			Collider = collider;
			ObjectPhysics = new Physics(new Vector2(velX, velY), collider);
			Health = new HealthState(maxHealth, this);
		}
	}
}
