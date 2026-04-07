using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Object_components
{
	public class Transform2D
	{
		public Vector2 Position { get; set; }
		public double Rotation { get; set; }
		public Vector2 Scale { get; set; }

		public Transform2D()
		{
			Position = Vector2.Zero;
			Rotation = 0d;
			Scale = new Vector2(400, 400);
		}

		public Transform2D(Vector2 position) : this()
		{
			Position = position;
		}

		public Transform2D(Vector2 position, Vector2 scale) : this()
		{
			Position = position;
			Scale = scale;
		}

		public Transform2D(Vector2 position, double rotation, Vector2 scale)
		{
			Position = position;
			Rotation = rotation;
			Scale = scale;
		}
	}
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