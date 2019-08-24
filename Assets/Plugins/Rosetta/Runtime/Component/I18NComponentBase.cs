using Sirenix.OdinInspector;
using UnityEngine;

namespace Rosetta.Runtime.Component
{
    public abstract class I18NComponentBase : MonoBehaviour
    {
        /// <summary>
        ///     Comments to the translator will be used for the pot file "#." comment.
        /// </summary>
        [MultiLineProperty] 
        public string I18NComment = "";

        /// <summary>
        ///     
        /// </summary>
        public string I18NSpace = "ui";

        /// <summary>
        ///     I18N Component initialization method.Will add the current component's `RosettaOnLocaleChanged` method
        ///     to <see cref="Rosetta.LocaleChanged">Rosetta.LocaleChanged</see> event.
        ///     This subscription is removed when the component is destroyed. 
        /// </summary>
        protected void Create()
        {
            Refresh(); 
            Rosetta.LocaleChanged += RosettaOnLocaleChanged;
        }

        /// <summary>
        ///     Call this method when the `LocaleChanged` event is triggered.
        /// </summary>
        public abstract void Refresh();
        
        void OnDestroy()
        {
            Rosetta.LocaleChanged -= RosettaOnLocaleChanged;
        }
        
        private void RosettaOnLocaleChanged(LangFlag _)
        {
            Refresh();
        }
    }
}