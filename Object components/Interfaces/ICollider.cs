using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Object_components;

public interface ICollider
{
	public float X { get; }
	public float Y { get; }
	public float Width { get; }
	public float Height { get; }

	public float Right { get; }
	public float Top { get; }

	public bool Collides(ICollider collider, Vector2 position);
}
