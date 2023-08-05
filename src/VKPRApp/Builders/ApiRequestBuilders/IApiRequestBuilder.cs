
namespace VKPRApp.Builders.ApiRequestBuilders
{
    public interface IApiRequestBuilder : IBuilder<string>
    {
        int Count { get; }

        bool IsMethodAdded { get; }

        IApiRequestBuilder AddAttribute(string attributeName, string value);

        IApiRequestBuilder AddMethod(string methodName);
    }
}
