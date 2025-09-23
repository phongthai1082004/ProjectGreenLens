namespace ProjectGreenLens.Exceptions
{
    public interface GeminiErrorMessages
    {
        public const string CONTENT_REQUIRED = "Nội dung câu hỏi là bắt buộc";
        public const string CONTENT_TOO_LONG = "Nội dung không được vượt quá 2000 ký tự";
        public const string USER_NOT_FOUND = "Không tìm thấy người dùng";
        public const string USER_PLANT_NOT_FOUND = "Không tìm thấy cây của người dùng";
        public const string AI_API_ERROR = "Lỗi xảy ra khi xử lý yêu cầu với AI API";
        public const string UNAUTHORIZED = "Người dùng không có quyền thực hiện hành động này";
        public const string INVALID_USER_PLANT = "Cây này không thuộc về người dùng";
        public const string INVALID_TOKEN = "Token không hợp lệ";
    }
}
