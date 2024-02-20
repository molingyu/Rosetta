using System.Linq;
using UnityEditor;
using static UnityEditor.EditorGUILayout;
using Rosetta.Runtime;
using System.Collections.Generic;


namespace Rosetta.Runtime.Custom
{
    [CustomEditor(typeof(RosettaRuntimeSetting))]
    public class RosettaRuntimeSettingEditor : UnityEditor.Editor
    {
        private static Dictionary<string, I18NFileType> textFileType = new Dictionary<string, I18NFileType>()
        {
            {"Po", I18NFileType.Po}
            //{"Mo", I18NFileType.Mo},
            //{"CSV", I18NFileType.Csv},
            //{"JSON", I18NFileType.Json},
        };

        public override void OnInspectorGUI()
        {
            var serSpaces = serializedObject.FindProperty("DefaultI18NSpaces");
            PropertyField(serSpaces, true);

            // DevLocale
            var langNames = Rosetta.LangNames;
            var displays = langNames.Select(i => $"{i.Value} ({i.Key})").ToArray();
            var langValues = langNames.Select(i => i.Key).ToArray();
            var serDevLocale = serializedObject.FindProperty("DevLocale");

            var prevVal = serDevLocale.enumValueIndex;
            var newVal = Popup("DevLocale", serDevLocale.enumValueIndex, displays);
            if (prevVal != newVal && EditorUtility.DisplayDialog("Switch Dev Locale language?",
                    "Are you sure you want to switch Dev Locale language?", "Switch", "Do Not Switch"))
                serDevLocale.enumValueIndex = (int)langValues[newVal];

            // TextFileType
            displays = textFileType.Select(i => i.Key).ToArray();
            var fileValues = textFileType.Select(i => i.Value).ToArray();
            var serTextFileType = serializedObject.FindProperty("TextFileType");
            serTextFileType.enumValueIndex = (int)fileValues[Popup("TextFileType", serTextFileType.enumValueIndex, displays)];
            serializedObject.ApplyModifiedProperties();
        }
    }
}