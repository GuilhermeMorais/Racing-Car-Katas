namespace TDDMicroExercises.UnicodeFileToHtmlTextConverter
{
    public class UnicodeFileToHtmlTextConverter
    {
        private readonly IHttpUtility _httpUtility;
        private readonly ITextReader _textReader;

        public UnicodeFileToHtmlTextConverter(string fullFilenameWithPath)
         : this(new HttpUtility(), new FileTextReader(fullFilenameWithPath))
        {
        }

        public UnicodeFileToHtmlTextConverter(IHttpUtility httpUtility, ITextReader textReader)
        {
            _httpUtility = httpUtility;
            _textReader = textReader;
        }

        public string ConvertToHtml()
        {
            using (var reader = _textReader.GetTextReader())
            {
                var html = string.Empty;
                var line = reader.ReadLine();
                
                while (line != null)
                {
                    html += _httpUtility.HtmlEncode(line);
                    html += "<br />";
                    line = reader.ReadLine();
                }

                return html;
            }
        }
    }
}
