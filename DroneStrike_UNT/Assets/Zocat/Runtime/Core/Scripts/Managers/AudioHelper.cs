using System.Linq;
using UnityEngine;

namespace Zocat
{
    public class AudioHelper : MonoBehaviour
    {
        public AudioSource[] AudioSources;

        public AudioSource AvailableAudioSource
        {
            get
            {
                var audioSource = AudioSources.FirstOrDefault(x => !x.isPlaying);
                if (audioSource == null) audioSource = AudioSources[0];

                return audioSource;
            }
        }
    }
}