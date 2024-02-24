using UnityEngine;
using UnityEditor;
using Rosetta.Editor.Collector;

namespace Rosetta.Editor.Custom.Collector
{
    [CustomEditor(typeof(ScriptableObjectCollector))]
    public class ScriptableObjectCollectorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Refresh"))
                (target as ScriptableObjectCollector)?.Refresh();
            serializedObject.ApplyModifiedProperties();
        }
    }
}