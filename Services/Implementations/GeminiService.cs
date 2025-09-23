using Microsoft.Extensions.Options;
using Mscc.GenerativeAI;
using ProjectGreenLens.Exceptions;
using ProjectGreenLens.Services.Interfaces;
using ProjectGreenLens.Settings;

namespace ProjectGreenLens.Services.Implementations
{
    public class GeminiService : IGeminiService
    {
        private readonly GoogleAI _googleAI;
        private readonly GoogleAISettings _settings;

        public GeminiService(IOptions<GoogleAISettings> settings)
        {
            _settings = settings.Value;
            _googleAI = new GoogleAI(_settings.ApiKey);
        }
        public async Task<string> GetPlantAdviceAsync(string prompt, string plantInfo)
        {
            try
            {
                var model = _googleAI.GenerativeModel(_settings.Model);

                var fullPrompt = $@"
                    Bạn là một chuyên gia chăm sóc cây cảnh có nhiều năm kinh nghiệm.
                    Hãy trả lời câu hỏi sau về cây của người dùng một cách chi tiết và hữu ích:
                    
                    Thông tin về cây:
                    {plantInfo}
                    
                    Câu hỏi của người dùng:
                    {prompt}
                    
                    Vui lòng đưa ra lời khuyên cụ thể,nhưng phải thật ngắn gọn dưới 2000 token, dễ hiểu và có thể áp dụng được.
                    Trả lời bằng tiếng Việt.
                ";

                var response = await model.GenerateContent(fullPrompt);
                return response.Text ?? throw new InvalidOperationException(GeminiErrorMessages.AI_API_ERROR);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(GeminiErrorMessages.AI_API_ERROR, ex);
            }
        }
    }
}
