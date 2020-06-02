using System;
using Unity.Entities;


namespace Sample1_0
{
    // Компонент по-умолчанию. Для контроля преобразования используются другие приемы
    // cм. далее
    [GenerateAuthoringComponent]
    public struct RotationSpeed : IComponentData
    {
        public float RadiansPerSecond;
    }
}
