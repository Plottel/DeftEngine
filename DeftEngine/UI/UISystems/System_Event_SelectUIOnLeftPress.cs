using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class System_Event_SelectUIOnLeftPress : ISystem, IEventSystem
    {
        public void SubscribeToEvents(EventPool eventPool)
            => eventPool.SubscribeTo<Event_OnLeftMousePress>(this);

        public void OnEvent(DeftEvent theEvent, params object[] args)
        {
            var selectableEntities = ECSCore.pool.GetEntities<Component_UI_Selectable>();
            var selectedEntities = ECSCore.pool.GetEntities<Component_UI_Selected>();
            Rectangle uiBounds;
            Vector2 mousePos = Input.MousePos;

            var newSelectionEntities = new List<Entity>();

            // Determine which entities are part of the new selection.
            foreach (var e in selectableEntities)
            {
                uiBounds = new Rectangle(e.Pos.ToPoint(), e.Size.ToPoint());

                if (uiBounds.Contains(Input.MousePos))
                    newSelectionEntities.Add(e);
            }

            // If a selection was made, remove previous selection so only new selection remains.
            if (newSelectionEntities.Count > 0)
            {
                for (int i = selectedEntities.Count - 1; i >= 0; --i)
                    selectedEntities[i].Remove<Component_UI_Selected>();
            }            

            // Add components to new selection
            for (int i = newSelectionEntities.Count - 1; i >= 0; --i)
                newSelectionEntities[i].Add<Component_UI_Selected>();
        }
    }
}
