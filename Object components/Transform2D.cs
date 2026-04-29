using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Object_components;

public record Transform2D(Vector2 position, float rotation,
		Point scale)
{
	public Vector2 Position { get; private set; } = position;
	public float Rotation { get; private set; } = rotation;
	public Point Scale { get; private set; } = scale;
	
	public void SetPosition(Vector2 position) => Position = position;

	public void SetRotation(float quaternion) => Rotation = quaternion;

	public void SetScale(Point scale) => Scale = scale;	 
}

//Not implemented:
/*
	private Transform2D _parent;

	private Action moveChildren;
	public event Action MoveChildren
	{
		add => moveChildren += value;
		remove => moveChildren -= value;
	}
*/

/* 
	public void SetParent(Transform2D parent)
	{
		_parent = parent;
		_parent.moveChildren += () => Console.WriteLine();
	}

	public void ClearParent()
	{
		_parent = null;
	}
*/