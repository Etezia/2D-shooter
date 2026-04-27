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
		public float X { get; private set; }
		public float Y { get; private set; }
		public float Width { get; private set; }
		public float Height { get; private set; }

		public float Right { get => X + Width; }
		public float Top { get => Y - Height; }

		public RectCollider(float width, float height, float x = 0, float y = 0)
		{
			Width = width;
			Height = height;
			X = x;
			Y = y;
		}

		public bool Collides(ICollider collider, Vector2 position)
		{
			if (collider is RectCollider)
				return X + position.X < collider.Right + position.X &&
				Right + position.X > collider.X + position.X &&
				Y + position.Y > collider.Top + position.Y &&
				collider.Y + position.Y > Top + position.Y;
			return false;
		}
	}
}
