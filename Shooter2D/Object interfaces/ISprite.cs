using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Object_components;

namespace Shooter2D;

internal interface ISprite
{
	public Texture2D Texture { get; }
	public Transform2D Transform { get; }
}
