using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace coll_test.Collisions
{
	public class CollisionHandler
	{
		public CollisionHandler()
		{
		}

		public bool Overlaps(CollisionBound first, CollisionBound second)
		{
			var axesFirst = BuildAxes(first.Edges);
			
			foreach(var a in axesFirst)
			{
				var p1 = first.ProjectToAxis(a);
				var p2 = second.ProjectToAxis(a);

				if (!p1.Overlaps(p2))
				{
					return false;
				}
			}

            var axesSecond = BuildAxes(second.Edges);

            foreach (var a in axesSecond)
            {
                var p1 = first.ProjectToAxis(a);
                var p2 = second.ProjectToAxis(a);

                if (!p1.Overlaps(p2))
                {
                    return false;
                }
            }

            return true;
		}

		public List<Vector2> BuildAxes(List<Edge> edges)
		{
			var axes = new List<Vector2>();

			foreach (var edge in edges)
			{
				var v = new Vector2(edge.First.X - edge.Second.X, edge.First.Y - edge.Second.Y);
				v.Normalize();
				axes.Add(new Vector2(v.Y, -(v.X)));
			}

			return axes;
		}
	}
}

