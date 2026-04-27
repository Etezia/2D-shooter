using Microsoft.Xna.Framework;
using Object_components;

namespace Shooter2D
{
	internal interface IDynamicObject
	{
		public Physics Physics { get; }
		public Transform2D Transform { get; }
	}
}