namespace ProjectGreenLens.Services.Interfaces
{
    public interface IGeminiService
    {
        Task<string> GetPlantAdviceAsync(string prompt, string plantInfo);
    }
}
