using System.Collections.Generic;
using Sirenix.Utilities;

namespace Zocat
{
    public class TopBarPanel : SerializedUIPanel
    {
        public CurrencyGroup CurrencyGroup;

        public override void Initialize()
        {
            base.Initialize();
            CurrencyGroup.Initialize();
        }
    }
}