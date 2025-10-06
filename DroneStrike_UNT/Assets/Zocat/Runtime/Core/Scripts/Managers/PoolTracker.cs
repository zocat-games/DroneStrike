using System.Collections.Generic;
using UnityEngine;
using EventHandler = Opsive.Shared.Events.EventHandler;
using ObjectPool = Opsive.Shared.Game.ObjectPool;


namespace Zocat
{
    public class PoolTracker : MonoSingleton<PoolTracker>
    {
        private readonly List<GameObject> AllObjects = new();

        protected override void Awake()
        {
            base.Awake();
            EventHandler.RegisterEvent(EventManager.MapDestroyed, OnMapDestroyed);
        }

        private void OnMapDestroyed()
        {
            foreach (var item in AllObjects) ObjectPool.Destroy(item);

            AllObjects.Clear();
        }
        /*--------------------------------------------------------------------------------------*/


        public T InstantiateObject<T>(T prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            var instance = ObjectPool.Instantiate(prefab, position, rotation);
            AddGameObject(instance.gameObject);
            return instance;
        }


        public T InstantiateObject<T>(T prefab) where T : Component
        {
            var instance = ObjectPool.Instantiate(prefab);
            AddGameObject(instance.gameObject);
            return instance;
        }

        public GameObject InstantiateGo(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            var instance = ObjectPool.Instantiate(prefab, position, rotation);
            AddGameObject(instance);
            return instance;
        }

        public GameObject InstantiateGo(GameObject prefab)
        {
            var instance = ObjectPool.Instantiate(prefab);
            AddGameObject(instance);
            return instance;
        }

        public void Destroy(GameObject gameObject)
        {
            ObjectPool.Destroy(gameObject);
        }

        /*--------------------------------------------------------------------------------------*/
        private void AddGameObject(GameObject coming)
        {
            if (!AllObjects.Contains(coming)) AllObjects.Add(coming);
        }
        /*--------------------------------------------------------------------------------------*/
    }
}