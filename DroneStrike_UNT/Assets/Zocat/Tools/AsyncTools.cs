using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Zocat
{
    public static class AsyncTools
    {
        public static async Task RunAsync(Action action, float delay = 1)
        {
            if (action == null) return;

            try
            {
                if (delay > 0)
                    await Task.Delay(TimeSpan.FromSeconds(delay));

                action.Invoke();
                IsoHelper.Log("Async çalıştı");
            }
            catch (Exception ex)
            {
                Debug.LogError($"StaticExecutor hata: {ex}");
            }
        }
    }
}