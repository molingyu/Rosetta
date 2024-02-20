using UnityEditor;
using Rosetta.Runtime.Component;

namespace Rosetta.Editor.Component.Custom
{
    [CustomEditor(typeof(I18NTMPText))]
    public class I18NTMPTextEditor : UnityEditor.Editor
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