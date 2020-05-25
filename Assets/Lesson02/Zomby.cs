using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Sample3
{
    public class Zomby : MonoBehaviour
    {
        [SerializeField] Transform pfZombie = null;
        public List<Zombie> zombieList;

        public class Zombie
        {
            public Transform transform;
            public float moveY;
        }


        void Start()
        {
            zombieList = new List<Zombie>();
            for (int i = 0; i < 10000; i++)
            {
                Transform zombieTransform =
                    Instantiate(pfZombie,
                    new Vector3(Random.Range(-8f, 8f), Random.Range(-4f, 4f)),
                    Quaternion.identity);
                zombieList.Add(new Zombie { transform = zombieTransform, moveY = Random.Range(1f, 2f) });
            }
        }


        void Update()
        {
            float startTime = Time.realtimeSinceStartup;

            foreach (Zombie zombie in zombieList)
            {
                zombie.transform.position += new Vector3(0, zombie.moveY * Time.deltaTime);
                if (zombie.transform.position.y > 5f)
                    zombie.moveY = -math.abs(zombie.moveY);
                if (zombie.transform.position.y < -5f)
                    zombie.moveY = math.abs(zombie.moveY);
            }

            Debug.Log(((Time.realtimeSinceStartup - startTime) * 1000f) + "ms");
        }
    }
}
