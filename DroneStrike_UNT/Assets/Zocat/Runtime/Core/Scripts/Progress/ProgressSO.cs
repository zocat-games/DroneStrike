using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zocat
{
    [CreateAssetMenu(fileName = "ProgressSO", menuName = "ScriptableObjects/ProgressSO", order = 1)]
    public class ProgressSO : ScriptableObject
    {
        public List<Sprite> Sprites;
    }
}