using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class SerilizedDictionary<T, U> : Dictionary<T, U>, ISerializationCallbackReceiver
{
    [HideInInspector] [SerializeField] private List<T> _keys = new();
    [HideInInspector] [SerializeField] private List<U> _values = new();

    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();

        foreach (var kvp in this)
        {
            _keys.Add(kvp.Key);
            _values.Add(kvp.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        Clear();

        for (var i = 0; i != Math.Min(_keys.Count, _values.Count); i++)
        {
            Add(_keys[i], _values[i]);
        }
    }
}