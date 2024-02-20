using System;
using System.IO;
using System.Collections.Generic;
using Rosetta.Editor.Collector;
using UnityEngine;
using UnityEditor;
using Rosetta.Runtime.Loader;
using Rosetta.Utils;

namespace Rosetta.Editor.Creator
{
    [CreateAssetMenu(menuName = "Rosetta/Creator/MediaCreator")]
    [Serializable]
    public class MediaCreator : CreatorBase
    {
        [HideInInspector]
        public MediaInfo MediaInfo;
        protected override void _create()
        {
            MediaInfo = new MediaInfo
            {
                Audios = new List<string>(),
                Images = new List<string>()
            };
            DirectoryCreate($"{OutputPath}/{Name}");
            DirectoryCreate($"{OutputPath}/res/img/{Name}");
            DirectoryCreate($"{OutputPath}/res/audio/{Name}");
            DirectoryCreate($"{OutputPath}/res/font/{Name}");
            Collectors.ForEach(collector =>
            {
                collector.Collect(Name);
                if (collector.I18NImages != null) ImagePass(collector);
                if (collector.I18NAudios != null) AudioPass(collector);
                if (collector.I18NFonts != null) FontPass(collector);
            });
            SaveFile($"{OutputPath}/{Name}/mediaInfo.json", JsonUtility.ToJson(MediaInfo));
        }

        private void FontPass(CollectorBase collector)
        {
            collector.I18NFonts.ForEach(pair => { 
                SaveFile($"{OutputPath}/res/font/{pair.Key}.json", JsonUtility.ToJson(pair.Value.Value));
            });
        }

        private void ImagePass(CollectorBase collector)
        {
            collector.I18NImages.ForEach(pair =>
            {
                MediaInfo.Images.Add(pair.Key);
                Sprite sprite = pair.Value.Value;
                var info = new ImageInfo
                {
                    PivotX = sprite.pivot.x,
                    PivotY = sprite.pivot.y,
                    Width = (int) sprite.rect.width,
                    Height = (int) sprite.rect.height,
                    Comment = pair.Value.Comment
                };
                SaveFile($"{OutputPath}/res/img/{Name}/{pair.Key}.json", JsonUtility.ToJson(info));
                var tex = new Texture2D((int) sprite.rect.width, (int) sprite.rect.height)
                {
                    filterMode = FilterMode.Point,
                    wrapMode = TextureWrapMode.Clamp
                };
                tex.SetPixels(sprite.texture.GetPixels(
                    (int) sprite.rect.x, 
                    (int) sprite.rect.y, 
                    (int) sprite.rect.width,
                    (int) sprite.rect.height));
                tex.Apply();
                SaveFile($"{OutputPath}/res/img/{Name}/{pair.Key}.png", tex.EncodeToPNG());
            });
        }
        
        private void AudioPass(CollectorBase collector)
        {
            
            collector.I18NAudios.ForEach(pair =>
            {
                MediaInfo.Audios.Add(pair.Key);
                AudioClip audio = pair.Value.Value;
                string path = AssetDatabase.GetAssetPath(audio);
                if (File.Exists($"{OutputPath}/res/audio/{Name}/{pair.Key}{Path.GetExtension(path)}"))
                {
                    File.Delete($"{OutputPath}/res/audio/{Name}/{pair.Key}{Path.GetExtension(path)}");
                }
                FileUtil.CopyFileOrDirectory(path, $"{OutputPath}/res/audio/{Name}/{pair.Key}{Path.GetExtension(path)}");
            });
            
        }
    }
}