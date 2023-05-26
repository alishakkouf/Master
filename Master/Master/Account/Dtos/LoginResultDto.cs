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

        public string Role => IsAdmin ? "Admin" : "";

        public bool IsAdmin { get; set; }
    }
}
