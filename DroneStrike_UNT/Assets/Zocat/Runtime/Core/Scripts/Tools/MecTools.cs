// using System;
// using System.Collections;
// using System.Collections.Generic;
// using MEC;
// using TMPro;
// using UnityEngine;
//
// namespace Zocat
// {
//     public static class MecTools
//     {
//         public static IEnumerator<float> CountDownTmp(TextMeshProUGUI tmp, int loopAmount, Action onComplete)
//         {
//             tmp.text = loopAmount.ToString();
//             var counter = loopAmount;
//
//             while (counter > 0)
//             {
//                 yield return Timing.WaitForSeconds(1);
//                 counter--;
//                 tmp.text = counter.ToString();
//             }
//
//             onComplete.Invoke();
//         }
//
//
//         public static IEnumerator<float> CycleForADuration(float duration, Action action, Action onComplete = null)
//         {
//             var counter = 0f;
//             while (counter < duration)
//             {
//                 yield return Timing.WaitForSeconds(Time.deltaTime);
//                 counter += Time.deltaTime;
//                 action?.Invoke();
//             }
//
//             onComplete?.Invoke();
//         }
//     }
// }

