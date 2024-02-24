using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Rosetta.Runtime.Loader
{
    public abstract class LoaderBase
    {
        /// <summary>
        ///     The extension of the file to be loaded.
        /// </summary>
        public string ExtensionName;
        /// <summary>
        ///     The LangFlag of the file to be loaded.
        /// </summary>
        public LangFlag LangFlag;
        /// <summary>
        ///     The file name of the file to be loaded.
        /// </summary>
        public string Name;

        protected void LoadBase(string filename, LangFlag flag)
        {
            Name = filename;
            LangFlag = flag;
        }

        /// <summary>
        /// Load i18n file by LangFlag.
        /// </summary>
        /// <typeparam name="T">The file load type(string or binary)</typeparam>
        /// <param name="space">the file space of the to be loaded</param>
        /// <param name="filename">The file name of the file to be loaded.</param>
        /// <param name="flag">The LangFlag of the file to be loaded.</param>
        /// <returns></returns>
        public abstract T Load<T>(string space, string filename, LangFlag? flag = null);

        /// <summary>
        ///     Loading i18n files will be loaded in two ways: binary or string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T LoadFile<T>()
        {
            foreach (var path in Rosetta.LoadPath.Where(path => File.Exists(GetFilePath(path))))
                if (typeof(T) == typeof(string))
                    return (T)(object)File.ReadAllText(GetFilePath(path), Encoding.UTF8);
                else if (typeof(T) == typeof(byte[])) return (T)(object)File.ReadAllBytes(GetFilePath(path));
            throw new Exception(
                $"File Load fail:\n{Rosetta.LoadPath.Select(GetFilePath).Aggregate((a, b)=> $"{a}\n{b}")}");
        }

        /// <summary>
        ///     Returns the actual path of the I18N file to be loaded.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetFilePath(string path)
        {
            return Path.Combine(path, LangFlag.ToString().ToLower(), $"{Name}.{ExtensionName}");
        }

    }
}
