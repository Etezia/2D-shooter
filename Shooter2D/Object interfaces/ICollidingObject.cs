using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;

namespace Shooter2D
{
	internal interface ICollidingObject
	{
		public ICollider Collider { get; }
	}
}
