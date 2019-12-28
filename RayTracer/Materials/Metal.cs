using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace RayTracer
{
    public class Metal : IMaterial
    {
        public Vector3 Albedo { get; }
        public float Fuzz { get; }

        public Metal(Vector3 albedo, float fuzz = 0.0f)
        {
            Albedo = albedo;
            Fuzz = fuzz;
        }

        public bool Scatter(Ray inRay, HitRecord rec, RandomUtil random, 
            out Vector3 attenuation, 
            out Ray scattered)
        {
            var reflected = Reflect(Vector3.Normalize(inRay.Direction), rec.Normal);
            scattered = new Ray(rec.P, reflected+Fuzz*random.InUnitSphere());
            attenuation = Albedo;
            return Vector3.Dot(scattered.Direction, rec.Normal) > 0;
        }

        private Vector3 Reflect(Vector3 v, Vector3 n)
        {
            return v - 2 * Vector3.Dot(v, n) * n;
        }
    }
}
