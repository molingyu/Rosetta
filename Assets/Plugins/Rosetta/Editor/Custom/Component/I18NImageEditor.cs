using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using Rosetta.Runtime.Component;

namespace Rosetta.Editor.Component.Custom
{
    [CustomEditor(typeof(I18NImage))]
    public class I18NImageEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (EditorApplication.isPlaying) return;
            TextureImporter textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(
                (target as GameObject)?.GetComponent<Image>().sprite.texture)) as TextureImporter;
            if (textureImporter != null && !textureImporter.isReadable)
                EditorGUILayout.HelpBox("The Image source sprite is not readable.", UnityEditor.MessageType.Error);
            serializedObject.ApplyModifiedProperties();
        }
    }
}