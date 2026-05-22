using Microsoft.Xna.Framework.Graphics;
using Object_components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D.Entities
{
	internal class Button : ISprite
	{
		public Texture2D Texture { get; private set; }
		public Transform2D Transform { get; private set; }

		public Button(Texture2D texture, Transform2D transform)
		{
			Texture = texture;
			Transform = transform;
		}
	}
}
