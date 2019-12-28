using System.Numerics;

namespace RayTracer
{
    public struct HitRecord
    {
        public float T;
        public Vector3 P;
        public Vector3 Normal;
        public IMaterial Material;
    }
}