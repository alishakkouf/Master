namespace Master.Account.Dtos
{
    public class LoginResultDto
    {
        /// <summary>
        /// Token expiration in seconds
        /// </summary>
        public int ExpiresIn { get; set; }

        /// <summary>
        /// The bearer access token (expiration equals ExpiresIn)
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The bearer refresh token, which can be used only once before it expires (another expiration, not ExpiresIn)
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
