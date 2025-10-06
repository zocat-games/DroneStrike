using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Sirenix.Utilities;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Zocat
{
    public class CurrencyAnimatorPanel : UIPanel
    {
        #region Initialize

        public override void Initialize()
        {
            base.Initialize();
        }

        // public override void ShowPanel(bool fade = false)
        // {
        //     base.ShowPanel(fade);
        //     DeActivateAllList();
        // }

        public override void Hide()
        {
            base.Hide();
        }

        #endregion

        /*--------------------------------------------------------------------------------------*/

        public List<Image> Dollars;
        public List<Image> Diamonds;
        public List<Image> Keys;


        private async void PlayAnimation(Vector3 startPos, Vector3 finishPos, List<Image> list)
        {
            DeActivateAllList();
            foreach (var item in list)
            {
                item.transform.DOKill();
                item.transform.position = startPos;
            }

            foreach (var item in list)
            {
                await Task.Delay(100);
                item.SetActive(true);
                item.transform.DOJump(finishPos, Random.Range(-200, 200), 1, 1).OnComplete(() => item.SetActive(false));
            }
        }

        private void DeActivateAllList()
        {
            Dollars.ForEach(_ => _.SetActive(false));
            Diamonds.ForEach(_ => _.SetActive(false));
            Keys.ForEach(_ => _.SetActive(false));
        }
    }
}