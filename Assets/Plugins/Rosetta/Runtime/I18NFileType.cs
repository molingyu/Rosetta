namespace Rosetta.Runtime
{
    /// <summary>
    ///     I18N file type. Each file type has a corresponding loader.
    /// </summary>
    public enum I18NFileType
    {
        Po,
        Png,
        Wav,
        Font,

        // TODO: Waiting for support
        TMPFont,
        Mo,
        Json,
        Xml,
        Csv
    }
}