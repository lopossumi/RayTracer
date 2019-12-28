using System.Numerics;

namespace RayTracer
{
    public interface IMaterial
    {
        public bool Scatter(Ray inRay, HitRecord hitRecord, RandomUtil random, 
            out Vector3 attenuation, 
            out Ray scattered);
    }
}
