using AngleSharp.Html.Parser;
using System;

namespace HabrParser.Core
{
    internal class ParserWorker<T>  where T : class
    {
        private IParser<T> parser;
        private IParserSettings settings;
        private HtmlLoader loader;
        private bool isActive;

        #region properties

        public IParser<T> Parser;

        public IParserSettings Settings
        { 
            get 
            {
                return settings; 
            } 
            set
            {
                settings = value;
                loader = new HtmlLoader(value);
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }
        }

        #endregion

        public event Action<object, T> OnNewData; 
        public event Action<object> OnCompleted; 

        public ParserWorker(IParser<T> parser) 
        {
            this.parser = parser;
        }

        public ParserWorker(IParser<T> parser, IParserSettings settings) : this(parser)
        {
            this.settings = settings;
        }

        public void Start()
        {
            isActive = true;
            Worker();
        }

        public void Abort()
        {
            isActive = false;
        }

        private async void Worker()
        {
            for(int i = settings.StartPoint; i <= settings.EndPoint; i++)
            {
                if (!isActive)
                {
                    OnCompleted?.Invoke(this);
                    return;
                }

                var source = await loader.GetSourceByPageId(i);
                var domParser = new HtmlParser();

                var document = await domParser.ParseDocumentAsync(source);

                var result = parser.Parse(document);
                
                OnNewData?.Invoke(this, result);
            }

            OnCompleted?.Invoke(this);
        }


    }
}
