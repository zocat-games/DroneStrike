using System.Collections.Generic;
using UnityEngine;

namespace Zocat
{
    public class ShooterAudioManager : MonoSingleton<ShooterAudioManager>
    {
        public AudioHelper AudioHelper;
        public Dictionary<ShooterAudioType, List<AudioClip>> AudioClips;

        public void PlayRandom(ShooterAudioType audioType)
        {
            var clip = AudioClips[audioType].RandomElement();
            AudioHelper.AvailableAudioSource.PlayOneShot(clip);
        }

        public AudioClip RandomAudioClip(ShooterAudioType audioType)
        {
            return AudioClips[audioType].RandomElement();
        }
    }
}