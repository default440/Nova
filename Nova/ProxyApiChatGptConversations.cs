using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;

namespace Nova
{
    /// <summary>
    /// Адаптер для ведения диалога на основе запросов к https://proxyapi.ru/.
    /// </summary>
    /// <remarks>
    /// Для работы необходимо добавить переменную среды ProxyAPI_API_KEY и положить в ней API_KEY от https://proxyapi.ru/.
    /// </remarks>
    internal class ProxyApiChatGptConversations : IChatConversations
    {
        private readonly string _proxyApiUrl = "https://api.proxyapi.ru/openai/v1/chat/completions";

        private Conversation _chat;

        /// <summary>
        /// ctor.
        /// </summary>
        internal ProxyApiChatGptConversations()
        {
            var apiKey = Environment.GetEnvironmentVariable("ProxyAPI_API_KEY") ?? string.Empty;

            OpenAIAPI api = new OpenAIAPI(apiKey);
            api.ApiUrlFormat = _proxyApiUrl;

            _chat = api.Chat.CreateConversation();
            _chat.Model = Model.ChatGPTTurbo_16k;
        }

        ///<inheritdoc/>
        async Task<string> IChatConversations.Conversate(string request)
        {
            _chat.AppendUserInput(request);

            // TODO: Можно заменить на потоковое получение.
            return await _chat.GetResponseFromChatbotAsync();
        }
    }
}
