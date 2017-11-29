using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public static class EntityFactory
    {
        public static Entity Fighter(Vector2 pos)
        {
            // Add Entity adds to subcomponent pool
            // Add Component adds to subcomponent pool
            // Duplication


            var e = SpatialRectDisplay(pos, new Vector2(50, 50), Color.Blue);

            ECSCore.pool.AddEntity(e);



            e.AddComponent<VelocityComponent>();
            e.Get<VelocityComponent>().speed = SimParams.FIGHTER_SPEED;
            e.AddComponent<FighterComponent>();
            e.AddComponent<NeuralNetComponent>();

            return e;
        }

        public static Entity Bullet(Vector2 pos, Vector2 vel, float speed, Entity shooter)
        {
            var e = SpatialRectDisplay(pos, new Vector2(10, 10), Color.Red);

            var velComp = new VelocityComponent();
            velComp.velocity = Vector2.Normalize(vel);
            velComp.speed = speed;

            e.AddComponent(velComp);

            var bullet = new BulletComponent();
            bullet.shooter = shooter;

            e.AddComponent(bullet);

            ECSCore.pool.AddEntity(e);

            return e;
        }


        private static Entity SpatialRectDisplay(Vector2 pos, Vector2 size, Color color)
        {
            var e = new Entity();

            var spatial = new SpatialComponent();
            spatial.pos = pos;
            spatial.size = size;

            e.AddComponent(spatial);

            var rectDisplay = new RectDisplayComponent();
            rectDisplay.color = color;

            e.AddComponent(rectDisplay);

            return e;
        }
    }
}
