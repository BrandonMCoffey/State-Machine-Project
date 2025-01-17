using UnityEngine;
using Utility.Audio.Systems.Base;
using Utility.Audio.Systems.Controllers;

namespace Utility.Audio.Systems.Events
{
    [CreateAssetMenu(menuName = "Utility/Sound System/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        [Header("Base Settings")]
        [SerializeField] private SfxReference _musicClip;
        [SerializeField] private bool _crossFade;
        [SerializeField] private float _fadeTime;

        public void Play()
        {
            if (_musicClip == null) return;
            if (_fadeTime <= 0) {
                SoundManager.Instance.PlayMusic(_musicClip);
            } else {
                if (_crossFade) {
                    SoundManager.Instance.PlayMusicWithFade(_musicClip, _fadeTime);
                } else {
                    SoundManager.Instance.PlayMusicWithCrossFade(_musicClip, _fadeTime);
                }
            }
        }

        public void Play(AudioSourceController source)
        {
            _musicClip?.Play(source);
        }
    }
}