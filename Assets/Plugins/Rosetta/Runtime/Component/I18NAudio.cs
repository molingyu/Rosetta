using UnityEngine;

namespace Rosetta.Runtime.Component
{
    /// <summary>
    ///     Marking the current Audio component requires I18N.
    /// </summary>
    [AddComponentMenu("Rosetta/I18NAudio")]
    [RequireComponent(typeof(AudioSource))]
    [HelpURL("https://molingyu.github.io/RosettaDocs/guides/makeI18NRes/component/I18NAudio.html")]
    public class I18NAudio : I18NComponentBase
    {
        private AudioClip _devAudio;
        /// <summary>
        ///     
        /// </summary>
        public string ResName;

        private void Start()
        {
            _devAudio = gameObject.GetComponent<AudioSource>().clip;
            Create();
        }

        public override void Refresh()
        {
            var audioSource = gameObject.GetComponent<AudioSource>();
            var status = audioSource.isPlaying;
            audioSource.clip = Rosetta.IsDefault() ? _devAudio : Rosetta.GetAudio(ResName, I18NSpace);
            if (status) audioSource.Play();
        }
    }
}