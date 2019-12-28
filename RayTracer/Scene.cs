using System;
using System.Collections.Generic;
using System.Text;

namespace RayTracer
{
    public class Scene : IHittable
    {
        public List<IHittable> Hittables { get; }

        public Scene(List<IHittable> hittables)
        {
            Hittables = hittables;
        }

        public bool Hit(Ray ray, float tMin, float tMax, out HitRecord record)
        {
            record = new HitRecord();
            var hitAnything = false;
            var closestHit = tMax;
            foreach(var hittable in Hittables)
            {
                if(hittable.Hit(ray, tMin, closestHit, out var objectRecord))
                {
                    hitAnything = true;
                    closestHit = objectRecord.T;
                    record = objectRecord;
                }
            }
            return hitAnything;
        }
    }
}
