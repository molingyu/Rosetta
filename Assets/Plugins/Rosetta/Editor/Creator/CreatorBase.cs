using System.Collections.Generic;
using System.IO;
using System.Text;
using Rosetta.Editor.Collector;
using Rosetta.Runtime;
using UnityEngine;

namespace Rosetta.Editor.Creator
{
    public struct I18NString
    {
        public List<string> PathList;
        public string Comment;
        public string Value;
    }

    public struct I18NMedia<T>
    {
        public List<string> PathList;
        public string Comment;
        public T Value;
    }
        
    
    public abstract class CreatorBase : ScriptableObject
    {
        [HideInInspector]
        public List<CollectorBase> Collectors = new();
        public string Name;
        [FolderPath] public string OutputPath;

        protected abstract void _create();

        public void Create()
        {
            _create();
        }
        protected void SaveFile(string path, string value)
        {
            File.WriteAllText(path, value, Encoding.UTF8);
        }
        
        protected void SaveFile(string path, byte[] value)
        {
            File.WriteAllBytes(path, value);
        }

        protected void DirectoryCreate(string path)
        {
            DirectoryInfo di = new DirectoryInfo(path);
            if (!di.Exists) di.Create();
        }
    }
}