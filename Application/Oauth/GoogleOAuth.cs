using Application.DTOs;

namespace Application.Oauth
{
    public class GoogleOAuth
    {
        public bool IsSuccess { get; set; }
        public UserInfoDTO? UserInfo { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class GoogleUserInfo
    {
        /// <summary>
        /// The issuer of the token, usually the URL of the authorization server.
        /// </summary>
        public string Iss { get; set; }

        /// <summary>
        /// The authorized party that the token was issued to.
        /// </summary>
        public string Azp { get; set; }

        /// <summary>
        /// The audience that this ID token is intended for.
        /// </summary>
        public string Aud { get; set; }

        /// <summary>
        /// The unique identifier for the user.
        /// </summary>
        public string Sub { get; set; }

        /// <summary>
        /// The email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Whether the email address is verified or not.
        /// </summary>
        public bool EmailVerified { get; set; }

        /// <summary>
        /// Access token hash, used to verify the integrity of the token.
        /// </summary>
        public string AtHash { get; set; }

        /// <summary>
        /// The full name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The URL of the user's profile picture.
        /// </summary>
        public string Picture { get; set; }

        /// <summary>
        /// The given name of the user.
        /// </summary>
        public string GivenName { get; set; }

        /// <summary>
        /// The family name of the user.
        /// </summary>
        public string FamilyName { get; set; }

        /// <summary>
        /// The time the ID token was issued, represented as Unix time.
        /// </summary>
        public long Iat { get; set; }

        /// <summary>
        /// The expiration time of the ID token, represented as Unix time.
        /// </summary>
        public long Exp { get; set; }
    }


    public class AuthLogin
    {
        public string code { get; set; }
        public string redirect_url { get; set; }

        public string state { get; set; }
    }
}
