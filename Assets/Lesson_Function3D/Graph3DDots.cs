using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;

public class Graph3DDots : MonoBehaviour
{
    [Range(10, 100)]
    public int resolution = 10;

    public GameObject pointPrefab;
    private BlobAssetStore blobAssetStore;

    Entity ballEntityPrefab;
    EntityManager manager;

    private void Awake()
    {
        // Увидим мир с игрвыми сущностями (Entity) - не обяз. активный
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;

        // получить префаб
        blobAssetStore = new BlobAssetStore();
        //using (blobAssetStore = new BlobAssetStore())
        //{
            GameObjectConversionSettings settings =
                GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
            ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(pointPrefab, settings);
        //}

        SpawnPoints();
    }

    private void Start()
    {
       
    }

    private void OnDestroy()
    {
        blobAssetStore?.Dispose();
    }

    void SpawnPoints()
    {
        for (int i = 0; i < 3; i++)
        {
            Entity point = manager.Instantiate(ballEntityPrefab);

            MoveComponent move = new MoveComponent
            {
                position = new float3(i, 0, 0)
            };


            manager.AddComponentData(point, move);
        }
    }

    #region Function

    static Vector3 SphereDecor(float u, float v, float t)
    {
        Vector3 p;
        float pi = math.PI;

        float r = 0.8f + math.sin(pi * (6f * u + t)) * 0.1f;
        r += math.sin(pi * (4f * v + t)) * 0.1f;
        float s = r * math.cos(pi * 0.5f * v);
        p.x = s * math.sin(pi * u);
        p.y = r * math.sin(pi * 0.5f * v);
        p.z = s * math.cos(pi * u);
        return p;
    }

    #endregion
}
