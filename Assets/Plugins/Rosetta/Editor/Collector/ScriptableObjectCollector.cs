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
            var i18NStrings = new Dictionary<string, I18NMedia<string>>();
            var i18NClass = I18NClass;
            foreach (var obj in DataList)
            {
                var type = obj.GetType();
                foreach (var classType in i18NClass)
                    if (type == classType)
                    {
                        //I18NString
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

                            if (i18NStrings.ContainsKey(text ?? throw new InvalidOperationException()))
                            {
                                i18NStrings[text].PathList.Add(path);
                            }
                            else
                            {
                                var i18NString = new I18NMedia<string>
                                {
                                    Comment = field.GetAttribute<I18NStringAttribute>().Comment,
                                    PathList = new List<string> {path},
                                    Value = text
                                };
                                i18NStrings.Add(text, i18NString);
                            }
                        });
                        
                        // I18NStringList
                        fields =
                            from field in classType.GetFields()
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
                                    if (i18NStrings.ContainsKey(text ?? throw new InvalidOperationException()))
                                    {
                                        i18NStrings[text].PathList.Add(path);
                                    }
                                    else
                                    {
                                        var i18NString = new I18NMedia<string>
                                        {
                                            Comment = field.GetAttribute<I18NStringListAttribute>().Comment,
                                            PathList = new List<string> {path},
                                            Value = text
                                        };
                                        i18NStrings.Add(text, i18NString);
                                    }
                                });
                        });
                    }
            }

            I18NStrings = i18NStrings;
        }
    }
}