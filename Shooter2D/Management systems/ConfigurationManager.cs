using System;
using System.Text.Json;

namespace Shooter2D;

internal static class ConfigurationManager
{
	public static class Render
	{
		public static class Layers
		{
			public static readonly float Floor = 1.0f;
			public static readonly float Player = 0.3f;
			public static readonly float Enemy = 0.35f;
			public static readonly float Wall = 0.9f;
			public static readonly float Collectables = 0.6f;
			public static readonly float Environment = 0.5f;
			public static readonly float Weapon = 0.2f;

		}

		public static class Graphics
		{
			public static class ScreenResolution
			{
				public static readonly int Width = 1280;
				public static readonly int Height = 720;
			}
		}
	}

	public static void Initialize()
	{
		
	}
}
