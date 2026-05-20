using Microsoft.Xna.Framework;

namespace Object_components;

public interface IDynamicObject
{
	public Physics ObjectPhysics { get; }
	public Transform2D Transform { get; }
}