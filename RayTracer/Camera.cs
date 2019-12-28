using System.Numerics;

namespace RayTracer
{
    public class Camera
    {
        Vector3 LowerLeftCorner;
        Vector3 Horizontal;
        Vector3 Vertical;
        Vector3 Origin;

        public Camera()
        {
            LowerLeftCorner = new Vector3(-2.0f, -1.0f, -1.0f);
            Horizontal = new Vector3(4.0f, 0.0f, 0.0f);
            Vertical = new Vector3(0.0f, 2.0f, 0.0f);
            Origin = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public Ray GetRay(float u, float v) 
        {
            return new Ray(Origin, LowerLeftCorner + u * Horizontal + v * Vertical);
        }
    }
}
