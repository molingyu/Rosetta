using System;
using System.Collections.Generic;
using System.Linq;
using Rosetta.Editor.Creator;
using Rosetta.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Rosetta.Editor.Collector
{
    [Serializable]
    public class ScriptableObjectCollector : CollectorBase
    {
        [FolderPath]
        public string DataFolderPath = "Data/Resources";
        [InlineButton("Refresh")]
        public List<ScriptableObject> DataList = new List<ScriptableObject>();

        private void Refresh()
        {
            DataList.Clear();
            var guids = AssetDatabase.FindAssets("t:" + typeof(ScriptableObject).Name, new[] {DataFolderPath});
            for (var i = 0; i < guids.Length; i++)
            {
                var path = AssetDatabase.GUIDToAssetPath(guids[i]);
                var data = AssetDatabase.LoadAssetAtPath(path, typeof(ScriptableObject)) as ScriptableObject;
                if (!DataList.Contains(data)) DataList.Add(data);
            }
        }

        public override void Collect(string space)
        {
            var i18NClass = I18NClass;
            foreach (var obj in DataList)
            {
                var type = obj.GetType();
                foreach (var classType in i18NClass)
                    if (type == classType)
                    {
                        // I18NString
                        I18NStringPass(classType, type, obj);
                        // I18NStringList
                        I18NStringListPass(classType, type, obj);
                        // I18NAudio
                        I18NAudioPass(classType, type, obj);
                        // I18NAudioList
                        I18NAudioListPass(classType, type, obj);
                        // I18NImage
                        I18NImagePass(classType, type, obj);
                        // I18NImageList
                        I18NImageListPass(classType, type, obj);
                    }
            }
        }
        
        private void I18NStringPass(Type classType, Type type, ScriptableObject obj)
        {
            var fields =
                from field in classType.GetFields()
                from attr in field.GetAttributes()
                where attr is I18NStringAttribute
                select field;
            fields.ForEach(field =>
            {
                var path =
                    $"Database[{type.ToString()}]:/InstanceID:{obj.GetInstanceID()}/FieldName:{field.Name}";
                var text = field.GetValue(obj) as string;

                if (I18NStrings.ContainsKey(text ?? throw new InvalidOperationException()))
                {
                    I18NStrings[text].PathList.Add(path);
                }
                else
                {
                    var i18NString = new I18NMedia<string>
                    {
                        Comment = field.GetAttribute<I18NStringAttribute>().Comment,
                        PathList = new List<string> {path},
                        Value = text
                    };
                    I18NStrings.Add(text, i18NString);
                }
            });
        }

        private void I18NStringListPass(Type classType, Type type, ScriptableObject obj)
        {
            var fields = from field in classType.GetFields()
                from attr in field.GetAttributes()
                where attr is I18NStringListAttribute
                select field;
            fields.ForEach(field =>
            {
                var path =
                    $"Database[{type.ToString()}]:/InstanceID:{obj.GetInstanceID()}/FieldName:{field.Name}";
                if (field.GetValue(obj) is List<string> texts)
                    texts.ForEach(text =>
                    {
                        if (I18NStrings.ContainsKey(text ?? throw new InvalidOperationException()))
                        {
                            I18NStrings[text].PathList.Add(path);
                        }
                        else
                        {
                            var i18NString = new I18NMedia<string>
                            {
                                Comment = field.GetAttribute<I18NStringListAttribute>().Comment,
                                PathList = new List<string> {path},
                                Value = text
                            };
                            I18NStrings.Add(text, i18NString);
                        }
                    });
            });
        }
        
        private void I18NAudioPass(Type classType, Type type, ScriptableObject obj)
        {
            var fields = from field in classType.GetFields()
                from attr in field.GetAttributes()
                where attr is I18NAudioAttribute
                select field;
            fields.ForEach(field =>
            {
                var path =
                    $"Database[{type.ToString()}]:/InstanceID:{obj.GetInstanceID()}/FieldName:{field.Name}";
                var value = field.GetValue(obj) as AudioClip;
                var audioName = (value ? value : throw new InvalidOperationException()).name;

                if (I18NAudios.ContainsKey(audioName))
                {
                    I18NAudios[audioName].PathList.Add(path);
                }
                else
                {
                    var i18NAudio = new I18NMedia<AudioClip>
                    {
                        Comment = field.GetAttribute<I18NAudioAttribute>().Comment,
                        PathList = new List<string> {path},
                        Value = value
                    };
                    I18NAudios.Add(audioName, i18NAudio);
                }
            });
        }
        
        private void I18NAudioListPass(Type classType, Type type, ScriptableObject obj)
        {
            var fields = from field in classType.GetFields()
                from attr in field.GetAttributes()
                where attr is I18NAudioListAttribute
                select field;
            fields.ForEach(field =>
            {
                var path =
                    $"Database[{type.ToString()}]:/InstanceID:{obj.GetInstanceID()}/FieldName:{field.Name}";
                var audios = field.GetValue(obj) as List<AudioClip>;
                (audios ?? throw new InvalidOperationException()).ForEach(audio =>
                {
                    var audioName = audio.name;
                    if (I18NAudios.ContainsKey(audioName))
                    {
                        I18NAudios[audioName].PathList.Add(path);
                    }
                    else
                    {
                        var i18NAudio = new I18NMedia<AudioClip>
                        {
                            Comment = field.GetAttribute<I18NAudioListAttribute>().Comment,
                            PathList = new List<string> {path},
                            Value = audio
                        };
                        I18NAudios.Add(audioName, i18NAudio);
                    }
                });
            });
        }

        private void I18NImagePass(Type classType, Type type, ScriptableObject obj)
        {
            var fields = from field in classType.GetFields()
                from attr in field.GetAttributes()
                where attr is I18NImageAttribute
                select field;
            fields.ForEach(field =>
            {
                var path =
                    $"Database[{type.ToString()}]:/InstanceID:{obj.GetInstanceID()}/FieldName:{field.Name}";
                var value = field.GetValue(obj) as Sprite;
                var imageName = (value ? value : throw new InvalidOperationException()).name;

                if (I18NImages.ContainsKey(imageName))
                {
                    I18NImages[imageName].PathList.Add(path);
                }
                else
                {
                    var i18NImage = new I18NMedia<Sprite>
                    {
                        Comment = field.GetAttribute<I18NImageAttribute>().Comment,
                        PathList = new List<string> {path},
                        Value = value
                    };
                    I18NImages.Add(imageName, i18NImage);
                }
            });
        }
        
        private void I18NImageListPass(Type classType, Type type, ScriptableObject obj)
        {
            var fields = from field in classType.GetFields()
                from attr in field.GetAttributes()
                where attr is I18NImageListAttribute
                select field;
            fields.ForEach(field =>
            {
                var path =
                    $"Database[{type.ToString()}]:/InstanceID:{obj.GetInstanceID()}/FieldName:{field.Name}";
                var images = field.GetValue(obj) as List<Sprite>;
                (images ?? throw new InvalidOperationException()).ForEach(image =>
                {
                    var imageName = image.name;
                    if (I18NAudios.ContainsKey(imageName))
                    {
                        I18NAudios[imageName].PathList.Add(path);
                    }
                    else
                    {
                        var i18NImage = new I18NMedia<Sprite>
                        {
                            Comment = field.GetAttribute<I18NImageListAttribute>().Comment,
                            PathList = new List<string> {path},
                            Value = image
                        };
                        I18NImages.Add(imageName, i18NImage);
                    }
                });
            });
        }
    }
}