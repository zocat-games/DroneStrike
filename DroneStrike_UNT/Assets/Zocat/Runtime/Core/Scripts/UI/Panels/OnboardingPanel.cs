using System.Collections;
using UnityEngine;

namespace Zocat
{
    public class OnboardingPanel : UIPanel
    {
        public Transform Main;
        public Transform Pos0;
        public Transform Pos1;
        public GameObject Click;
        private Coroutine handCoroutine;
        private Coroutine clickCoroutine;

        public override void Show()
        {
            base.Show();
            handCoroutine = StartCoroutine(HandIE());
            clickCoroutine = StartCoroutine(ClickIE());

            StartCoroutine(StartIE());

            IEnumerator StartIE()
            {
                yield return new WaitForSeconds(4);
                Stop();
                Hide();
            }
        }

        private void Stop()
        {
            Shown = true;
            StopCoroutine(HandIE());
            StopCoroutine(ClickIE());
        }
        /*--------------------------------------------------------------------------------------*/

        IEnumerator HandIE()
        {
            while (!Shown)
            {
                Main.transform.position = Pos0.position;
                yield return new WaitForSeconds(1);
                Main.transform.position = Pos1.position;
                yield return new WaitForSeconds(1);
            }
        }

        IEnumerator ClickIE()
        {
            while (!Shown)
            {
                Click.SetActive(false);
                yield return new WaitForSeconds(0.5f);
                Click.SetActive(true);
                yield return new WaitForSeconds(0.5f);
            }
        }
        /*--------------------------------------------------------------------------------------*/

        public bool Shown
        {
            get => ES3.Load("Shown", false);
            set => ES3.Save("Shown", value);
        }
    }
}