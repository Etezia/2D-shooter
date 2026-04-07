using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.ComponentModel.Design;

namespace Shooter2D
{
	internal abstract class Sprite
	{
		public Texture2D Texture { get; protected set; }
		public ObjectTag Tag { get; protected set; }
		public Vector2 Position { get; protected set; }
		
		public Sprite(Texture2D texture, ObjectTag tag = ObjectTag.Default)
		{
			Texture = texture;
			Position = new Vector2(0, 0);
			Tag = tag;
		}

		public Sprite(Texture2D texture, Vector2 position, ObjectTag tag = ObjectTag.Default)
		{
			Texture = texture;
			Position = position;
			Tag = tag;
		}
	}
}
