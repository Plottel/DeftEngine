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
        protected List<Gadget> _children = new List<Gadget>();

        public bool isDraggable;
        public bool isResizable;
        public bool canReceiveTextInput;
        public bool alwaysShowTexture = false;
        public bool inheritSizeX = true;
        public bool inheritSizeY = false;

        public int fontSize;

        public string backgroundTextureName;

        protected SpriteFont Font
        {
            get => Assets.GetFont("GadgetFont" + fontSize.ToString());
        }

        protected string _label;
        public virtual string Label
        {
            get => _label;
            set
            {
                _label = value;
                float labelWidth = Assets.GetFont("GadgetFont16").MeasureString(_label).X;

                int overflowX = (int)labelWidth - (int)_size.X + X_PADDING;
                if (overflowX > 0)
                    Resize(new Vector2(overflowX + X_PADDING, 0));
            }
            
        }

        private Vector2 _pos;
        private Vector2 _size;
        private Rectangle _bounds;

        protected int _layer;
        public int Layer
        {
            get => _layer;
            set
            {
                _layer = value;

                foreach (var child in _children)
                    child.Layer = value + 1;
            }
        }

        public Gadget()
        {
            backgroundTextureName = "GadgetBackground";
            SetSize(new Vector2(75, 50));
            Label = "";
            fontSize = 12;
        }

        public virtual float FirstChildTop
        {
            get
            {
                float labelHeight = Font.MeasureString("AAAA").Y;
                return Pos.Y + Y_PADDING + labelHeight + Y_PADDING;
            }
        }

        public float LastChildBottom
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

        public Gadget Add(Type gadgetType, string label)
        {
            if (!IsValidGadgetType(gadgetType))
                throw new Exception("Tried to Add invalid Gadget Type : " + gadgetType.ToString());

            var newGadget = (Gadget)Activator.CreateInstance(gadgetType);
            newGadget.parent = this;
            newGadget.SetSize(new Vector2(Size.X - (X_PADDING * 2), newGadget.Size.Y));
            newGadget.Layer = this.Layer + 1;
            newGadget.Label = label;
            newGadget.isDraggable = false;
            newGadget.isResizable = false;

            if (_children.Count == 0)
                newGadget.MoveTo(new Vector2(_pos.X + X_PADDING, FirstChildTop));
            else
                newGadget.MoveTo(new Vector2(_pos.X + X_PADDING, LastChildBottom + Y_PADDING));

            int overflowY = newGadget.Bounds.Bottom - Bounds.Bottom;
            if (overflowY > 0)
                Resize(new Vector2(0, overflowY + Y_PADDING));

            int overflowX = newGadget.Bounds.Right - Bounds.Right;
            if (overflowX > 0)
                Resize(new Vector2(overflowX + X_PADDING, 0));

            _children.Add(newGadget);
            DeftUI.Subscribe(newGadget);
            return newGadget;
        }

        public T Add<T>(string label) where T : Gadget
            => (T)Add(typeof(T), label);

        public T Get<T>(string label) where T : Gadget
            => _children.Find(g => g.Label == label) as T;

        public Gadget Get(string label)
            => _children.Find(g => g.Label == label);
        
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

            spriteBatch.DrawString(Font, Label, Pos + new Vector2(X_PADDING, Y_PADDING), ColorScheme.GadgetText);
        }

        private bool IsValidGadgetType(Type t)
            => t.IsSubclassOf(typeof(Gadget)) || t == typeof(Gadget);
    }
}
