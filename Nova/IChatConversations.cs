namespace Nova
{
    /// <summary>
    /// Интерфейс для ведения диалога.
    /// </summary>
    internal interface IChatConversations
    {
        /// <summary>
        /// Метод получения ответа на пользовательский запрос.
        /// </summary>
        /// <param name="request">Текст пользовательского запроса.</param>
        /// <returns></returns>
        internal Task<string> Conversate(string request);
    }
}
