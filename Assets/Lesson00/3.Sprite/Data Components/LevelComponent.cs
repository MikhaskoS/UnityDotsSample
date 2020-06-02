using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;


namespace Sample0_3
{
    public struct LevelComponent : IComponentData
    {
        public float level;
    }
}
