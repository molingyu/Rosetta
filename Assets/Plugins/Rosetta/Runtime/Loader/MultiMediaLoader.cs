using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Rosetta.Runtime.Loader
{

    [Serializable]
    public struct MediaInfo
    {
        public List<string> Audios;
        public List<string> Images;
    }
    
    /// <summary>
    ///     MultiMediaLoader will load multiple sprites and audios according to the `mediaInfo` file.
    /// </summary>
    public  class MultiMediaLoader : LoaderBase
    {
        public override T Load<T>(string space, string filename, LangFlag? flag = null)
        {
            throw new Exception("MultiMediaLoader this method is invalid.");
        }
        
        public  void Load(string space, LangFlag locale,
            ref Dictionary<string, Dictionary<string, Sprite>> spriteCache, 
            ref Dictionary<string, Dictionary<string, AudioClip>> audioCache)
        {
            LoadBase(Path.Combine(space, "mediaInfo"), locale);
            ExtensionName = "json";
            var info = JsonUtility.FromJson<MediaInfo>(LoadFile<string>());;
            audioCache[space] = new Dictionary<string, AudioClip>();
            spriteCache[space] = new Dictionary<string, Sprite>();
            foreach (var infoAudio in info.Audios)
            {
                audioCache[space].Add(infoAudio, Rosetta.Loaders[I18NFileType.Wav]
                    .Load<AudioClip>(space, infoAudio, locale));
            }
            foreach (var infoImage in info.Images)
            {
                spriteCache[space].Add(infoImage, Rosetta.Loaders[I18NFileType.Png]
                    .Load<Sprite>(space, infoImage, locale));
            }
        }
    }
}