Asteroid Game

Part 1. Basic Input and Movement

Part 2. Tags
https://www.youtube.com/watch?v=6iNXkR3ukD0
- Движение и вращение вешаются на разные Game Objects
- Вращение на угол у астероида 
- Теги и фильтрация по тегам

Part 3. ComponentDataFromEntity
- GetComponentDataFromEntity<Translation>(true) - получение даанных Entity

Part 4. EntytyQuery
https://www.youtube.com/watch?v=IuoKWupJ6zo&t=192s
- EntytyQuery (7.05)
- Жизненный цикл System (OnCreate - OnUpdate ...) (9.42)
- Порядок обновления System (System Update Order) (12.00)
  [Update Before]
  [Update After]
  [UpdateInGroup]

Part 5. Trigger Events
https://www.youtube.com/watch?v=jga3nj_Ott4&t=583s
- Сравнение компонент Physics и DOTS Physics
- PhysicWords (3.30) : BuildPhysicsWorld-StepPhysicsWorld-ExportPhysicsWorld
- PhysicBody (6.03)
- PhysicShape (6.28)
- PhysicsDebugDisplay (7.06) - чтобы были видны коллайдеры
- ITriggerEventsJob (11.01)
- [UpdateAfter(typeof(EndFramePhysicsSystem))]  (17.25)
- EndSimulationEntityCommandBufferSystem (21.05)