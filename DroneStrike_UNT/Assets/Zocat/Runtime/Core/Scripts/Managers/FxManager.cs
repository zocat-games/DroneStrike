using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Zocat
{
    public class FxManager : InstanceBehaviour
    {
        public GameObject cameraGo;
        public GameObject particle;

        public void ShowConfetti()
        {
            cameraGo.SetActive(true);

            _ = StartAs();

            async Task StartAs()
            {
                particle.SetActive(true);
                await Task.Delay(TimeSpan.FromSeconds(1));
                particle.SetActive(false);
                cameraGo.SetActive(false);
            }
        }
    }
}