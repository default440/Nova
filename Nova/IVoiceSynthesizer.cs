namespace Nova
{
    /// <summary>
    /// Интерфейс для синтезатора речи.
    /// </summary>
    internal interface IVoiceSynthesizer
    {
        /// <summary>
        /// Метод преобразования текста в речь.
        /// </summary>
        /// <param name="textToSpeak">Текст для преобразования.</param>
        internal Task Speak(string textToSpeak);
    }
}
