
namespace VKPRApp.Builders.ApiRequestBuilders
{
    public interface IApiRequestBuilder : IBuilder<string>
    {
        int Count { get; }

        bool IsMethodAdded { get; }

        bool IsBaseAdded { get; }

        IApiRequestBuilder AddBase(string baseUrl);

        IApiRequestBuilder AddAttribute(string attributeName, string value);

        IApiRequestBuilder AddMethod(string methodName);
    }
}
