using System;
using UnityEngine;

namespace Rosetta.Runtime
{
    /// <summary>
    /// The substitude to Odin's [FolderPath] Attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class FolderPathAttribute : PropertyAttribute
    {
        public FolderPathAttribute() { }
    }
}