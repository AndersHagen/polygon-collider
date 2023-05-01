using System;
namespace coll_test.Collisions
{
	public class Projection
	{
		public float Min { get; private set; }

		public float Max { get; private set; }

		public Projection(float min, float max)
		{
			Min = min;
			Max = max;	
		}

		public bool Overlaps(Projection other)
		{
			if (Max < other.Min || Min > other.Max)
			{
				return false;
			}

			return true;
		}
	}
}

