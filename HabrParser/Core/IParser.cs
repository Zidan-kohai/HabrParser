using AngleSharp.Html.Dom;

namespace HabrParser.Core
{
    internal interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
        
    }
}
