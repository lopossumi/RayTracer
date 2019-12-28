using System.Numerics;
using System.Drawing;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RayTracer
{
    class Program
    {
        static Vector3 GetColorVector(Ray r, IHittable world, int depth, RandomUtil random)
        {
            if (world.Hit(r, 0.001f, float.MaxValue, out var rec))
            {
                if(depth < 50 && rec.Material.Scatter(r, rec, random, out var attenuation, out var scattered))
                {
                    return attenuation * GetColorVector(scattered, world, depth + 1, random);
                }
                return Vector3.Zero;
            }
            else
            {
                var unitDirection = Vector3.Normalize(r.Direction);
                float t = 0.5f * (unitDirection.Y + 1.0f);
                var colorVector = (1.0f - t) * Vector3.One + t * new Vector3(0.5f, 0.7f, 1.0f);
                return colorVector;
            }
        }

        static Color GetColor(Vector3 rgb)
        {
            return Color.FromArgb(
                255,
                (int)Math.Floor(255.99 * rgb.X),
                (int)Math.Floor(255.99 * rgb.Y),
                (int)Math.Floor(255.99 * rgb.Z));
        }

        static void Main(string[] args)
        {
            var timer = new Stopwatch();
            timer.Start();
            var width = 1000;
            var height = 500;
            var samples = 100;

            var world = new Scene(new List<IHittable>
            {
                new Sphere(new Vector3(0f,0f,-1f), 0.5f, new Lambertian(new Vector3(0.8f, 0.3f, 0.3f))),
                new Sphere(new Vector3(0f,-100.5f,-1f), 100f, new Lambertian(new Vector3(0.8f, 0.8f, 0.0f))),
                new Sphere(new Vector3(1f,0f,-1f), 0.5f, new Metal(new Vector3(0.8f, 0.6f, 0.2f), 0.03f)),
                new Sphere(new Vector3(-1f,0f,-1f), 0.5f, new Metal(new Vector3(0.8f, 0.8f, 0.8f), 1.0f))
            });

            var image = new Bitmap(width, height);
            var camera = new Camera();

            var array = new Vector3[width, height];

            using (var progress = new ShellProgressBar.ProgressBar(height, "Processing..."))
            {
                Parallel.For(0, height, j =>
                {
                    progress.Tick();
                    var random = new RandomUtil();
                    for (int i = 0; i < width; i++)
                    {
                        var colorVector = new Vector3(0, 0, 0);
                        for (int s = 0; s < samples; s++)
                        {
                            var u = (i + random.NextFloat()) / width;
                            var v = (j + random.NextFloat()) / height;
                            var ray = camera.GetRay(u, v);
                            colorVector += GetColorVector(ray, world, 0, random);
                        }
                        colorVector /= samples;
                        colorVector = new Vector3(
                            MathF.Sqrt(colorVector.X),
                            MathF.Sqrt(colorVector.Y),
                            MathF.Sqrt(colorVector.Z));
                        array[i, j] = colorVector;
                    }
                });
            }
            for(int j=0; j<height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    image.SetPixel(i, height - j - 1, GetColor(array[i, j]));
                }
            }

            image.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);
            Console.WriteLine($"Time: {timer.ElapsedMilliseconds}ms");
        }
    }
}
