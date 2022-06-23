using Auth.Database.Models;
using JWT;
using JWT.Algorithms;
using JWT.Exceptions;
using JWT.Serializers;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Auth.Database
{
    public class JWTHandler
    {
        public static string Secret { get; set; }

        public static async Task<string?> GenerateJWTAsync(string userId)
        {
            AuthDbContext context = new ();
            User? destUser = await context.Users.SingleOrDefaultAsync(x=> x.Userid == userId);
            if (destUser == null) return null;
            
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            return encoder.Encode(destUser, Secret);
        }

        public static User? DecodeJWT(string jwt)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtAlgorithm algorithm = new HMACSHA256Algorithm(); // symmetric
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder, algorithm);

                string json = decoder.Decode(jwt, Secret, verify: true);
                return JsonConvert.DeserializeObject<User>(json);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
