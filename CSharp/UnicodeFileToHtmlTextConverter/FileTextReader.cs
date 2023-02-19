using System.IO;

namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter;

public class FileTextReader : ITextReader
{
    private readonly string _fullFilenameWithPath;

    public FileTextReader(string fullFilenameWithPath)
    {
        _fullFilenameWithPath = fullFilenameWithPath;
    }

    public TextReader GetTextReader()
    {
        return File.OpenText(_fullFilenameWithPath);
    }
}