using UnityEngine;

namespace Function2D
{
	public enum GraphFunctionName
	{
		Sine,
		Sine2D,
		MultiSine,
		MultiSine2D
	}

	// https://catlikecoding.com/unity/tutorials/basics/mathematical-surfaces/
	public class Graph : MonoBehaviour
	{
		[Range(10, 100)]
		public int resolution = 10;
		public GraphFunctionName function;
		Transform[] points;
		public Transform pointPrefab;
		const float pi = Mathf.PI;

		static GraphFunction[] functions = {
		SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction
	};

		public delegate float GraphFunction(float x, float z, float t);

		#region Methods

		static float SineFunction(float x, float z, float t)
		{
			return Mathf.Sin(pi * (x + t));
		}

		static float MultiSineFunction(float x, float z, float t)
		{
			float y = Mathf.Sin(pi * (x + t));
			y += Mathf.Sin(2f * pi * (x + 2f * t)) / 2f;
			y *= 2f / 3f;
			return y;
		}

		static float Sine2DFunction(float x, float z, float t)
		{
			//return Mathf.Sin(pi * (x + z + t));
			float y = Mathf.Sin(pi * (x + t));
			y += Mathf.Sin(pi * (z + t));
			y *= 0.5f;
			return y;
		}

		static float MultiSine2DFunction(float x, float z, float t)
		{
			float y = 4f * Mathf.Sin(pi * (x + z + t * 0.5f));
			y += Mathf.Sin(pi * (x + t));
			y += Mathf.Sin(2f * pi * (z + 2f * t)) * 0.5f;
			y *= 1f / 5.5f;
			return y;
		}
		#endregion

		#region UnityMethods

		private void Awake()
		{
			points = new Transform[resolution * resolution];
			float step = 2f / resolution;
			Vector3 scale = Vector3.one * step;
			Vector3 position;
			position.y = 0f;
			position.z = 0f;

			for (int i = 0, z = 0; z < resolution; z++)
			{
				position.z = (z + 0.5f) * step - 1f;
				for (int x = 0; x < resolution; x++, i++)
				{
					Transform point = Instantiate(pointPrefab);
					position.x = (x + 0.5f) * step - 1f;
					point.localPosition = position;
					point.localScale = scale;
					point.SetParent(transform, false);
					points[i] = point;
				}
			}
		}

		void Update()
		{
			float t = Time.time;
			GraphFunction f = functions[(int)function];

			for (int i = 0; i < points.Length; i++)
			{
				Transform point = points[i];
				Vector3 position = point.localPosition;

				position.y = f(position.x, position.z, t);
				point.localPosition = position;
			}
		}

		#endregion
	}
}
