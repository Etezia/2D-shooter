using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Object_components;

public class Camera : IDynamicObject
{
	public Physics ObjectPhysics { get; private set; } = new Physics(new Vector2(0, 0));
	public Transform2D Transform { get; private set; }

	public Camera(Transform2D transform)
	{
		Transform = transform;
	}
}
