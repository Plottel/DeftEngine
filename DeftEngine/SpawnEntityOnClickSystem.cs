using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class SpawnEntityOnClickSystem : IEntitySystem, IEventSystem
    {
        public IEntityQuery GetQuery()
            => EntityPool.QUERY_NO_ENTITIES;

        public bool ShouldProcess(ECSData ecsData)
            => Input.LeftMouseClicked();

        public void Process(ECSData ecsData)
        {
            // We can assume left mouse click is already down.

            // NOTE: It's only okay to add an entity this way since we're adding it to the ECSCore immediately after instantiation.
            ecsData.pool.AddEntity(MakeDefaultEntity());
        }

        private Entity MakeDefaultEntity()
        {
            var newEntity = new Entity();
            var spatial = new SpatialComponent();
            var rectDisplay = new RectDisplayComponent();

            spatial.pos = Input.MousePos;
            spatial.size = new Vector2(50, 50);
            spatial.rotation = 0;

            rectDisplay.color = Color.Blue;

            // TODO: BIG BIG BIG COUPLING PROBLEM HERE
            // Every time I add a component... it triggers on the ECS core and the Entity Pool and reruns queries and such.
            // DeCouple Entities from ECSCore.
            // Really need something like entityPool.AddComponent(Entity e, IComponent c);
            newEntity.AddComponent(spatial);
            newEntity.AddComponent(rectDisplay);

            return newEntity;
        }
    }
}
