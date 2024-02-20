using UnityEngine;
using UnityEditor;
using static UnityEditor.EditorGUILayout;
using System;
using System.Linq;
using Rosetta.Editor.Creator;
using Rosetta.Editor.Collector;

namespace Rosetta.Editor.Custom.Creator
{
    [CustomEditor(typeof(CreatorBase), true)]
    public class CreatorBaseEditor : UnityEditor.Editor
    {
        private static Type[] collectorTypes;

        private void OnEnable()
        {
            collectorTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(assem =>
                assem.GetTypes().Where(type => type.IsSubclassOf(typeof(CollectorBase)) && !type.IsAbstract && !type.GetCustomAttributes(false).Any(attr => attr is HideInInspector))
            ).ToArray();
        }

        public override void OnInspectorGUI()
        {
            var creator = target as CreatorBase;
            var path = AssetDatabase.GetAssetPath(target);
            var collectors = AssetDatabase.LoadAllAssetsAtPath(path);
            var serCreators = serializedObject.FindProperty("Collectors");
            serCreators.arraySize = Mathf.Max(0, collectors.Length - 1);

            if (GUILayout.Button("Add Collector", GUILayout.Height(20)))
            {
                var menu = new GenericMenu();
                foreach (var type in collectorTypes)
                {
                    menu.AddItem(new GUIContent(type.Name), false,
                    () =>
                    {
                        var collector = CreateInstance(type);
                        // collector.hideFlags |= HideFlags.HideInHierarchy;
                        AssetDatabase.AddObjectToAsset(collector, target);
                        AssetDatabase.SaveAssets();
                    });
                }
                menu.ShowAsContext();
            }

            int idx = 0;
            foreach (var item in collectors)
            {
                GUILayout.Space(20f);
                if (item == target) continue;

                serCreators.GetArrayElementAtIndex(idx++).objectReferenceValue = item;
                // item.hideFlags = ShowSubObjs ? HideFlags.None : HideFlags.HideInHierarchy;

                BeginHorizontal();
                var itemName = item.GetType().Name;
                LabelField(itemName, EditorStyles.boldLabel);
                if (GUILayout.Button("Remove") && EditorUtility.DisplayDialog("Remove Collector", $"Are you sure you'd like to remove {itemName}?", "Yes", "No"))
                {
                    AssetDatabase.RemoveObjectFromAsset(item);
                    AssetDatabase.SaveAssets();
                    break;
                }
                EndHorizontal();
                EditorGUI.indentLevel++;
                var editor = CreateEditor(item);
                editor.OnInspectorGUI();
                EditorGUI.indentLevel--;
            }

            GUILayout.Space(30f);
            serializedObject.ApplyModifiedProperties();

            base.OnInspectorGUI();

            if (GUILayout.Button("Create I18N file", GUILayout.Height(30)))
                (target as CreatorBase).Create();
            AssetDatabase.SaveAssets();
            serializedObject.ApplyModifiedProperties();
        }
    }
}