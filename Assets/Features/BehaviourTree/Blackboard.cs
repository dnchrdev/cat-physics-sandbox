using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.BehaviourTree
{
    public class BBKey<T>
    {
        //public readonly string Name;
        private T _value;

        public BBKey(T initialValue)
        {
            //Name = name;
            _value = initialValue;
        }

        internal T Get() => _value;
        internal void Set(T value) => _value = value;
    }

    public class Blackboard
    {
        //private readonly HashSet<string> _keys = new();
        private bool _locked = false;

        public BBKey<T> Register<T>(T initialValue)
        {
            //if (_locked)
            //    throw new InvalidOperationException(
            //        $"Blackboard locked. Cannot register '{name}' after Build().");

            //if (!_keys.Add(name))
            //    throw new InvalidOperationException(
            //        $"Key '{name}' already registered.");

            return new BBKey<T>(initialValue);
        }

        public Blackboard Build()
        {
            _locked = true;
            return this;
        }

        // Get/Set делегируют прямо в ключ — никакого словаря, никакого боксинга
        public T Get<T>(BBKey<T> key)
        {
            if (!_locked)
                throw new InvalidOperationException("Call Build() before using Blackboard.");
            return key.Get();
        }

        public void Set<T>(BBKey<T> key, T value)
        {
            if (!_locked)
                throw new InvalidOperationException("Call Build() before using Blackboard.");
            key.Set(value);
        }
    }
}