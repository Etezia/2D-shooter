using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Shooter2D
{
	internal interface IGameObject
	{
		
	}

	public enum ObjectTag
	{
		Default,
		Player,
		Enemy,
		Wall,
		PlayerProjectile,
		EnemyProjectile
	}
}
