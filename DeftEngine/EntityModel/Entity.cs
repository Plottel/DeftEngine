using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;

namespace DeftEngine
{
    public class Entity
    {
        private Dictionary<Type, IComponent> _components = new Dictionary<Type, IComponent>();
        private List<IColliderComponent> _colliders = new List<IColliderComponent>();

        private Vector2 _pos;
        private Vector2 _size;
        private float _rotation;

        public Vector2 Pos
        {
            get => _pos;
        }

        public Vector2 Size
        {
            get => _size;

            set
            {
                _size = value;
                Collisions.SyncColliders(this);
            }
        }

        public float Rotation
        {
            get => _rotation;

            set
            {
                _rotation = value;
                Collisions.SyncColliders(this);
            }
        }

        public List<IColliderComponent> Colliders
        {
            get => _colliders;
        }

        public Point MidPt
        {
            get { return new Point((int)_pos.X + (int)_size.X / 2, (int)_pos.Y + (int)_size.Y / 2); }
        }

        public Vector2 MidVector
        {
            get { return new Vector2(_pos.X + _size.X / 2, _pos.Y + _size.Y / 2); }
        }

        public void Destroy()
            => Add<Component_KillMe>();

        public void MoveTo(Vector2 newPos)
        {
            if (_pos != newPos)
                Add(new Component_Moved { oldPos = _pos });

            _pos = newPos;
            Collisions.SyncColliders(this);
        }

        public void MoveTo(float newX, float newY) => MoveTo(new Vector2(newX, newY));
        public void MoveToX(float newX) => MoveTo(new Vector2(newX, 0));
        public void MoveToY(float newY) => MoveTo(new Vector2(0, newY));

        public void MoveBy(Vector2 deltaPos)
        {
            if (deltaPos != Vector2.Zero)
                Add(new Component_Moved { oldPos = _pos });

            _pos += deltaPos;
            Collisions.SyncColliders(this);
        }

        public void MoveBy(float deltaX, float deltaY) => MoveBy(new Vector2(deltaX, deltaY));
        public void MoveByX(float deltaX) => MoveBy(new Vector2(deltaX, 0));
        public void MoveByY(float deltaY) => MoveBy(new Vector2(0, deltaY));

        //public Entity Copy()
        //{
        //    var copy = (Entity)Activator.CreateInstance(this.GetType());
        //    foreach (var component in ComponentList)
        //        copy.AddComponent(component.Copy());
        //    return copy;
        //}

        /// <summary>
        /// Serializes all components into a BinaryWriter
        /// </summary>
        /// <param name="writer"></param>
        //public void Serialize(BinaryWriter writer)
        //{
        //    writer.Write(this.GetType().Name);
        //    writer.Write(_components.Count);

        //    foreach (var componentKVP in _components)
        //    {
        //        string typeString = componentKVP.Value.GetType().Name;
        //        writer.Write(typeString);
        //        componentKVP.Value.Serialize(writer);
        //    }
        //}

        /// <summary>
        /// Deserializes all components from a BinaryReader
        /// </summary>
        /// <param name="reader"></param>
        //public void Deserialize(BinaryReader reader)
        //{
        //    int numComponents = reader.ReadInt32();

        //    for (int i = 0; i < numComponents; ++i)
        //    {
        //        string typeString = reader.ReadString();
        //        string fullTypeString = "DeftLib." + typeString;
        //        Type componentType = Type.GetType(fullTypeString);
        //        var newComponent = (IComponent)Activator.CreateInstance(componentType);
        //        newComponent.Deserialize(reader);
        //        AddComponent(newComponent);
        //    }
        //}

        public Dictionary<Type, IComponent> ComponentMap
        {
            get { return _components; }
        }

        public List<IComponent> ComponentList
        {
            get { return _components.Values.ToList(); }
        }

        public List<Type> ComponentTypes
        {
            get { return _components.Keys.ToList(); }
        }

        public T Get<T>() where T : IComponent
        {
            var type = typeof(T);

            if (_components.ContainsKey(type))
                return (T)_components[type];
            return default(T);
        }

        public IComponent Get(Type componentType)
        {
            if (_components.ContainsKey(componentType))
                return _components[componentType];
            return null;
        }

        public bool Has<T>()
        {
            return _components.ContainsKey(typeof(T));
        }

        public bool Has(Type t)
        {
            return _components.ContainsKey(t);
        }

        public void Replace(IComponent component)
        {
            if (_components.ContainsKey(component.GetType()))
            {
                _components[component.GetType()] = component;
            }
        }

        public void Add<T>(IComponent component) where T : IComponent
        {
            var collider = component as IColliderComponent;
            if (collider != null)
            {
                collider.SetDefault(this);
                _colliders.Add(collider);
            }

            _components.Add(typeof(T), component);
            ECSCore.pool.OnAddComponent(this, component);
        }

        public void Add(IComponent component)
        {
            var collider = component as IColliderComponent;
            if (collider != null)
            {
                collider.SetDefault(this);
                _colliders.Add(collider);
            }

            _components[component.GetType()] = component;
            ECSCore.pool.OnAddComponent(this, component);
        }

        public void Add<T>() where T : IComponent
        {
            var cType = typeof(T);
            var c = (T)Activator.CreateInstance(cType);

            var collider = c as IColliderComponent;
            if (collider != null)
            {
                collider.SetDefault(this);
                _colliders.Add(collider);
            }

            _components[cType] = c;
            ECSCore.pool.OnAddComponent(this, _components[cType]);
        }

        public void Remove<T>()
        {
            var componentType = typeof(T);

            if (_components.ContainsKey(componentType))
            {
                ECSCore.pool.OnRemoveComponent(this, _components[componentType]);
                _components.Remove(componentType);

                if (typeof(IColliderComponent).IsAssignableFrom(componentType))
                    _colliders.Remove(_colliders.Find(c => c.GetType() == componentType));
            }
        }

        public void Remove(IComponent component)
        {
            var componentType = component.GetType();

            if (_components.ContainsKey(componentType))
            {
                ECSCore.pool.OnRemoveComponent(this, _components[componentType]);
                _components.Remove(componentType);

                if (typeof(IColliderComponent).IsAssignableFrom(componentType))
                    _colliders.Remove(_colliders.Find(c => c.GetType() == componentType));
            }
        }
    }
}
