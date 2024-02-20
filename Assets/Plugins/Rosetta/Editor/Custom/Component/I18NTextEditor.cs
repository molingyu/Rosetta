using UnityEditor;
using Rosetta.Runtime.Component;

namespace Rosetta.Editor.Component.Custom
{
    [CustomEditor(typeof(I18NText))]
    public class I18NTextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var showFont = serializedObject.FindProperty("IsVirtualFont");
            EditorGUILayout.PropertyField(showFont);
            if (showFont.boolValue) EditorGUILayout.PropertyField(serializedObject.FindProperty("VirtualFontName"));
            serializedObject.ApplyModifiedProperties();
        }
    }
}