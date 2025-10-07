using System.Collections;
using UnityEngine;

namespace Zocat
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private void Start()
        {
            Application.targetFrameRate = 60;


            UiManager.Initialize();

            StartCoroutine(StartIE());

            IEnumerator StartIE()
            {
                yield return new WaitForSeconds(1);
                StreamingManager.ShowUi();
                // StreamingManager.CreateLevel();
                CurrencyType.Silver.SetAmount(5000);
            }
        }
    }
}
//  Oyunu build etmeden önce LevelManager MapAmount Set
//  MapManager'da SetMapSoFields çalıştır.
//  Build'den Map üzerindeki Set All Ids çalıştır.
//  UpgradePoints'in miktarı TableWithChair gibi tiplerin sayısınca olmalı.
// ES3.Load ciddi performans sorunu yarattı.

// Command + F12    =>  SO
// Command + F11    =>  Panel
// Command + F10    =>  ClassGenerator

// Ctrl + F12    =>  Assembly
// Ctrl + F11    =>  ClearAll

/*--------------------------------------------------------------------------------------*/