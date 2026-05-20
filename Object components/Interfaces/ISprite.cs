using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Object_components;

public interface ISprite
{
	public Texture2D Texture { get; }
	public Transform2D Transform { get; }
}
