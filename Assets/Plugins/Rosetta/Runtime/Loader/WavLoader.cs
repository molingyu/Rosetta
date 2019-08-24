using System.IO;
using Rosetta.Runtime.Common;
using UnityEngine;

namespace Rosetta.Runtime.Loader
{
    /// <summary>
    ///     Load `.wav` file in runtime and return a `AudioClip` object.
    /// </summary>
    public class WavLoader : LoaderBase
    {
        
        public override T Load<T>(string space, string filename, LangFlag? flag = null)
        {
            LoadBase(Path.Combine("res", "audio", space, filename), flag ?? Rosetta.Locale);
            ExtensionName = "wav";
            var wav = new Wav(LoadFile<byte[]>());
            var clip = AudioClip.Create($"{filename}_{(flag ?? Rosetta.Locale).ToString().ToLower()}",
                wav.SampleCount, 1, wav.Frequency, false);
            clip.SetData(wav.LeftChannel, 0);
            return (T) (object) clip;
        }
    }
}