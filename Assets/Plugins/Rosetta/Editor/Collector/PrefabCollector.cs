using System.Collections.Generic;
using UnityEngine;

namespace Rosetta.Editor.Collector
{
    public class PrefabCollector : PrefabCollectorBase
    {
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