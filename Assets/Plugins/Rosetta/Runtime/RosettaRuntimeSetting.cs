using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEditor;
using UnityEngine;

namespace Rosetta.Runtime
{
    [CreateAssetMenu(fileName = "RosettaRuntimeSetting", menuName = "Rosetta/RuntimeSetting")]
    [Serializable]
    public class RosettaRuntimeSetting : SerializedScriptableObject
    {
        public List<string> DefaultI18NSpaces = new List<string> { "UI" };

        [ValueDropdown("langFlag")]
        [OnValueChanged("SwitchDevLocale")]
        public LangFlag DevLocale = LangFlag.EN;

        [HideInInspector] public LangFlag Locale = LangFlag.EN;

        [ValueDropdown("textFileType")] public I18NFileType TextFileType;

#if UNITY_EDITOR
        private LangFlag _devLocale;

        private static IEnumerable langFlag()
        {
            var list = new ValueDropdownList<LangFlag>();
            Rosetta.LangNames.ForEach(pair => list.Add(new ValueDropdownItem<LangFlag>($"{pair.Value} ({pair.Key.ToString()})", pair.Key)));
            return list;
        }
        
        private static IEnumerable textFileType = new ValueDropdownList<I18NFileType>
        {
            {"Po", I18NFileType.Po}
            //{"Mo", I18NFileType.Mo},
            //{"CSV", I18NFileType.Csv},
            //{"JSON", I18NFileType.Json},
        };

        private void SwitchDevLocale()
        {
            if (EditorUtility.DisplayDialog("Switch Dev Locale language?",
                "Are you sure you want to switch Dev Locale language?", "Switch", "Do Not Switch"))
                _devLocale = DevLocale;
            else
                DevLocale = _devLocale;
        }
#endif
    }
}