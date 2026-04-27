using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Object_components;

public record Physics (Vector2 velocity)
{
	public Vector2 Velocity { get; private set; } = velocity;

	public void SetVelocity(Vector2 velocity) => Velocity = velocity;

	public void Translate(Vector2 vector, Transform2D transform) => 
		transform.SetPosition(transform.Position + vector * Velocity);

	public void Rotate(Quaternion quaternion, Transform2D transform) =>
		transform.SetRotation(transform.Rotation + quaternion);
}
