using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D
{
	internal class Enemy : Sprite, IDynamicObject
	{
		public Enemy(Texture2D texture, ObjectTag tag = ObjectTag.Default) 
			: base(texture, tag)
		{
		}

		public Enemy(Texture2D texture, Vector2 position, ObjectTag tag = ObjectTag.Default) 
			: base(texture, position, tag)
		{
		}

		void IDynamicObject.Move(float dt)
		{
			throw new NotImplementedException();
		}

		void IDynamicObject.Move(Vector2 vector, float dt)
		{
			throw new NotImplementedException();
		}
	}
}
