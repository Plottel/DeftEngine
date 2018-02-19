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
        private static List<IUpdateGadget> _updateGadgets = new List<IUpdateGadget>();

        public static Gadget focus;
        public static UIFocusState focusState;
        public static AnchorPoint focusResizeAnchor;

        public static T Get<T>(string label) where T : Gadget
            => (T)_gadgets.Find(g => g.Label == label);

        public static List<Gadget> Gadgets
        {
            get => _gadgets;
        }

        public static List<IUpdateGadget> UpdateGadgets
        {
            get => _updateGadgets;
        }

        public static bool IsEmpty
        {
            get => _gadgets.Count > 0;
        }

        public static int TopLayer
        {
            get => _gadgets.Max(g => g.Layer);
        }

        public static void Subscribe(Gadget g)
        {
            if (_gadgets.Contains(g)) return; // Prevent duplicates

            _gadgets.Add(g);

            var update = g as IUpdateGadget;
            if (update != null) _updateGadgets.Add(update);
        }

        public static void Unsubscribe(Gadget g)
        {
            _gadgets.Remove(g);

            var update = g as IUpdateGadget;
            if (update != null) _updateGadgets.Remove(update);

            foreach (var child in g.Children)
                Unsubscribe(child);
        }

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
