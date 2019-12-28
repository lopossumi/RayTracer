using System;
namespace RayTracer
{
    public interface IHittable
    {
        public bool Hit(Ray ray, float tMin, float tMax, out HitRecord record);
    }
}
