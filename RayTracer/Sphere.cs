using System;
using System.Numerics;

namespace RayTracer
{
    public class Sphere : IHittable
    {
        public Vector3 Center { get; }
        public float Radius { get; }
        public IMaterial Material;

        public Sphere(Vector3 center, float radius, IMaterial material)
        {
            Center = center;
            Radius = radius;
            Material = material;
        }

        public bool Hit(Ray ray, float tMin, float tMax, out HitRecord record)
        {
            record = new HitRecord();
            var oc = ray.Origin - Center;
            var a = Vector3.Dot(ray.Direction, ray.Direction);
            var b = Vector3.Dot(oc, ray.Direction);
            var c = Vector3.Dot(oc, oc) - Radius * Radius;
            float discriminant = b * b - a * c;

            if (discriminant > 0)
            {
                var t = (-b - MathF.Sqrt(discriminant)) / a;
                if(t < tMax && t > tMin)
                {
                    record.T = t;
                    record.P = ray.PointAtParameter(record.T);
                    record.Normal = (record.P - Center) / Radius;
                    record.Material = Material;
                    return true;
                }
                t = (-b + MathF.Sqrt(discriminant)) / a;
                if (t < tMax && t > tMin)
                {
                    record.T = t;
                    record.P = ray.PointAtParameter(record.T);
                    record.Normal = (record.P - Center) / Radius;
                    record.Material = Material;
                    return true;
                }
            }
            record.P = new Vector3();
            record.Normal = new Vector3();
            record.T = 0;
            record.Material = Material;
            return false;
        }
    }
}
