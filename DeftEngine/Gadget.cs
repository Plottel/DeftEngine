﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeftEngine
{
    public class Gadget
    {
        public Gadget parent;
        private List<Gadget> _children = new List<Gadget>();

        public bool isDraggable;
        public bool isResizable;

        private Vector2 _pos;
        private Rectangle _bounds;

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

        public Rectangle Bounds
        {
            get => _bounds;
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
        {}
    }
}
