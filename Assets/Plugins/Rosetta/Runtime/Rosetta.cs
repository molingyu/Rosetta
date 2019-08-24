using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Rosetta.Runtime.Loader;
using Sirenix.Utilities;
using TMPro;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rosetta.Runtime
{
    /// <summary>
    ///     Rosetta i18n management at run-time.
    /// </summary>
    public static class Rosetta
    {
        /// <summary>
        ///     Default language for game development.
        /// </summary>
        public static LangFlag DevLocale;

        private static LangFlag _locale;

        /// <summary>
        ///     I18N File type of text file.
        /// </summary>
        public static I18NFileType I18NTextFileType;

        /// <summary>
        ///     The dictionary of language names.
        /// </summary>
        public static readonly Dictionary<LangFlag, string> LangNames = new Dictionary<LangFlag, string>
        {
            {LangFlag.AA, "Afaraf"},
            {LangFlag.AB, "Аҧсуа"},
            {LangFlag.AE, "avesta"},
            {LangFlag.AF, "Afrikaans"},
            {LangFlag.AK, "Akan"},
            {LangFlag.AM, "አማርኛ"},
            {LangFlag.AN, "Aragonés"},
            {LangFlag.AR, "العربية"},
            {LangFlag.AS, "অসমীয়া"},
            {LangFlag.AV, "магӀарул мацӀ"},
            {LangFlag.AY, "aymar aru"},
            {LangFlag.AZ, "azərbaycan dili"},
            {LangFlag.BA, "башҡорт теле"},
            {LangFlag.BE, "Беларуская"},
            {LangFlag.BG, "български език"},
            {LangFlag.BH, "भोजपुरी"},
            {LangFlag.BI, "Bislama"},
            {LangFlag.BM, "bamanankan"},
            {LangFlag.BN, "বাংলা"},
            {LangFlag.BO, "བོད་ཡིག"},
            {LangFlag.BR, "brezhoneg"},
            {LangFlag.BS, "bosanski jezik"},
            {LangFlag.CA, "Català"},
            {LangFlag.CE, "нохчийн мотт"},
            {LangFlag.CH, "Chamoru"},
            {LangFlag.CO, "lingua corsa"},
            {LangFlag.CR, "ᓀᐦᐃᔭᐍᐏᐣ"},
            {LangFlag.CS, "čeština"},
            {LangFlag.CU, "ѩзыкъ словѣньскъ"},
            {LangFlag.CV, "чӑваш чӗлхи"},
            {LangFlag.CY, "Cymraeg"},
            {LangFlag.DA, "dansk"},
            {LangFlag.DE, "Deutsch"},
            {LangFlag.DV, "ދިވެހި"},
            {LangFlag.DZ, "རྫོང་ཁ"},
            {LangFlag.EE, "Ɛʋɛgbɛ"},
            {LangFlag.EL, "Ελληνικά"},
            {LangFlag.EN, "English"},
            {LangFlag.EN_GB, "English(United Kingdom)"},
            {LangFlag.EO, "Esperanto"},
            {LangFlag.ES, "español"},
            {LangFlag.ET, "Eesti keel"},
            {LangFlag.EU, "euskara"},
            {LangFlag.FA, "فارسی"},
            {LangFlag.FF, "Fulfulde"},
            {LangFlag.FI, "suomen kieli"},
            {LangFlag.FJ, "vosa Vakaviti"},
            {LangFlag.FO, "Føroyskt"},
            {LangFlag.FR, "français"},
            {LangFlag.FY, "Frysk"},
            {LangFlag.GA, "Gaeilge"},
            {LangFlag.GD, "Gàidhlig"},
            {LangFlag.GL, "Galego"},
            {LangFlag.GN, "Avañe'ẽ"},
            {LangFlag.GU, "ગુજરાતી"},
            {LangFlag.GV, "Ghaelg"},
            {LangFlag.HA, "هَوُسَ"},
            {LangFlag.HE, "עברית"},
            {LangFlag.HI, "हिन्दी"},
            {LangFlag.HO, "Hiri Motu"},
            {LangFlag.HR, "Hrvatski"},
            {LangFlag.HT, "Kreyòl ayisyen"},
            {LangFlag.HU, "Magyar"},
            {LangFlag.HY, "Հայերեն"},
            {LangFlag.HZ, "Otjiherero"},
            {LangFlag.IA, "Interlingua"},
            {LangFlag.ID, "Bahasa Indonesia"},
            {LangFlag.IE, "Interlingue"},
            {LangFlag.IG, "Igbo"},
            {LangFlag.II, "ꆇꉙ"},
            {LangFlag.IK, "Iñupiaq"},
            {LangFlag.IO, "Ido"},
            {LangFlag.IS, "Íslenska"},
            {LangFlag.IT, "Italiano"},
            {LangFlag.IU, "ᐃᓄᒃᑎᑐᑦ"},
            {LangFlag.JA, "日本語(にほんご)"},
            {LangFlag.JV, "basa Jawa"},
            {LangFlag.KA, "ქართული"},
            {LangFlag.KG, "KiKongo"},
            {LangFlag.KI, "Gĩkũyũ"},
            {LangFlag.KJ, "Kuanyama"},
            {LangFlag.KK, "قازاق تىلى"},
            {LangFlag.KL, "kalaallisut"},
            {LangFlag.KM, "ភាសាខ្មែរ"},
            {LangFlag.KN, "ಕನ್ನಡ"},
            {LangFlag.KO_KR, "한국어(韓國語)"},
            {LangFlag.KO_KP, "조선말(朝鮮말)"},
            {LangFlag.KR, "Kanuri"},
            {LangFlag.KS, "कश्मीरी‎"},
            {LangFlag.KU, "كوردی‎"},
            {LangFlag.KV, "коми кыв"},
            {LangFlag.KW, "Kernewek"},
            {LangFlag.KY, "قىرعىز تىلى"},
            {LangFlag.LA, "latine"},
            {LangFlag.LB, "Lëtzebuergesch"},
            {LangFlag.LG, "Luganda"},
            {LangFlag.LI, "Limburgs"},
            {LangFlag.LN, "Lingála"},
            {LangFlag.LO, "ພາສາລາວ"},
            {LangFlag.LT, "lietuvių kalba"},
            {LangFlag.LU, "Tshiluba"},
            {LangFlag.LV, "latviešu valoda"},
            {LangFlag.MG, "Malagasy fiteny"},
            {LangFlag.MH, "Kajin M̧ajeļ"},
            {LangFlag.MI, "te reo Māori"},
            {LangFlag.MK, "македонски јазик"},
            {LangFlag.ML, "മലയാളം"},
            {LangFlag.MN, "Монгол хэл"},
            {LangFlag.MO, "moldovenească"},
            {LangFlag.MR, "मराठी"},
            {LangFlag.MS, "bahasa Melayu"},
            {LangFlag.MT, "Malti"},
            {LangFlag.MY, "ဗမာစာ"},
            {LangFlag.NA, "Ekakairũ Naoero"},
            {LangFlag.NB, "Norsk bokmål"},
            {LangFlag.ND, "isiNdebele"},
            {LangFlag.NE, "नेपाली"},
            {LangFlag.NG, "Owambo"},
            {LangFlag.NL, "Nederlands"},
            {LangFlag.NN, "Norsk nynorsk"},
            {LangFlag.NO, "Norsk"},
            {LangFlag.NR, "Ndébélé"},
            {LangFlag.NV, "Diné bizaad"},
            {LangFlag.NY, "chiCheŵa"},
            {LangFlag.OC, "Occitan"},
            {LangFlag.OJ, "ᐊᓂᔑᓈᐯᒧᐎᓐ"},
            {LangFlag.OM, "Afaan Oromoo"},
            {LangFlag.OR, "ଓଡ଼ିଆ"},
            {LangFlag.OS, "Ирон æвзаг"},
            {LangFlag.PA, "ਪੰਜਾਬੀ"},
            {LangFlag.PI, "पाऴि"},
            {LangFlag.PL, "polski"},
            {LangFlag.PS, "پښتو"},
            {LangFlag.PT, "Português"},
            {LangFlag.PT_BR, "Português(Brasil)"},
            {LangFlag.QU, "Runa Simi"},
            {LangFlag.RM, "rumantsch grischun"},
            {LangFlag.RN, "kiRundi"},
            {LangFlag.RO, "română"},
            {LangFlag.RU, "русский язык"},
            {LangFlag.RW, "Kinyarwanda"},
            {LangFlag.SA, "संस्कृतम्"},
            {LangFlag.SC, "sardu"},
            {LangFlag.SD, "सिन्धी"},
            {LangFlag.SE, "Davvisámegiella"},
            {LangFlag.SG, "yângâ tî sängö"},
            {LangFlag.SI, "සිංහල"},
            {LangFlag.SK, "slovenčina"},
            {LangFlag.SL, "slovenščina"},
            {LangFlag.SM, "gagana fa'a Samoa"},
            {LangFlag.SN, "gagana fa'a Samoa"},
            {LangFlag.SO, "Soomaaliga"},
            {LangFlag.SQ, "Shqip"},
            {LangFlag.SR, "српски језик"},
            {LangFlag.SS, "SiSwati"},
            {LangFlag.ST, "seSotho"},
            {LangFlag.SU, "Basa Sunda"},
            {LangFlag.SV, "Svenska"},
            {LangFlag.SW, "Kiswahili"},
            {LangFlag.TA, "தமிழ்"},
            {LangFlag.TE, "తెలుగు"},
            {LangFlag.TG, "тоҷикӣ"},
            {LangFlag.TH, "ไทย"},
            {LangFlag.TI, "ትግርኛ"},
            {LangFlag.TK, "Türkmen"},
            {LangFlag.TL, "Tagalog"},
            {LangFlag.TN, "seTswana"},
            {LangFlag.TO, "faka Tonga"},
            {LangFlag.TR, "Türkçe"},
            {LangFlag.TS, "xiTsonga"},
            {LangFlag.TT, "татарча"},
            {LangFlag.TW, "Twi"},
            {LangFlag.TY, "Reo Mā`ohi"},
            {LangFlag.UG, "ئۇيغۇرچە‎"},
            {LangFlag.UK, "Українська"},
            {LangFlag.UR, "اردو"},
            {LangFlag.UZ, "O'zbek"},
            {LangFlag.VE, "tshiVenḓa"},
            {LangFlag.VI, "Tiếng Việt"},
            {LangFlag.VO, "Volapük"},
            {LangFlag.WA, "Walon"},
            {LangFlag.WO, "Wollof"},
            {LangFlag.XH, "isiXhosa"},
            {LangFlag.YI, "ייִדיש"},
            {LangFlag.YO, "Yorùbá"},
            {LangFlag.ZA, "Saw cuengh"},
            {LangFlag.ZH_CN, "简体中文"},
            {LangFlag.ZH_TW, "正體中文"},
            {LangFlag.ZU, "isiZulu"}
        };

        // I18N Resource Caches
        private static List<string> _i18NTextSpaceCaches;

        /// <summary>
        ///     The I18N String cache.Storage by space.
        /// </summary>
        public static Dictionary<string, Dictionary<string, string>> I18NTextCache =
            new Dictionary<string, Dictionary<string, string>>();

        /// <summary>
        ///     The I18N Sprite cache.Storage by space.
        /// </summary>
        public static Dictionary<string, Dictionary<string, Sprite>> I18NSpriteCache =
            new Dictionary<string, Dictionary<string, Sprite>>();

        /// <summary>
        ///     The I18N AudioClip cache.Storage by space.
        /// </summary>
        public static Dictionary<string, Dictionary<string, AudioClip>> I18NAudioCache =
            new Dictionary<string, Dictionary<string, AudioClip>>();

        /// <summary>
        ///     The I18N Font cache.
        /// </summary>
        public static Dictionary<string, Font> I18NFontCache = new Dictionary<string, Font>();

        /// <summary>
        ///     The I18N TMP_FontAsset cache.
        /// </summary>
        public static Dictionary<string, TMP_FontAsset> I18NTMPFontCache = new Dictionary<string, TMP_FontAsset>();

        /// <summary>
        ///     All I18N File Loader.
        /// </summary>
        public static Dictionary<I18NFileType, LoaderBase> Loaders = new Dictionary<I18NFileType, LoaderBase>
        {
            {I18NFileType.Po, new PoLoader()},
            {I18NFileType.Png, new PngLoader()},
            {I18NFileType.Wav, new WavLoader()},
            {I18NFileType.Font, new FontLoader()}
        };
        
        public static MultiMediaLoader MultiMediaLoader = new MultiMediaLoader();

        /// <summary>
        ///     ToDo: error
        ///     I18N files load path.
        /// </summary>
        public static readonly List<string> LoadPath = new List<string>
        {
            $"{new Regex("/[^/]*$").Replace(Application.dataPath, "")}/I18N/"
        };

        /// <summary>
        ///     Default language for game runtime.
        /// </summary>
        public static LangFlag Locale
        {
            get => _locale;
            set
            {
                _locale = value;
                ChangeLocale();
            }
        }

        // Event
        /// <summary>
        ///     Subscribe to this event to get notified when the locale has changed.
        /// </summary>
        public static event Action<LangFlag> LocaleChanged;

        /// <summary>
        ///     Subscribe to this event to get notified when the I18N strings are missing.
        /// </summary>
        public static event Action<LangFlag, string, string> I18NStringMissing;

        /// <summary>
        ///     Subscribe to this event to get notified when the I18N Files are missing.
        /// </summary>
        public static event Action<LangFlag, Exception> I18NFileMissing = (_, __) => Locale = DevLocale;

        /// <summary>
        ///     Initialize the Rosetta by RosettaRuntimeSetting.
        /// </summary>
        /// <param name="locale">Default language for game runtime.</param>
        public static void Init(LangFlag? locale = null)
        {
            var setting = LoadSetting();
            Init(setting.DevLocale,
                locale ?? setting.DevLocale,
                setting.DefaultI18NSpaces,
                setting.TextFileType);
        }

        /// <summary>
        ///     Initialize the Rosetta.
        /// </summary>
        /// <param name="devLocale">Default language for game development.</param>
        /// <param name="locale">Default language for game runtime.</param>
        /// <param name="i18NSpaces">All Runtime default i18n space</param>
        /// <param name="textFileType">I18N File type of text file.</param>
        public static void Init(LangFlag devLocale,
            LangFlag locale,
            List<string> i18NSpaces,
            I18NFileType textFileType = I18NFileType.Po)
        {
#if UNITY_EDITOR || DEBUG
            I18NFileMissing += (flag, exception) => Debug.Log($"Lang:{flag}\n{exception.Message}");
#endif
            DevLocale = devLocale;
            _locale = locale;
            I18NTextFileType = textFileType;
            _i18NTextSpaceCaches = i18NSpaces;
            if (DevLocale != Locale) _i18NTextSpaceCaches.ForEach(LoadI18NResSpace);
        }

        /// <summary>
        ///     Returns the selected text for the current language.
        /// </summary>
        /// <param name="i18NString"></param>
        /// <param name="i18NSpace"></param>
        /// <returns></returns>
        public static string GetText(string i18NString, string i18NSpace)
        {
            if (DevLocale == Locale) return i18NString;
            var cache = I18NTextCache[i18NSpace];
            if (cache != null && !cache.ContainsKey(i18NString))
                return cache[i18NString];
            I18NStringMissing?.Invoke(Locale, i18NString, i18NSpace);
            return i18NString;
        }

        /// <summary>
        ///     Returns the selected Sprite resource for the current language.
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="i18NSpace"></param>
        /// <returns></returns>
        public static Sprite GetSprite(string resName, string i18NSpace)
        {
            try
            {
                var cache = I18NSpriteCache[i18NSpace];
                if (cache.ContainsKey(resName))
                {
                    var loader = Loaders[I18NFileType.Png];
                    cache.Add(resName, loader.Load<Sprite>(Path.Combine(resName)));
                }

                return cache[resName];
            }
            catch (Exception e)
            {
                I18NFileMissing?.Invoke(Locale, e);
                return null;
            }
        }

        /// <summary>
        ///     Returns the selected AudioClip resource for the current language.
        /// </summary>
        /// <param name="resName"></param>
        /// <param name="i18NSpace"></param>
        /// <returns></returns>
        public static AudioClip GetAudio(string resName, string i18NSpace)
        {
            try
            {
                var cache = I18NAudioCache[i18NSpace];
                if (cache.ContainsKey(resName))
                {
                    var loader = Loaders[I18NFileType.Wav];
                    cache.Add(resName, loader.Load<AudioClip>(Path.Combine(resName)));
                }

                return cache[resName];
            }
            catch (Exception e)
            {
                I18NFileMissing?.Invoke(Locale, e);
                return null;
            }
        }

        /// <summary>
        ///     Returns the selected Font resource for the current language.
        /// </summary>
        /// <param name="resName"></param>
        /// <returns></returns>
        public static Font GetFont(string resName)
        {
            if (!I18NFontCache.ContainsKey(resName))
            {
                var loader = Loaders[I18NFileType.Font];
                try
                {
                    I18NFontCache.Add(resName, loader.Load<Font>(Path.Combine(resName)));
                }
                catch (Exception e)
                {
                    I18NFileMissing?.Invoke(Locale, e);
                    return null;
                }
            }
            return I18NFontCache[resName];
        }

        /// <summary>
        ///     Returns the selected TMP_FontAsset resource for the current language.
        /// </summary>
        /// <param name="resName"></param>
        /// <returns></returns>
        public static TMP_FontAsset GetTMPFont(string resName)
        {
            if (!I18NTMPFontCache.ContainsKey(resName))
            {
                var loader = Loaders[I18NFileType.TMPFont];
                try
                {
                    I18NTMPFontCache.Add(resName, loader.Load<TMP_FontAsset>(Path.Combine(resName)));
                }
                catch (Exception e)
                {
                    I18NFileMissing?.Invoke(Locale, e);
                    return null;
                }
            }

            return I18NTMPFontCache[resName];
        }

        /// <summary>
        ///     Returns true if Locale equals DevLocale.
        /// </summary>
        /// <returns></returns>
        public static bool IsDefault()
        {
            return Locale == DevLocale;
        }

        /// <summary>
        ///     Load given space I18N Resource.
        /// </summary>
        /// <param name="space">Space to be unloaded</param>
        public static void LoadI18NResSpace(string space)
        {
            try
            {
                I18NTextCache[space] = Loaders[I18NTextFileType].Load<Dictionary<string, string>>(space, Locale);
                I18NSpriteCache[space] = Loaders[I18NFileType.Png].Load<Dictionary<string, Sprite>>(space, Locale);
                I18NAudioCache[space] = Loaders[I18NFileType.Wav].Load<Dictionary<string, AudioClip>>(space, Locale);
            }
            catch (Exception e)
            {
                I18NFileMissing?.Invoke(Locale, e);
            }
        }

        /// <summary>
        ///     Destroys and removes all given space I18N resource cache.
        /// </summary>
        /// <param name="space">Space to be unloaded</param>
        public static void UnloadI18NResSpace(string space)
        {
            _i18NTextSpaceCaches.Remove(space);
            I18NTextCache.Remove(space);
            I18NSpriteCache[space].Values.ForEach(sprite => Object.Destroy(sprite.texture));
            I18NSpriteCache.Remove(space);
            I18NAudioCache[space].Values.ForEach(audio => Object.Destroy(audio));
            I18NAudioCache.Remove(space);
        }

        /// <summary>
        ///     Clean All I18N Resource Cache(Text\Sprite\Audio\Font).
        /// </summary>
        public static void ClearAllCache()
        {
            _i18NTextSpaceCaches.ForEach(UnloadI18NResSpace);
            _i18NTextSpaceCaches.Clear();
            I18NTextCache.Clear();
            I18NSpriteCache.Clear();
            I18NAudioCache.Clear();
            I18NFontCache.Clear();
            I18NTMPFontCache.Clear();
        }

        private static void ChangeLocale()
        {
            if (DevLocale != Locale)
            {
                _i18NTextSpaceCaches.ForEach(LoadI18NResSpace);
                LocaleChanged?.Invoke(Locale);
            }
        }

        private static RosettaRuntimeSetting LoadSetting()
        {
            return Resources.Load("RosettaRuntimeSetting") as RosettaRuntimeSetting;
        }
    }
}