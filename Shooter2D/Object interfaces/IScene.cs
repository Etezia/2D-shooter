using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shooter2D;

internal interface IScene
{
	public void Update(GameTime gameTime);
	public void Draw(SpriteBatch spriteBatch);
}
