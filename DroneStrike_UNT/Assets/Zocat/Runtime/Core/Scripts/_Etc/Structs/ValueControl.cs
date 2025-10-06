using System.Collections;
using UnityEngine;

namespace Zocat
{
    // public struct ValueControl<T> where T : Object
    public struct ValueControl<T>
    {
        private T current;

        public bool Changed(T coming)
        {
            if (!coming.Equals(current))
            {
                current = coming;
                return true;
            }

            return false;
        }
    }
}