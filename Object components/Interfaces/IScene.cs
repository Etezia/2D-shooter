using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Object_components;

public interface IScene
{

	public List<object> GameObjects { get; }
	public void Update(GameTime gameTime);
	public void Draw(SpriteBatch spriteBatch);
}
