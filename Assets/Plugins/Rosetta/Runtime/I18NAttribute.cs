using System;

namespace Rosetta.Runtime
{
    [AttributeUsage(AttributeTargets.Field)]
    public class I18NStringAttribute : Attribute
    {
        public string Comment;

        /// <summary>
        ///     Indicate this is a string field that requires I18N translation.
        /// </summary>
        /// <param name="comment">Comments to the translator will be used for the pot file "#." comment.</param>
        public I18NStringAttribute(string comment = "")
        {
            Comment = comment;
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class I18NStringListAttribute : Attribute
    {
        public string Comment;

        /// <summary>
        ///     Indicate this is a string list field that requires I18N translation.
        /// </summary>
        /// <param name="comment">Comments to the translator will be used for the pot file "#." comment.</param>
        public I18NStringListAttribute(string comment = "")
        {
            Comment = comment;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class I18NImageAttribute : Attribute
    {

        public string Comment;

        /// <summary>
        ///     Indicate this is a image(sprite) list field that requires I18N translation.
        /// </summary>
        /// <param name="comment">Comments to the translator will be used for the pot file "#." comment.</param>
        public I18NImageAttribute(string comment = "")
        {
            Comment = comment;
        }
    }
    
    [AttributeUsage(AttributeTargets.Field)]
    public class I18NImageListAttribute : Attribute
    {

        public string Comment;

        /// <summary>
        ///     Indicate this is a image(sprite) list field that requires I18N translation.
        /// </summary>
        /// <param name="comment">Comments to the translator will be used for the pot file "#." comment.</param>
        public I18NImageListAttribute(string comment = "")
        {
            Comment = comment;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class I18NAudioAttribute : Attribute
    {

        public string Comment;

        /// <summary>
        ///     Indicate this is a audio(AudioClip) field that requires I18N translation.
        /// </summary>
        /// <param name="comment">Comments to the translator will be used for the pot file "#." comment.</param>
        public I18NAudioAttribute(string comment = "")
        {
            Comment = comment;
        }
    }

    [AttributeUsage(AttributeTargets.Field)]
    public class I18NAudioListAttribute : Attribute
    {

        public string Comment;

        /// <summary>
        ///     Indicate this is a audio(AudioClip) list field that requires I18N translation.
        /// </summary>
        /// <param name="comment">Comments to the translator will be used for the pot file "#." comment.</param>
        public I18NAudioListAttribute(string comment = "")
        {
            Comment = comment;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class I18NClassAttribute : Attribute
    {
    }
}