using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Rosetta.Editor.Collector
{
    [Serializable]
    public class PrefabCollector : PrefabCollectorBase
    {
        [AssetsOnly]
        public List<GameObject> PrefabList = new List<GameObject>();

        public override void Collect(string space)
        {
            PrefabList.ForEach(prefab =>
            {
                GameObjects = new List<GameObject>{prefab};
                PrefixName = $"Scene[{prefab.name}]:";
                base.Collect(space);
            });
        }
    }
}