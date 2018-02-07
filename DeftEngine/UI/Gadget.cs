using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace DeftEngine
{
    public class Gadget
    {
        public const int X_PADDING = 5;
        public const int Y_PADDING = 3;

        public Gadget parent;
        private List<Gadget> _children = new List<Gadget>();

        public bool isDraggable;
        public bool isResizable;
        public bool canReceiveTextInput;
        public bool alwaysShowTexture = false;

        public string backgroundTextureName;
        public string label;

        private Vector2 _pos;
        private Vector2 _size;
        private Rectangle _bounds;

        public float LastGadgetBottom
        {
            get
            {
                if (_children.Count == 0)
                    return _pos.Y;

                return _children[_children.Count - 1].Bounds.Bottom;
            }
        }

        public int layer;

        public Gadget()
        {
            backgroundTextureName = "GadgetBackground";
            Size = new Vector2(75, 50);
        }

        public Vector2 Pos
        {
            get => _pos;

            set
            {
                Vector2 delta = value - _pos;

                foreach (var child in _children)
                    child.Pos += delta;

                _pos = value;
                _bounds.Location = _pos.ToPoint();
            }
        }

        public float X
        {
            get => _pos.X;

            set
            {
                float delta = value - _pos.X;

                foreach (var child in _children)
                    child.X += delta;

                _pos.X = value;
                _bounds.X = (int)_pos.X;
            }
        }

        public float Y
        {
            get => _pos.Y;

            set
            {
                float delta = value - _pos.Y;

                foreach (var child in _children)
                    child.Y += delta;

                _pos.Y = value;
                _bounds.Y = (int)_pos.Y;
            }
        }   
        
        public Vector2 Size
        {
            get => _size;

            set
            {
                _size = value;
                _bounds.Size = _size.ToPoint();
            }
        }

        public Rectangle Bounds
        {
            get => _bounds;
        }

        public T AddGadget<T>(string label) where T : Gadget
        {
            var newGadget = (T)Activator.CreateInstance(typeof(T));
            newGadget.layer = this.layer + 1;
            newGadget.label = label;
            newGadget.isDraggable = false;
            newGadget.isResizable = false;

            // Stuff in here about sizes and such
            newGadget.Pos = new Vector2(_pos.X + X_PADDING, LastGadgetBottom + Y_PADDING);

            int overflow = newGadget.Bounds.Bottom - Bounds.Bottom;

            if (overflow > 0)
                Size += new Vector2(0, overflow + Y_PADDING);

            _children.Add(newGadget);
            DeftUI.AddGadget(newGadget);
            return newGadget;
        }

        public T GetGadget<T>(string label) where T : Gadget
        {
            return _children.Find(g => g.label == label) as T;
        }
        
        public virtual void OnSelect()
        { }

        public virtual void OnDrag()
        { }

        public virtual void OnResize()
        { }

        public virtual void OnTextEntry()
        { }

        public virtual void Display(SpriteBatch spriteBatch)
        {
            if (parent == null || alwaysShowTexture)
                spriteBatch.Draw(Assets.GetTexture(backgroundTextureName), Pos, 0, Size);
        }
    }
}
