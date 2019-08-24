using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Rosetta.Runtime.Component
{
    /// <summary>
    ///     Marking the current TMPText component requires I18N.
    /// </summary>
    [AddComponentMenu("Rosetta/I18NTMPText")]
    [RequireComponent(typeof(TMP_Text))]
    [HelpURL("https://molingyu.github.io/RosettaDocs/guides/makeI18NRes/component/I18NTMPText.html")]
    public class I18NTMPText : I18NComponentBase
    {
        private TMP_FontAsset _devFont;
        private string _devString;
        /// <summary>
        ///      If true, the corresponding i18n font is loaded via the `VirtualFontName` instead of the currently set font.
        /// </summary>
        public bool IsVirtualFont;
        /// <summary>
        ///     I18n font name.
        /// </summary>
        [ShowIf("IsVirtualFont")] public string VirtualFontName = "";

        private void Start()
        {
            _devString = gameObject.GetComponent<TMP_Text>().text;
            _devFont = gameObject.GetComponent<TMP_Text>().font;
            Create();
        }

        public override void Refresh()
        {
            var text = gameObject.GetComponent<TMP_Text>();
            text.text = Rosetta.GetText(_devString, I18NSpace);
            if (IsVirtualFont) text.font = Rosetta.IsDefault() ? _devFont : Rosetta.GetTMP_Font(VirtualFontName);
        }
    }
}