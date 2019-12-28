using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracer
{
    public class RandomUtil
    {
        private Random random;
        public RandomUtil()
        {
            random = new Random();
        }

        public Vector3 InUnitSphere()
        {
            Vector3 p;
            do
            {
                var x = (float)random.NextDouble();
                var y = (float)random.NextDouble();
                var z = (float)random.NextDouble();
                p = 2.0f * new Vector3(x, y, z) - Vector3.One;
            } while (p.LengthSquared() >= 1.0);
            return p;
        }

        public float NextFloat()
        {
            return (float) random.NextDouble();
        }
    }
}
