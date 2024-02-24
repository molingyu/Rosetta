using System;
using System.Collections.Generic;
using System.Linq;
using Rosetta.Runtime;
using UnityEngine;

namespace Rosetta.Editor.Creator
{
    [Serializable]
    public struct PotInfo
    {
        public string BugsAddress;
        public string CopyrightHolder;
        public string CopyrightYear;
        public string PackageName;
        public string PackageVersion;
    }

    [CreateAssetMenu(menuName = "Rosetta/Creator/PotCreator")]
    [Serializable]
    public class PotCreator : CreatorBase
    {
        public PotInfo Info;
        public string PotTitle;
        [HideInInspector]
        public RosettaRuntimeSetting RuntimeSetting;

        protected override void _create()
        {
            DirectoryCreate($"{OutputPath}/res/font/{Name}");
            RuntimeSetting = Resources.Load<RosettaRuntimeSetting>("RosettaRuntimeSetting");
            var str = "";
            str += GetHead();
            Collectors.ForEach(collector =>
            {
                collector.Collect(Name);
                if (collector.I18NStrings != null) 
                {
                    foreach (var i18NString in collector.I18NStrings.Values)
                    {
                        str += GetComment(i18NString.Comment, i18NString.PathList);
                        str += GetBody(i18NString.Value) + "\n";
                    }
                }    
            });
            SaveFile($"{OutputPath}/{Name}.pot", str);
        }

        private string GetHead()
        {
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:m:sszzz").Remove(22, 1);
            return $@"
# {PotTitle}
# Copyright (C) {Info.CopyrightYear} {Info.CopyrightHolder}
# This file is distributed under the same license as the {Info.PackageName} package.
# FIRST AUTHOR <EMAIL@ADDRESS>, {Info.CopyrightYear}.

msgid """"
msgstr """"

""Project-Id-Version: {Info.PackageName} {Info.PackageVersion}\n""
""Report-Msgid-Bugs-To: {Info.BugsAddress}\n""
""POT-Creation-Date: {time}\n""
""PO-Revision-Date: {time}\n""
""Last-Translator: FULL NAME <EMAIL@ADDRESS>\n""
""Language-Team: LANGUAGE <LL@li.org>\n""
""Language: {RuntimeSetting.DevLocale.ToString().ToLower()}\n""
""MIME-Version: 1.0\n""
""Content-Type: text/plain; charset=UTF-8\n""
""Content-Transfer-Encoding: 8bit\n""
""Plural-Forms: nplurals=INTEGER; plural=EXPRESSION;\n""

";
        }

        private string GetComment(string comment, List<string> paths)
        {
            var value = "";
            if (comment != "") value += $"#. {comment}";
            return paths.Aggregate(value, (current, path) => current + $"#: {path}\n");
        }

        private string GetBody(string value)
        {
            return $"msgid \"{value}\"\nmsgstr \"\"\n";
        }
    }
}