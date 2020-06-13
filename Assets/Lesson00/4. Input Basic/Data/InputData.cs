using Unity.Entities;
using UnityEngine;


namespace Sample0_4
{
    [GenerateAuthoringComponent]
    public struct InputData : IComponentData
    {
        public KeyCode upKey;
        public KeyCode downKey;
        public KeyCode rightKey;
        public KeyCode leftKey;
    }
}
