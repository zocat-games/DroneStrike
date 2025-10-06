using System;
using System.Linq;
using System.Reflection;
using DG.Tweening;
// using Iso;
using JetBrains.Annotations;
using UnityEngine;

namespace Zocat
{
    public static class GeneralConstants
    {
        public const string GameLevelIndex = "GameLevelIndex";
        public const string CurrentLanguage = "CurrentLanguage";
        public const string CoinAmount = "CoinAmount";

        private static ConstantHelper _ConstantHelperInstance;
        public static ConstantHelper Helper => _ConstantHelperInstance ??= new ConstantHelper();
    }

    public class ConstantHelper
    {
        public T GetVariable<T>(string _Name)
        {
            Type _T = typeof(GeneralConstants);
            FieldInfo[] fields = _T.GetFields(BindingFlags.Static | BindingFlags.Public);

            foreach (var _Item in fields)
            {
                if (_Item.Name == _Name)
                {
                    return (T)_Item.GetValue(null);
                }
            }

            return default;
        }
    }
}