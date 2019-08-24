using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Rosetta.Runtime.Component
{
    /// <summary>
    ///     Marking the current Image component requires I18N.
    /// </summary>
    [AddComponentMenu("Rosetta/I18NImage")]
    [RequireComponent(typeof(Image))]
    [HelpURL("https://molingyu.github.io/RosettaDocs/guides/makeI18NRes/component/I18NImage.html")]
    public class I18NImage : I18NComponentBase
    {
        private Sprite _devSprite;
       
        /// <summary>
        ///     I18N image name.
        /// </summary>
        public string ResName;
        
        private void Start()
        {
            _devSprite = gameObject.GetComponent<Image>().sprite;
            Create();
        }

        public override void Refresh()
        {
            var image = gameObject.GetComponent<Image>();
            image.sprite = Rosetta.IsDefault() ? _devSprite : Rosetta.GetSprite(ResName, I18NSpace);
        }
        
#if UNITY_EDITOR

        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            if (EditorApplication.isPlaying) return;
            TextureImporter textureImporter = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(gameObject.GetComponent<Image>().sprite.texture)) as TextureImporter;
            if (!textureImporter.isReadable)
                UnityEditor.EditorGUILayout.HelpBox("The Image source sprite is not readable.", UnityEditor.MessageType.Error);
        }

#endif


    }
}