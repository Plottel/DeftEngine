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
        public bool inheritSizeX = true;
        public bool inheritSizeY = false;

        public string backgroundTextureName;

        protected string _label;
        public virtual string Label
        {
            get => _label;
            set => _label = value;
        }

        private Vector2 _pos;
        private Vector2 _size;
        private Rectangle _bounds;

        public int layer;

        public Gadget()
        {
            backgroundTextureName = "GadgetBackground";
            SetSize(new Vector2(75, 50));
            Label = "";
        }

        public float LastGadgetBottom
        {
            get
            {
                if (_children.Count == 0)
                    return _pos.Y;

                return _children[_children.Count - 1].Bounds.Bottom;
            }
        }

        public Vector2 Pos
        {
            get => _pos;            
        } 
        
        public Vector2 Size
        {
            get => _size;
        }

        public Rectangle Bounds
        {
            get => _bounds;
        }

        public T AddGadget<T>(string label) where T : Gadget
        {
            var newGadget = (T)Activator.CreateInstance(typeof(T));
            newGadget.parent = this;
            newGadget.SetSize(new Vector2(Size.X - (X_PADDING * 2), newGadget.Size.Y));
            newGadget.layer = this.layer + 1;
            newGadget.Label = label;
            newGadget.isDraggable = false;
            newGadget.isResizable = false;

            // Stuff in here about sizes and such
            newGadget.MoveTo(new Vector2(_pos.X + X_PADDING, LastGadgetBottom + Y_PADDING));

            int overflowY = newGadget.Bounds.Bottom - Bounds.Bottom;
            if (overflowY > 0)
                Resize(new Vector2(0, overflowY + Y_PADDING));

            int overflowX = newGadget.Bounds.Right - Bounds.Right;
            if (overflowX > 0)
                Resize(new Vector2(overflowX + X_PADDING, 0));

            _children.Add(newGadget);
            DeftUI.AddGadget(newGadget);
            return newGadget;
        }

        public T GetGadget<T>(string label) where T : Gadget
        {
            return _children.Find(g => g.Label == label) as T;
        }
        
        public virtual void OnSelect()
        { }

        public virtual void MoveTo(Vector2 newPos)
        {
            var delta = newPos - _pos;

            _pos = newPos;
            _bounds.Location = _pos.ToPoint();

            foreach (var child in _children)
                child.OnParentMoveBy(delta);
        }

        public void MoveToX(float newX) => MoveTo(new Vector2(newX, _pos.Y));
        public void MoveToY(float newY) => MoveTo(new Vector2(_pos.X, newY));

        public void MoveBy(Vector2 delta) => MoveTo(_pos + delta);
        public void MoveByX(float deltaX) => MoveTo(new Vector2(_pos.X + deltaX, _pos.Y));
        public void MoveByY(float deltaY) => MoveTo(new Vector2(_pos.X, _pos.Y + deltaY));

        public virtual void SetSize(Vector2 newSize)
        {
            Vector2 delta = newSize - _size;
            Resize(delta);
        }

        public virtual void Resize(Vector2 deltaSize)
        {
            _size += deltaSize;
            _bounds.Size = _size.ToPoint();

            foreach (var child in _children)
                child.OnParentResize(deltaSize);
        }

        public virtual void OnParentResize(Vector2 deltaSize)
        {
            if (inheritSizeX)
                _size.X += deltaSize.X;
            if (inheritSizeY)
                _size.Y += deltaSize.Y;

            _bounds.Size = _size.ToPoint();

            foreach (var child in _children)
                child.OnParentResize(deltaSize);
        }

        public virtual void OnParentMoveBy(Vector2 delta)
        {
            _pos += delta;
            _bounds.Location = _pos.ToPoint();

            foreach (var child in _children)
                child.OnParentMoveBy(delta);
        }

        public virtual void OnTextEntry(string text)
        { }

        public virtual void Display(SpriteBatch spriteBatch)
        {
            if (parent == null || alwaysShowTexture)
                spriteBatch.Draw(Assets.GetTexture(backgroundTextureName), Pos, 0, Size);
        }
    }
}
