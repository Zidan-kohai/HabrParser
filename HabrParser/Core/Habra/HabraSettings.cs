
namespace HabrParser.Core.Habra
{
    internal class HabraSettings : IParserSettings
    {
        public HabraSettings(int start, int end) 
        {
           StartPoint = start;
           EndPoint = end;
        }
        public string BaseUrl { get; set; } = "https://habr.com/ru";
        public string Prefix { get; set; } = "pageCurrentId";
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
