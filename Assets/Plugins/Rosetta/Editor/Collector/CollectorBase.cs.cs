using System;
using System.Collections.Generic;
using System.Linq;
using Rosetta.Editor.Creator;
using Rosetta.Runtime;
using Rosetta.Runtime.Loader;
using Sirenix.Utilities;
using UnityEngine;

namespace Rosetta.Editor.Collector
{
    [Serializable, HideInInspector]
    public abstract class CollectorBase
    {
        private static List<Type> _i18NClass;
        
        [HideInInspector] 
        public Dictionary<string, I18NMedia<AudioClip>> I18NAudios;

        [HideInInspector] 
        public Dictionary<string, I18NMedia<Sprite>> I18NImages;

        [HideInInspector] 
        public Dictionary<string, I18NMedia<FontInfo>> I18NFonts;

        [HideInInspector] 
        public Dictionary<string, I18NMedia<string>> I18NStrings;

        public static List<Type> I18NClass => _i18NClass ?? (_i18NClass = GetI18NClass());

        public abstract void Collect(string space);

        private static List<Type> GetI18NClass()
        {
            var subclasses =
                from assembly in AppDomain.CurrentDomain.GetAssemblies()
                from type in assembly.GetTypes()
                from attr in type.GetAttributes()
                where attr.GetType() == typeof(I18NClassAttribute)
                select type;
            return subclasses.ToList();
        }
    }
}