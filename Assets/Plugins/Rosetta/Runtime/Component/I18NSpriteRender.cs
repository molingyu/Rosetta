using UnityEngine;

namespace Rosetta.Runtime.Component
{
    /// <summary>
    ///     Marking the current SpriteRenderer component requires I18N.
    /// </summary>
    [AddComponentMenu("Rosetta/I18NSpriteRenderer")]
    [RequireComponent(typeof(SpriteRenderer))]
    [HelpURL("https://molingyu.github.io/RosettaDocs/guides/makeI18NRes/component/I18NSpriteRenderer.html")]
    public class I18NSpriteRenderer : I18NComponentBase
    {
        private Sprite _devSprite;
        /// <summary>
        ///     I18N image name.
        /// </summary>
        public string ResName;

        private void Start()
        {
            _devSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            Create();
        }

        public override void Refresh()
        {
            var image = gameObject.GetComponent<SpriteRenderer>();
            image.sprite = Rosetta.IsDefault() ? _devSprite : Rosetta.GetSprite(ResName, I18NSpace);
        }
    }
}