using System.IO;
using TMPro;
using UnityEngine;

namespace Rosetta.Runtime.Loader
{
    /// <summary>
    ///     Load font/TMP_Font file in runtime and return a `Font`/`TMP_FontAsset` object
    /// </summary>
    public struct FontInfo
    {
        /// <summary>
        ///     The font file name.
        /// </summary>
        public string FontName;
        /// <summary>
        ///     The font size. Used only when reading font from the OS. 
        /// </summary>
        public int Size;
        /// <summary>
        ///     Whether it is a font in TMP format.
        /// </summary>
        public bool IsTmpFont;
        /// <summary>
        ///     Whether the font file is included in Resources. When this is no, the font will be loaded from the operating system.
        /// </summary>
        public bool IsDefaultInclude;
    }
    
    public class FontLoader : LoaderBase
    {
        public override T Load<T>(string _, string virtualFontName, LangFlag? flag = null)
        {
            LoadBase(Path.Combine("res", "font", virtualFontName), flag ?? Rosetta.Locale);
            ExtensionName = "json";
            var info = JsonUtility.FromJson<FontInfo>(LoadFile<string>());
            if (info.IsDefaultInclude)
            {
                if (info.IsTmpFont)
                {
                    return (T) (object) Resources.Load<TMP_FontAsset>(info.FontName);
                }
                return (T) (object) Resources.Load<Font>(info.FontName);
            }
            var font = Font.CreateDynamicFontFromOSFont(info.FontName, info.Size);
            return (T) (object) font;
        }
    }
}