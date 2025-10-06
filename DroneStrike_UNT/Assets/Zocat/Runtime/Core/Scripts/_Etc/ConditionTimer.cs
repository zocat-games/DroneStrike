using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Zocat
{
    public class ConditionTimer
    {
        private readonly float interval;
        private readonly Func<bool> controlFunc;
        private readonly Action finishAction;
        private readonly CancellationTokenSource cts = new();
        private readonly bool isLooping;

        public ConditionTimer(float interval, Func<bool> controlFunc, Action finishAction, bool isLooping = false)
        {
            this.interval = interval;
            this.controlFunc = controlFunc;
            this.finishAction = finishAction;
            this.isLooping = isLooping;
            LoopTimerManager.Instance.Add(this);
            ControlLoop(cts.Token).Forget();
        }

        private async UniTask ControlLoop(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                if (controlFunc())
                {
                    finishAction?.Invoke();
                    if (!isLooping)
                    {
                        Kill();
                        break;
                    }
                }

                await UniTask.Delay(TimeSpan.FromSeconds(interval), cancellationToken: token);
            }
        }

        public void Kill()
        {
            cts.Cancel();
            LoopTimerManager.Instance.Remove(this);
        }
    }
}