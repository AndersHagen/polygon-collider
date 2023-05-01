using System;
using Microsoft.Xna.Framework;

namespace coll_test.Collisions
{
	public static class Vector2Extensions
	{
		public static float Dot(this Vector2 v, Vector2 v2)
		{
			return v.X * v2.X + v.Y * v2.Y;
		} 
	}
}

