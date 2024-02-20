using UnityEngine;
using UnityEditor;
using Rosetta.Runtime;

namespace Rosetta.Editor.Custom.Property
{
    [CustomPropertyDrawer(typeof(FolderPathAttribute), true)]
    public class FolderPathDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(new Rect(position.x, position.y, position.width - 100, position.height),
                                    property, label);
            if (GUI.Button(new Rect(position.x + position.width - 80, position.y, 80, position.height), "Select"))
            {
                var selection = EditorUtility.OpenFolderPanel("Select Folder", property.stringValue, "Assets");
                int assetIdx = selection.IndexOf("Assets");
                if (selection.Trim().Length == 0) return;
                if (assetIdx == -1)
                    EditorUtility.DisplayDialog("Warning", "You need to specify a folder within the Assets folder.", "OK");
                else property.stringValue = selection.Substring(assetIdx);
            }
        }
    }
}