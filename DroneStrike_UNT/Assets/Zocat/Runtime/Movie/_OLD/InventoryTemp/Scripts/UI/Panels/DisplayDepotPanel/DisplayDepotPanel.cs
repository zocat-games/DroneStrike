using System;
using System.Collections.Generic;
using System.Linq;
using Opsive.Shared.Game;
using UnityEngine;

namespace Zocat
{
    public class DisplayDepotPanel : SerializedUIPanel
    {
        public DurableDisplay DurableDisplayPrf;
        public DisposableDisplay DisposableDisplayPrf;
        public CategoryDurableDisplay CategoryDurableDisplayPrf;
        public CategoryDisposableDisplay CategoryDisposableDisplayPrf;
        /*--------------------------------------------------------------------------------------*/
        public Transform KillParent;
        public Dictionary<Type, List<CategoryDisplayBase>> EquipmentDisplayDic;
        public Dictionary<Type, List<ItemDisplayBase>> StockDisplayDic;

        public override void Initialize()
        {
            base.Initialize();
            StockDisplayDic.Add(typeof(DurableDisplay), new List<ItemDisplayBase>());
            StockDisplayDic.Add(typeof(DisposableDisplay), new List<ItemDisplayBase>());
            KillParent.SetActive(false);
            for (var i = 0; i < CategoryType._Gun.Count(); i++)
            {
                var display = CloneTools.InstantiatePrefab(DurableDisplayPrf, KillParent);
                StockDisplayDic[typeof(DurableDisplay)].Add(display);
            }

            for (var i = 0; i < CategoryType._Disposable.Count(); i++)
            {
                var display = CloneTools.InstantiatePrefab(DisposableDisplayPrf, KillParent);
                StockDisplayDic[typeof(DisposableDisplay)].Add(display);
            }
            /*--------------------------------------------------------------------------------------*/

            EquipmentDisplayDic.Add(typeof(CategoryDurableDisplay), new List<CategoryDisplayBase>());
            EquipmentDisplayDic.Add(typeof(CategoryDisposableDisplay), new List<CategoryDisplayBase>());
            for (var i = 0; i < 10; i++)
            {
                var display = CloneTools.InstantiatePrefab(CategoryDurableDisplayPrf, KillParent);
                EquipmentDisplayDic[typeof(CategoryDurableDisplay)].Add(display);
                display.Initialize();
            }

            for (var i = 0; i < 5; i++)
            {
                var display = CloneTools.InstantiatePrefab(CategoryDisposableDisplayPrf, KillParent);
                EquipmentDisplayDic[typeof(CategoryDisposableDisplay)].Add(display);
                display.Initialize();
            }
        }


        public T GetStockDisplay<T>(Transform parent) where T : ItemDisplayBase
        {
            var display = StockDisplayDic[typeof(T)].FirstOrDefault(display => display.AvailableToGo);
            display.AvailableToGo = false;
            display.transform.SetParent(parent);
            return display.gameObject.GetCachedComponent<T>();
        }

        public T GetEquipmentDisplay<T>(Transform parent) where T : CategoryDisplayBase
        {
            var display = EquipmentDisplayDic[typeof(T)].FirstOrDefault(display => display.AvailableToGo);
            display.AvailableToGo = false;
            display.transform.SetParent(parent);
            return display.gameObject.GetCachedComponent<T>();
        }

        /*--------------------------------------------------------------------------------------*/
        public void KillForStock()
        {
            foreach (var item in StockDisplayDic.SelectMany(list => list.Value))
            {
                item.transform.SetParent(KillParent);
                item.AvailableToGo = true;
            }
        }

        public void KillForEquipment()
        {
            foreach (var item in EquipmentDisplayDic.SelectMany(list => list.Value))
            {
                item.transform.SetParent(KillParent);
                item.AvailableToGo = true;
            }
        }
        //
        // public void KillDisplay(ItemDisplayBase display)
        // {
        //     display.transform.SetParent(KillParent);
        //     display.AvailableToGo = true;
        // }
    }
}