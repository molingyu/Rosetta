using System;
using System.IO;
using UnityEngine;

namespace Rosetta.Runtime.Loader
{
    /// <summary>
    ///     Record picture pivot and size.
    /// </summary>
    [Serializable]
    public struct ImageInfo
    {
        /// <summary>
        ///     The x value of image pivot.it equal to the x value of sprite pivot.
        /// </summary>
        public float PivotX;
        /// <summary>
        ///     The y value of image pivot.it equal to the y value of sprite pivot.
        /// </summary>
        public float PivotY;
        /// <summary>
        ///     The `width` of image.
        /// </summary>
        public int Width;
        /// <summary>
        ///     The `height` of image.
        /// </summary>
        public int Height;
        /// <summary>
        ///     The I18N file comment.
        /// </summary>
        public string Comment;
    }
    
    /// <summary>
    ///     Load `.png` file in runtime and return a `Sprite` object
    /// </summary>
    public class PngLoader : LoaderBase
    { 
        public override T Load<T>(string space,string filename, LangFlag? flag = null)
        {
            LoadBase(Path.Combine("res", "img", space, filename), flag ?? Rosetta.Locale);
            ExtensionName = "json";
            var info = JsonUtility.FromJson<ImageInfo>(LoadFile<string>());
            ExtensionName = "png";
            var tex = new Texture2D(info.Width, info.Height);
            tex.LoadImage(LoadFile<byte[]>());
            var sprite = Sprite.Create(tex, new Rect(new Vector2(0, 0), new Vector2(info.Width, info.Height)),
                new Vector2(info.PivotX, info.PivotY));
            sprite.name = $"{filename}_{(flag ?? Rosetta.Locale).ToString().ToLower()}";
            return (T) (object) sprite;
        }
    }
}