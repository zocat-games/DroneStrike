using UnityEngine;

namespace Zocat
{
    public static class ShooterTools
    {
        // public static void Reload(ref int stock, ref int clipMax, ref int clipCurrent)
        // {
        //     int needed = clipMax - clipCurrent;
        //     int toLoad = Mathf.Min(needed, stock);
        //
        //     clipCurrent += toLoad;
        //     stock -= toLoad;
        //
        //     // Tekrar clamp’leyelim (garanti olsun)
        //     clipCurrent = Mathf.Clamp(clipCurrent, 0, clipMax);
        //     stock = Mathf.Clamp(stock, 0, stock);
        //     
        // }
        // public static void Reload(ref int stockCurrent, ref int clipCurrent, int clipMax, bool infiniteAmmo = true)
        // {
        //     if (clipCurrent >= clipMax || stockCurrent <= 0)
        //         return;
        //     var needed = clipMax - clipCurrent;
        //     var toLoad = Mathf.Min(needed, stockCurrent);
        //     clipCurrent += toLoad;
        //     if (!infiniteAmmo) stockCurrent -= toLoad;
        //     clipCurrent = Mathf.Clamp(clipCurrent, 0, clipMax);
        //     stockCurrent = Mathf.Max(stockCurrent, 0);
        // }
        public static void Reload(ref int stockCurrent, ref int clipCurrent, int clipMax, bool infiniteAmmo = true)
        {
            if (clipCurrent >= clipMax)
                return;

            if (infiniteAmmo)
            {
                // Direkt full clip doldur
                clipCurrent = clipMax;
            }
            else
            {
                if (stockCurrent <= 0)
                    return;

                var needed = clipMax - clipCurrent;
                var toLoad = Mathf.Min(needed, stockCurrent);

                clipCurrent += toLoad;
                stockCurrent -= toLoad;
            }

            // Clamp güvenliği
            clipCurrent = Mathf.Clamp(clipCurrent, 0, clipMax);
            stockCurrent = Mathf.Max(stockCurrent, 0);
        }
    }
}