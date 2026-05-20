using System;
using System.Text.Json;

namespace Shooter2D;

internal static class ConfigurationManager
{
	public static class Layers
    {
        public static readonly float Floor = 1.0f;
        public static readonly float Player = 0.3f;
		public static readonly float Enemy = 0.3f;
		public static readonly float Wall = 0.9f;
		public static readonly float Collectables = 0.6f;
		public static readonly float Environment = 0.5f;
		public static readonly float Weapon = 0.2f;

	}

	public static void Initialize()
	{
		
	}
}
