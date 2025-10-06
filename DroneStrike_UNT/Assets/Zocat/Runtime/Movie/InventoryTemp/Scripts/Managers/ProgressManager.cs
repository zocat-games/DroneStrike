using System.Collections.Generic;
using System.Linq;
using Opsive.Shared.Events;

namespace Zocat
{
    public class ProgressManager : MonoSingleton<ProgressManager>
    {
        public List<CategoryType> DurableCategories;
        public List<CategoryType> DisposableCategories;

        protected override void Awake()
        {
            base.Awake();
            EventHandler.RegisterEvent(EventManager.AfterCompleteLevel, Check);
        }

        public void Check()
        {
            foreach (var durableCategory in DurableCategories)
            {
                var itemList = durableCategory.ItemTypeList().OrderBy(_ => _.Index()).ToList();
                for (var i = 0; i < itemList.Count - 1; i++)
                {
                    var item = itemList[i];
                    var next = itemList[i + 1];
                    if (item.Level() == ConfigManager.LevelMax && !next.Unlocked()) next.SetValue(AttributeType.Unlocked, true);
                }
            }
        }
    }
}