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

        public bool Is<T>() where T : Entity
            => this.GetType() == typeof(T);

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

        public void ReplaceComponent(IComponent component)
        {
            if (_components.ContainsKey(component.GetType()))
            {
                _components[component.GetType()] = component;
            }
        }

        public void AddComponent<T>(IComponent component) where T : IComponent
        {
            _components.Add(typeof(T), component);
            ECSCore.pool.OnAddComponent(this, component);
        }

        public void AddComponent(IComponent component)
        {
            _components[component.GetType()] = component;
            ECSCore.pool.OnAddComponent(this, component);
        }

        public void AddComponent<T>() where T : IComponent
        {
            var cType = typeof(T);
            var c = (T)Activator.CreateInstance(cType);
  
            _components[cType] = (T)Activator.CreateInstance(cType);
            ECSCore.pool.OnAddComponent(this, _components[cType]);
        }

        public void RemoveComponent<T>()
        {
            var componentType = typeof(T);

            if (_components.ContainsKey(componentType))
            {
                ECSCore.pool.OnRemoveComponent(this, _components[componentType]);
                _components.Remove(componentType);
            }
        }

        public void RemoveComponent(IComponent component)
        {
            var componentType = component.GetType();

            if (_components.ContainsKey(componentType))
            {
                ECSCore.pool.OnRemoveComponent(this, _components[componentType]);
                _components.Remove(componentType);
            }
        }
    }
}
