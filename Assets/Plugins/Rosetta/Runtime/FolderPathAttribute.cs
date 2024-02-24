using System;
using UnityEngine;

namespace Rosetta.Runtime
{
    /// <summary>
    /// The substitute to Odin's [FolderPath] Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class FolderPathAttribute : PropertyAttribute
    {
        public FolderPathAttribute() { }
    }
}