using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public enum UIFocusState
    {
        None,
        Dragging,
        Resizing,
        EnteringText
    }

    public static class DeftUI
    {
        private static List<Gadget> _gadgets = new List<Gadget>();
        public static Gadget focus;
        public static UIFocusState focusState;
        public static AnchorPoint focusResizeAnchor;

        public static List<Gadget> Gadgets
        {
            get => _gadgets;
        }

        public static bool IsEmpty
        {
            get => _gadgets.Count > 0;
        }

        public static void Subscribe(Gadget g)
            => _gadgets.Add(g);

        public static void Unsubscribe(Gadget g)
            => _gadgets.Remove(g);

        /// <summary>
        /// Returns gadgets sorted by layer.
        /// Highest layer gadgets at the start of the list.
        /// </summary>
        public static List<Gadget> FrontToBackGadgets
        {
            get => _gadgets.OrderByDescending(g => g.Layer).ToList();
        }

        public static List<Gadget> BackToFrontGadgets
        {
            get => _gadgets.OrderBy(g => g.Layer).ToList();
        }


    }
}
