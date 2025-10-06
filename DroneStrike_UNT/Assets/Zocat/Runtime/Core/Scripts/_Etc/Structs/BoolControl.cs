using System.Collections;
using UnityEngine;

namespace Zocat
{
    public class BoolControl
    {
        private bool current;

        public bool Changed(bool coming)
        {
            if (coming != current)
            {
                current = coming;
                return true;
            }

            return false;
        }
    }
}