using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Rosetta.Runtime.Component
{
    /// <summary>
    ///     Marking the current Text component requires I18N.
    /// </summary>
    [AddComponentMenu("Rosetta/I18NText")]
    [RequireComponent(typeof(Text))]
    [HelpURL("https://molingyu.github.io/RosettaDocs/guides/makeI18NRes/component/I18NText.html")]
    public class I18NText : I18NComponentBase
    {
        private Font _devFont;
        private string _devString;
        /// <summary>
        ///      When this is true, the corresponding i18n font is loaded via the `VirtualFontName` instead of the currently set font.
        /// </summary>
        public bool IsVirtualFont;

        /// <summary>
        ///     I18n font name.
        /// </summary>
        [ShowIf("IsVirtualFont")] public string VirtualFontName = "";

        private void Start()
        {
            _devString = gameObject.GetComponent<Text>().text;
            _devFont = gameObject.GetComponent<Text>().font;
            Create();
        }

        public override void Refresh()
        {
            var text = gameObject.GetComponent<Text>();
            text.text = Rosetta.GetText(_devString, I18NSpace);
            if (IsVirtualFont) text.font = Rosetta.IsDefault() ? _devFont : Rosetta.GetFont(VirtualFontName);
        }
    }
}