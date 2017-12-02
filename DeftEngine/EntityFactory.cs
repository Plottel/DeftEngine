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
        public static Entity Default(Vector2 pos, Vector2 size, Color color)
        {
            var e = new Entity();

            var spatial = new SpatialComponent();
            spatial.pos = pos;
            spatial.size = size;

            e.AddComponent(spatial);

            var rectDisplay = new RectDisplayComponent();
            rectDisplay.color = color;

            e.AddComponent(rectDisplay);

            var vel = new VelocityComponent();
            vel.velocity = Vector2.Zero;
            vel.speed = 2f;

            e.AddComponent(vel);

            return e;
        }
    }
}
