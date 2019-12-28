using System;
using System.Numerics;

namespace RayTracer
{
    public class Lambertian : IMaterial
    {
        public Vector3 Albedo { get; }
       
        public Lambertian(Vector3 albedo)
        {
            Albedo = albedo;
        }

        public bool Scatter(Ray inRay, HitRecord rec, RandomUtil random,
            out Vector3 attenuation, 
            out Ray scattered)
        {
            var target = rec.P + rec.Normal + random.InUnitSphere();
            scattered = new Ray(rec.P, target - rec.P);
            attenuation = Albedo;
            return true;
        }
    }
}
