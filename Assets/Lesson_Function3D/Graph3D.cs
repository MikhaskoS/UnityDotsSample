using UnityEngine;


namespace Function3D
{
    public enum GraphFunctionName
    {
        SineFunction, Sine2DFunction, MultiSineFunction, Ripple, Sphere, SphereDecor
    }

    public class Graph3D : MonoBehaviour
    {
        [Range(10, 100)]
        public int resolution = 10;
        public GraphFunctionName function;
        Transform[] points;
        public Transform pointPrefab;
        const float pi = Mathf.PI;

        static GraphFunction[] functions = {
            SineFunction, Sine2DFunction, MultiSineFunction, Ripple, Sphere, SphereDecor
        };

        public delegate Vector3 GraphFunction(float u, float v, float t);

        #region Function

        static Vector3 SineFunction(float x, float z, float t)
        {
            Vector3 p;
            p.x = x;
            p.y = Mathf.Sin(pi * (x + t));
            p.z = z;
            return p;
        }

        static Vector3 Sine2DFunction(float x, float z, float t)
        {
            Vector3 p;
            p.x = x;
            p.y = Mathf.Sin(pi * (x + t));
            p.y += Mathf.Sin(pi * (z + t));
            p.y *= 0.5f;
            p.z = z;
            return p;
        }

        static Vector3 MultiSineFunction(float x, float z, float t)
        {
            Vector3 p;
            p.x = x;
            p.y = Mathf.Sin(pi * (x + t));
            p.y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
            p.y *= 2f / 3f;
            p.z = z;
            return p;
        }

        static Vector3 Ripple(float x, float z, float t)
        {
            Vector3 p;
            float d = Mathf.Sqrt(x * x + z * z);
            p.x = x;
            p.y = Mathf.Sin(pi * (4f * d - t));
            p.y /= 1f + 10f * d;
            p.z = z;
            return p;
        }

        static Vector3 Sphere(float u, float v, float t)
        {
            Vector3 p;
            float r = Mathf.Cos(pi * 0.5f * v);
            p.x = r * Mathf.Sin(pi * u);
            p.y = Mathf.Sin(pi * 0.5f * v);
            p.z = r * Mathf.Cos(pi * u);
            return p;
        }

        static Vector3 SphereDecor(float u, float v, float t)
        {
            Vector3 p;
            float r = 0.8f + Mathf.Sin(pi * (6f * u + t)) * 0.1f;
            r += Mathf.Sin(pi * (4f * v + t)) * 0.1f;
            float s = r * Mathf.Cos(pi * 0.5f * v);
            p.x = s * Mathf.Sin(pi * u);
            p.y = r * Mathf.Sin(pi * 0.5f * v);
            p.z = s * Mathf.Cos(pi * u);
            return p;
        }

        #endregion

        private void Awake()
        {
            float step = 2f / resolution;
            Vector3 scale = Vector3.one * step;

            points = new Transform[resolution * resolution];

            for (int i = 0; i < points.Length; i++)
            {
                Transform point = Instantiate(pointPrefab);
                point.localScale = scale;
                point.SetParent(transform, false);
                points[i] = point;
            }
        }

        // Update is called once per frame
        void Update()
        {
            float t = Time.time;
            GraphFunction f = functions[(int)function];
            float step = 2f / resolution;
            for (int i = 0, z = 0; z < resolution; z++)
            {
                float v = (z + 0.5f) * step - 1f;
                for (int x = 0; x < resolution; x++, i++)
                {
                    float u = (x + 0.5f) * step - 1f;
                    points[i].localPosition = f(u, v, t);
                }
            }
        }
    }
}
