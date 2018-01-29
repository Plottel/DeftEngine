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

        /// <summary>
        /// Do not change this variable unless you understand the consequences.
        /// Colliders will be out of sync. Colliders can be Synced manually using
        /// Collisions.SyncColliders(Entity)
        /// </summary>
        public Vector2 pos;

        /// <summary>
        /// Used to access the Position. On set, immediately moves the entity and forces collider Sync.
        /// This is slow, avoid using this method of moving an Entity unless you need to. Prefer using 
        /// MoveTo and MoveBy methods.
        /// </summary>
        public Vector2 Pos
        {
            get => pos;

            set
            {
                pos = value;
                Collisions.SyncColliders(this);
                MoveTo(value);
            }
        }

        public Vector2 size;
        public float rotation;

        public List<IColliderComponent> Colliders
        {
            get => _colliders;
        }

        public Point MidPt
        {
            get { return new Point((int)pos.X + (int)size.X / 2, (int)pos.Y + (int)size.Y / 2); }
        }

        public Vector2 MidVector
        {
            get { return new Vector2(pos.X + size.X / 2, pos.Y + size.Y / 2); }
        }

        public void Destroy()
            => Add<Component_KillMe>();

        public void MoveTo(Vector2 newPos)
            => ECSCore.actionPool.AddAction(new Action_MoveTo { actor = this, newPosition = newPos });

        public void MoveToX(float newX)
            => ECSCore.actionPool.AddAction(new Action_MoveTo { actor = this, newPosition = new Vector2(newX, pos.Y) });

        public void MoveToY(float newY)
            => ECSCore.actionPool.AddAction(new Action_MoveTo { actor = this, newPosition = new Vector2(pos.X, pos.Y) });

        public void MoveBy(Vector2 deltaPos)
            => ECSCore.actionPool.AddAction(new Action_MoveBy { actor = this, deltaPos = deltaPos });

        public void MoveByX(float deltaX) 
            => ECSCore.actionPool.AddAction(new Action_MoveBy { actor = this, deltaPos = new Vector2(deltaX, 0) });

        public void MoveByY(float deltaY)
            => ECSCore.actionPool.AddAction(new Action_MoveBy { actor = this, deltaPos = new Vector2(0, deltaY) });

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
