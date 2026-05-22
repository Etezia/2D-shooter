using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_components
{
	public class RectCollider : ICollider
	{
		private const int Offset = 0;

		public float X { get; private set; }
		public float Y { get; private set; }
		public float Width { get; private set; }
		public float Height { get; private set; }

		public float Right { get => X + Width; }
		public float Bottom { get => Y + Height; }

		private IEnumerable<object> collidingObjects = new List<object>();

		public RectCollider(float width, float height, float x = 0, float y = 0)
		{
			Width = width;
			Height = height;
			X = x;
			Y = y;
		}

		public void SetCollidingObjects(IEnumerable<object> objects)
		{
			collidingObjects = objects;
		}

		public bool HasCollisions(Vector2 position)
		{
			var currLeft = position.X + X - Offset;
			var currTop = position.Y + Y - Offset;
			var currRight = position.X + Right + Offset;
			var currBottom = position.Y + Bottom + Offset;

			foreach (var obj in collidingObjects.OfType<ICollidingObject>())
			{
				if (obj.Collider is RectCollider)
				{
					var collider = obj.Collider;
					var objPositionX = obj.Transform.Position.X;
					var objPositionY = obj.Transform.Position.Y;

					if (currLeft < collider.Right + objPositionX &&
						currRight > collider.X + objPositionX &&
						currBottom > collider.Y + objPositionY &&
						currTop < collider.Bottom + objPositionY)
						return true;
				}
			}
			return false;
		}
	}
}
