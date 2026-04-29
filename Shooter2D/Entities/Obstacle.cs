using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Shooter2D
{
	internal class Obstacle : ISprite, ICollidingObject
	{
		public Texture2D Texture { get; private set; }
		public Transform2D Transform { get; private set; }
		public ICollider Collider { get; private set; }

		public Obstacle(Texture2D texture, Transform2D transform, ICollider collider)
		{
			Texture = texture;
			Transform = transform;
			Collider = collider;
		}
	}
}
