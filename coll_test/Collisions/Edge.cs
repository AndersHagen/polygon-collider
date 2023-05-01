using System;
using Microsoft.Xna.Framework;

namespace coll_test.Collisions
{
	public class Edge
	{
		public Vector2 First { get; private set; }

        public Vector2 Second { get; private set; }

        public Edge(Vector2 first, Vector2 second)
		{
			First = first;
			Second = second;
		}
	}
}

