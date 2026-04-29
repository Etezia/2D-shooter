using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Object_components;

namespace Shooter2D;

internal interface IScene
{

	public List<object> GameObjects { get; }
	public void Update(GameTime gameTime);
	public void Draw(SpriteBatch spriteBatch);
}
