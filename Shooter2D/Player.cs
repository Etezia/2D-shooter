using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Shooter2D.InputManager;

namespace Shooter2D
{
	internal class Player : Sprite, IDynamicObject
	{
		private const float MoveStep = 150;
		
		public Player(Texture2D texture, ObjectTag tag = ObjectTag.Default)
			: base(texture, tag)
		{
		}

		public Player(Texture2D texture, Vector2 position, ObjectTag tag = ObjectTag.Default) 
			: base(texture, position, tag) 
		{
		}
		
		public void Move(float dt)
		{
			Position += InputToVector2D() * MoveStep * dt;
		}
		
		public void Move(Vector2 vector, float dt)
			=> new NotImplementedException();
	}
}
