using System;
using System.Collections.Generic;
using System.Linq;
using Rosetta.Runtime.Component;
using Rosetta.Editor.Creator;
using Rosetta.Runtime.Loader;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rosetta.Editor.Collector
{
    [HideInInspector]
    public class PrefabCollectorBase : CollectorBase
    {

        public static Action<PrefabCollectorBase, GameObject> CollectHook;
        
        protected List<GameObject> GameObjects;
        protected string PrefixName;
        public override void Collect(string space)
        {
            I18NImages = new Dictionary<string, I18NMedia<Sprite>>();
            I18NStrings = new Dictionary<string, I18NMedia<string>>();
            I18NAudios = new Dictionary<string, I18NMedia<AudioClip>>();
            I18NFonts = new Dictionary<string, I18NMedia<FontInfo>>();
            foreach (var gameObject in GameObjects)
            {
                    // image collect
                    foreach (var component in gameObject.GetComponentsInChildren<I18NImage>())
                        if (component.I18NSpace == space)
                            AddI18NImage(PrefixName, component);
                    // audio collect
                    foreach (var component in gameObject.GetComponentsInChildren<I18NAudio>())
                        if (component.I18NSpace == space)
                            AddI18NAudio(PrefixName, component);
                    // text collect
                    foreach (var component in gameObject.GetComponentsInChildren<I18NText>())
                        if (component.I18NSpace == space) {
                            if (component.IsVirtualFont) AddI18NFont(PrefixName, component);
                            AddI18NString(PrefixName, component);
                        }
                    foreach (var component in gameObject.GetComponentsInChildren<I18NTMPText>())
                        if (component.I18NSpace == space){
                            if (component.IsVirtualFont) AddI18NTMPFont(PrefixName, component);
                            AddI18NString(PrefixName, component);
                        }
                    
                    CollectHook?.Invoke(this, gameObject);
            }
        }
        
        public void AddI18NFont(string prefixName, I18NText component)
        {
            GameObject gameObject = component.gameObject;
            var font = gameObject.GetComponent<Text>().font;
            var path = $"{prefixName}/{GetPath(gameObject, new List<string>())}";
            if (I18NFonts.ContainsKey(component.VirtualFontName))
            {
                I18NFonts[component.VirtualFontName].PathList.Add(path);
            }
            else
            {
                var i18NFont = new I18NMedia<FontInfo>
                {
                    Comment = component.I18NComment,
                    PathList = new List<string> {path},
                    Value = new FontInfo {
                        FontName = font.name,
                        Size = font.fontSize,
                        IsTmpFont = false,
                        IsDefaultInclude = true
                    }
                };
                I18NFonts.Add(component.VirtualFontName, i18NFont);
            } 
        }

        public void AddI18NTMPFont(string prefixName, I18NTMPText component)
        {
            GameObject gameObject = component.gameObject;
            var font = gameObject.GetComponent<TextMeshProUGUI>().font;
            var path = $"{prefixName}/{GetPath(gameObject, new List<string>())}";
            if (I18NFonts.ContainsKey(component.VirtualFontName))
            {
                I18NFonts[component.VirtualFontName].PathList.Add(path);
            }
            else
            {
                var i18NTMPFont = new I18NMedia<FontInfo>
                {
                    Comment = component.I18NComment,
                    PathList = new List<string> {path},
                    Value = new FontInfo {
                        FontName = font.name,
                        Size = 12,
                        IsTmpFont = false,
                        IsDefaultInclude = true
                    }
                };
                I18NFonts.Add(component.VirtualFontName, i18NTMPFont);
            }
        }

        public void AddI18NString(string prefixName, I18NComponentBase component)
        {
            GameObject gameObject = component.gameObject;
            var text = gameObject.GetComponent<Text>()
                ? gameObject.GetComponent<Text>().text
                : gameObject.GetComponent<TMP_Text>().text;
            var path = $"{prefixName}/{GetPath(gameObject, new List<string>())}";
            if (I18NStrings.ContainsKey(text))
            {
                I18NStrings[text].PathList.Add(path);
            }
            else
            {
                var i18NString = new I18NMedia<string>
                {
                    Comment = component.I18NComment,
                    PathList = new List<string> {path},
                    Value = text
                };
                I18NStrings.Add(text, i18NString);
            }
        }

        public void AddI18NImage(string prefixName, I18NImage component)
        {
            GameObject gameObject = component.gameObject;
            var sprite = gameObject.GetComponent<Sprite>()
                ? gameObject.GetComponent<Sprite>()
                : gameObject.GetComponent<Image>().sprite;
            var path = $"{prefixName}/{GetPath(gameObject, new List<string>())}";
            if (I18NImages.ContainsKey(component.ResName))
            {
                I18NImages[component.ResName].PathList.Add(path);
            }
            else
            {
                var i18NImage = new I18NMedia<Sprite>
                {
                    Comment = component.I18NComment,
                    PathList = new List<string> {path},
                    Value = sprite
                };
                I18NImages.Add(component.ResName, i18NImage);
            }
        }

        public void AddI18NAudio(string prefixName, I18NAudio component)
        {
            GameObject gameObject = component.gameObject;
            var audioClip = gameObject .GetComponent<AudioSource>().clip;
            var path = $"{prefixName}/{GetPath(gameObject, new List<string>())}";
            if (I18NAudios.ContainsKey(component.ResName))
            {
                I18NAudios[component.ResName].PathList.Add(path);
            }
            else
            {
                var i18NAudio = new I18NMedia<AudioClip>
                {
                    Comment = component.I18NComment,
                    PathList = new List<string> {path},
                    Value = audioClip
                };
                I18NAudios.Add(component.ResName, i18NAudio);
            }
        }

        private string GetPath(GameObject gameObject, List<string> paths)
        {
            paths.Add(gameObject.name);
            if (gameObject.transform.parent)
            {
                return GetPath(gameObject.transform.parent.gameObject, paths);
            }
            paths.Reverse();
            return paths.Aggregate((a, b) => $"{a}/{b}");
        }
    }
}