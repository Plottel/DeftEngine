﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public class System_Display_DebugCollisionCircle : ISystem, IDisplaySystem
    {
        public void Display(SpriteBatch spriteBatch)
        {
            var entities = ECSCore.pool.GetEntities<Component_Collision_Circle>();
            Component_Collision_Circle circle;
            Color color;

            foreach (var e in entities)
            {
                circle = e.Get<Component_Collision_Circle>();
                color = ECSCore.collisionPool.HasCollision(e) ? Color.Red : Color.LawnGreen;

                spriteBatch.DrawCircle(circle.bounds, 360, color, 1);
            }
        }
    }
}
