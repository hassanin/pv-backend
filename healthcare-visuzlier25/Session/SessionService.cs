using healthcare_visuzlier25.Crypto;
using healthcare_visuzlier25.Models;
using Newtonsoft.Json;

namespace healthcare_visuzlier25.Session
{
    public class SessionService : ISessionService
    {
        private ILogger<SessionService> logger;
        private ICryptoProvider cryptoProvider;
        public SessionService(ILogger<SessionService> logger, ICryptoProvider cryptoProvider)
        {
            this.logger = logger;
            this.cryptoProvider = cryptoProvider;
        }


        public string EncryptedSessionForPerson(Person person, AuxillaryUserSessionInfo sessionInfo)
        {
            var session = SessionForPerson(person, sessionInfo);
            // serialize the session to a string
            var session_string = JsonConvert.SerializeObject(session);
            var encoded_string = cryptoProvider.Base64Encode(session_string);
            return encoded_string;
        }

        //TODO: change the interface so that it takes the tenant IDs 
        public UserSession SessionForPerson(Person person, AuxillaryUserSessionInfo sessionInfo)
        {
            var expiryTime = Util.Time.GetEpochTime() + ISessionService.SESSION_TIME_SECONDS;
            var userSession = new UserSession(expiryTime, person.Email, person.Name, person.Id, sessionInfo.Tenants);
            return userSession;
        }

        /// <summary>
        /// Potentially throws exception
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public UserSession DecryptSessionForPerson(string token)
        {
            var session = cryptoProvider.Base64Decode(token);
            return JsonConvert.DeserializeObject<UserSession>(session);
        }
    }
}
