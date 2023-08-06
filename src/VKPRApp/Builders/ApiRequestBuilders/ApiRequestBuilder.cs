using VKPRApp.Builders.ApiRequestBuilders.Exceptions;

namespace VKPRApp.Builders.ApiRequestBuilders
{
    public class ApiRequestBuilder : IApiRequestBuilder
    {
        private string _url = string.Empty;

        public int Count { get; private set; }
        public bool IsMethodAdded { get; private set; }
        public bool IsBaseAdded { get; private set; }

        public IApiRequestBuilder AddBase(string baseUrl)
        {
            if (IsBaseAdded)
                throw new BaseAlreadyAddedException();

            _url += baseUrl;
            IsBaseAdded = true;

            return this;
        }

        public IApiRequestBuilder AddAttribute(string attributeName, string value)
        {
            if (!IsMethodAdded)
                throw new MethodIsNotAddedException();

            _url = Count == 0 ? $"{_url}?{attributeName}={value}" :
                                $"{_url}&{attributeName}={value}";
            Count++;

            return this;
        }

        public IApiRequestBuilder AddMethod(string methodName)
        {
            if (!IsBaseAdded)
                throw new BaseIsNotAddedException();

            if (IsMethodAdded)
                throw new MethodAlreadyAddedException();

            _url = $"{_url}/{methodName}";

            IsMethodAdded = true;

            return this;
        }

        public string Build()
        {
            string url = _url;

            _url = string.Empty;

            Count = 0;
            IsMethodAdded = false;
            IsBaseAdded = false;

            return url;
        }
    }
}
