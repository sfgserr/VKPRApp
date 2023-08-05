using VKPRApp.Builders.ApiRequestBuilders.Exceptions;

namespace VKPRApp.Builders.ApiRequestBuilders
{
    public class ApiRequestBuilder : IApiRequestBuilder
    {
        private string _url;

        private readonly string _baseUrl = "https://api.vk.com";

        public ApiRequestBuilder()
        {
            _url = _baseUrl;
        }

        public int Count { get; private set; }
        public bool IsMethodAdded { get; private set; }

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
            if (IsMethodAdded)
                throw new MethodAlreadyAddedException();

            _url = $"{_url}/method/{methodName}";

            IsMethodAdded = true;

            return this;
        }

        public string Build()
        {
            string url = _url;

            _url = _baseUrl;
            Count = 0;
            IsMethodAdded = false;

            return url;
        }
    }
}
