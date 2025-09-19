namespace ProjectGreenLens.Exceptions
{
    public static class ValidationMessages
    {
        // Username
        public const string RequiredUsername = "Tên người dùng không được để trống";
        public const string UsernameMaxLength = "Tên người dùng không được vượt quá 50 ký tự";

        // Email
        public const string RequiredEmail = "Email không được để trống";
        public const string InvalidEmail = "Email không đúng định dạng";
        public const string EmailMaxLength = "Email không được vượt quá 100 ký tự";

        // Password
        public const string RequiredPassword = "Mật khẩu không được để trống";
        public const string PasswordMinLength = "Mật khẩu phải có ít nhất 8 ký tự";
        public const string PasswordRequireLower = "Mật khẩu phải chứa ít nhất một chữ cái viết thường";
        public const string PasswordRequireUpper = "Mật khẩu phải chứa ít nhất một chữ cái viết hoa";
        public const string PasswordRequireDigit = "Mật khẩu phải chứa ít nhất một chữ số";
        public const string PasswordRequireSpecial = "Mật khẩu phải chứa ít nhất một ký tự đặc biệt";

        // Refresh Token
        public const string RequiredRefreshToken = "Phải có RefreshToken!";
    }
}
