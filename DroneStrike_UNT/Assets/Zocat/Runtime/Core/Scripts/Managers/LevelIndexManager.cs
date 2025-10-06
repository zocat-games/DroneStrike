using Sirenix.OdinInspector;
using UnityEngine;

namespace Zocat
{
    public class LevelIndexManager : MonoSingleton<LevelIndexManager>
    {
        public int MapAmount;
        public int TempIndex;

        /*--------------------------------------------------------------------------------------*/
        public int CurrentIndex
        {
            // get => ES3.Load(Constants.LevelIndex, 0);
            get => TempIndex;
            private set
            {
                value.Clamp(0, MapAmount - 1);
                ES3.Save(Constants.LevelIndex, value);
            }
        }


        public bool LastMap => CurrentIndex == MapAmount - 1;

        public void IncreaseCurrentIndex()
        {
            CurrentIndex++;
        }

        [Button(ButtonSizes.Medium)]
        public void SetLevelAmount()
        {
            MapAmount = Resources.LoadAll("Levels").Length;
        }
    }
}

// liste elemanının sırası:             Index
// Toplanmakta olan miktar:             Counter
// Belli bir nesnenin sayılan değeri:   No
// Önceden belli olan miktar:           Amount