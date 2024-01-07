using NAudio.Wave;
using OpenAI_API;
using static OpenAI_API.Audio.TextToSpeechRequest;

namespace Nova
{
    /// <summary>
    /// Синтезатор на основе запросов к https://proxyapi.ru/.
    /// </summary>
    /// <remarks>
    /// Для работы необходимо добавить переменную среды ProxyAPI_API_KEY и положить в ней API_KEY от https://proxyapi.ru/.
    /// </remarks>
    internal class ProxyApiSynthesizer : ISynthesizer
    {
        private readonly string _proxyApiUrl = "https://api.proxyapi.ru/openai/v1/audio/speech";

        private string _apiKey = string.Empty;

        /// <summary>
        /// ctor.
        /// </summary>
        internal ProxyApiSynthesizer() =>
            _apiKey = Environment.GetEnvironmentVariable("ProxyAPI_API_KEY") ?? string.Empty;

        ///<inheritdoc/>
        async Task ISynthesizer.Speak(string textToSpeak)
        {
            OpenAIAPI api = new OpenAIAPI(_apiKey);
            api.ApiUrlFormat = _proxyApiUrl;

            using (Stream result = await api.TextToSpeech.GetSpeechAsStreamAsync(textToSpeak, Voices.Nova))
            {
                using (WaveStream blockAlignedStream = new BlockAlignReductionStream(WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(result))))
                {
                    using (WasapiOut waveOut = new WasapiOut())
                    {
                        waveOut.Init(blockAlignedStream);
                        waveOut.Play();
                        while (waveOut.PlaybackState == PlaybackState.Playing)
                        {
                            Thread.Sleep(100);
                        }
                    }
                }
            }
        }
    }
}
