using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Rosetta.Runtime
{
    [CreateAssetMenu(fileName = "RuntimeSetting", menuName = "Rosetta/RuntimeSetting")]
    [Serializable]
    public class RosettaRuntimeSetting : SerializedScriptableObject
    {
        public List<string> DefaultI18NSpaces = new List<string> { "UI" };

        [OnValueChanged("SwitchDevLocale")] public LangFlag DevLocale = LangFlag.EN;

        [HideInInspector] public LangFlag? Locale = null;

        [ValueDropdown("_textFileType")] public I18NFileType TextFileType;

#if UNITY_EDITOR
        private LangFlag _devLocale;

        private static IEnumerable _textFileType = new ValueDropdownList<I18NFileType>
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
