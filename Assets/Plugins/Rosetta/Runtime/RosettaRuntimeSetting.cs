using System;
using System.Collections.Generic;
using UnityEngine;

namespace Rosetta.Runtime
{
    [CreateAssetMenu(fileName = "RosettaRuntimeSetting", menuName = "Rosetta/RuntimeSetting")]
    [Serializable]
    public class RosettaRuntimeSetting : ScriptableObject
    {
        public List<string> DefaultI18NSpaces = new List<string> { "UI" };

        public LangFlag DevLocale = LangFlag.EN;

        [HideInInspector] public LangFlag Locale = LangFlag.EN;

        public I18NFileType TextFileType;
    }
}