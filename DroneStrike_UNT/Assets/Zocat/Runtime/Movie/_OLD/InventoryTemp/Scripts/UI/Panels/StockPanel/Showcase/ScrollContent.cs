using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Zocat
{
    public class ScrollContent : InstanceBehaviour
    {
        public List<ItemDisplayBase> Displays;
        public Transform ItemContent;

        public void SortGrid()
        {
            Displays.Clear();
            Displays = ItemContent.GetComponentsInChildren<ItemDisplayBase>().ToList();
            Displays = Displays.OrderBy(_ => _.ItemType.Purchased()).ToList();

            for (var i = 0; i < Displays.Count; i++)
            {
                var item = Displays[i];
                item.transform.SetSiblingIndex(i);
            }
        }
    }
}