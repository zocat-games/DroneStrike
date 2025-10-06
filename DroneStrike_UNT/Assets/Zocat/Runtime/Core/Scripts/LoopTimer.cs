using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Zocat
{
    public abstract class LoopTimer
    {
        private readonly CancellationTokenSource cts = new();
        private readonly Action cycleEndAction;
        private readonly Action cycleStartAction;
        private float interval;
        // private bool living = true;
        private bool playable;

        protected LoopTimer(float interval, Action cycleEndAction, Action cycleStartAction = null, bool playable = true)
        {
            this.interval = interval;
            this.cycleStartAction = cycleStartAction;
            this.cycleEndAction = cycleEndAction;
            this.playable = playable;
            LoopTimerManager.Instance.Add(this);
            LoopAsync(cts.Token).Forget();
        }

        private async UniTaskVoid LoopAsync(CancellationToken token)
        {
            try
            {
                while (!token.IsCancellationRequested)
                {
                    while (!playable && !token.IsCancellationRequested) await UniTask.Yield(PlayerLoopTiming.Update, token);

                    cycleStartAction?.Invoke();
                    await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);
                    cycleEndAction?.Invoke();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("Canceled..");
            }
        }

        public void SetPlayable(bool playableComing)
        {
            playable = playableComing;
        }

        public void SetInterval(float intervalComing)
        {
            interval = intervalComing;
        }

        public void Kill()
        {
            cts.Cancel();
        }
    }
}