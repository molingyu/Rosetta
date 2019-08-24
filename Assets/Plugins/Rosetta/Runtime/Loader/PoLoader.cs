using System.Collections.Generic;
using System.Linq;

namespace Rosetta.Runtime.Loader
{
    /// <summary>
    ///     Po file Loader.See more information about <c>.po</c> files here(https://www.gnu.org/software/gettext/manual/html_node/PO-Files.html).
    /// </summary>
    public class PoLoader : LoaderBase
    {
        public override T Load<T>(string _, string filename, LangFlag? flag = null)
        {
            ExtensionName = "po";
            LoadBase(filename, flag ?? Rosetta.Locale);
            return (T)(object)Parse(LoadFile<string>());
        }

        /// <summary>
        ///     Parse the Po file string into a C# data `Dictionary` and return it.
        /// </summary>
        /// <param name="str">The Po file string</param>
        /// <returns></returns>
        public Dictionary<string, string> Parse(string str)
        {
            var catalog = new Dictionary<string, string>();
            string key = null;
            string val = null;
            str.Split('\n').ToList().ForEach(line =>
            {
                if (line.StartsWith("msgid \""))
                {
                    key = line.Substring(7, line.Length - 8);
                }
                else if (line.StartsWith("msgstr \""))
                {
                    val = line.Substring(8, line.Length - 9);
                }
                else
                {
                    if (key != null && val != null)
                    {
                        catalog.Add(key, val);
                        key = val = null;
                    }
                }
            });
            return catalog;
        }
    }
}