using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Object_components;

public record Physics (Vector2 velocity, ICollider collider = null)
{
	private ICollider collider = collider;
	public Vector2 Velocity { get; private set; } = velocity;

	public void SetVelocity(Vector2 velocity) => Velocity = velocity;

	public void Translate(Vector2 vector, Transform2D transform)
	{
		if (collider == null || !collider.HasCollisions(transform.Position + vector * velocity))
			transform.SetPosition(transform.Position + vector * Velocity);
	}

	public void Rotate(float angle, Transform2D transform) =>
		transform.SetRotation(transform.Rotation + angle);
}
