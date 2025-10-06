using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

// using Iso;


namespace Zocat
{
    public class AudioManager : MonoSingleton<AudioManager>
    {
        [InfoBox(
            "Please add your gui audios to /Assets/Zocat/Runtime/Core/Audio/Resources/Sfx. " +
            "Then use EnumGenerator to generate enum. Using Example: " +
            "GameManager.AudioManager.PlayOneShotForSfx(SfxEnum.ANY_AUDIO).")]
        private const string _SfxPath = "Sfx/";
        public AudioSource[] SfxAudioSources;
        public AudioSource MusicAudioSource;

        public AudioSource GetAvailableAudioSourceForSfx
        {
            get
            {
                var audioSource = SfxAudioSources.FirstOrDefault(x => !x.isPlaying);
                if (audioSource == null) audioSource = SfxAudioSources[0];

                return audioSource;
            }
        }

        /*--------------------------------------------------------------------------------------*/


        /*--------------------------------------------------------------------------------------*/
        public void PlaySfx(SfxType _SfxEnum)
        {
            // if (!UiManager.SettingsPanel.AudioToggleGroup.IsToggleEnabled) return;
            var _Clip = Resources.Load<AudioClip>(_SfxPath + _SfxEnum);
            var _AvailableAudioSource = GetAvailableAudioSourceForSfx;
            _AvailableAudioSource.PlayOneShot(_Clip);
        }

        public void PlayOneShot(AudioClip _Clip)
        {
            var _AvailableAudioSource = GetAvailableAudioSourceForSfx;
            _AvailableAudioSource.PlayOneShot(_Clip);
        }

        /*--------------------------------------------------------------------------------------*/
        public void StopAllSfx()
        {
            foreach (var _item in SfxAudioSources)
            {
                _item.Stop();
                _item.loop = false;
                _item.clip = null;
            }
        }
#if UNITY_EDITOR
        [Button(ButtonSizes.Large)] [GUIColor(0.4f, 0.8f, 1)]
        public void GenerateEnum()
        {
            EnumGenerator.Create("SfxType");
        }
#endif
    }
}