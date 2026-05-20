using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;

namespace Object_components;

public interface ICollidingObject
{
	public Transform2D Transform { get; }
	public ICollider Collider { get; }
}
