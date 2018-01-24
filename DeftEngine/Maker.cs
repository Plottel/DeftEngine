using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public static class Maker
    {
        public static Entity MakeBlank(Vector2 position, Vector2 size)
        {
            var e = new Entity();
            e.pos = position;
            e.size = size;
            ECSCore.pool.AddEntity(e);

            return e;
        }

        public static Entity MakeEntityBeginner(Vector2 position)
        {
            // Create an Entity object ready to be configured.
            Entity newEntity = new Entity();

            // Configure the Entity according to parameters.
            newEntity.pos = position;

            // Fetch the Entity Pool so we can add the Entity to the scene.
            EntityPool entityPool = ECSCore.pool;

            // Add the Entity to the scene.
            entityPool.AddEntity(newEntity);

            // Return the Entity so the user has a reference.
            return newEntity;
        }

        public static Entity MakeEntityIntermediate(Vector2 position, Vector2 direction, float speed)
        {
            var newEntity = new Entity();

            // Configure the Entity according to parameters.
            newEntity.pos = position;

            // Add components
            var velComp = new Component_Velocity { direction = direction, speed = speed };
            newEntity.Add(velComp);

            ECSCore.pool.AddEntity(newEntity);

            return newEntity;
        }
    }
}
