using healthcare_visuzlier25.Models;

namespace healthcare_visuzlier25.Session
{
    public interface ISessionService
    {
        public static int SESSION_TIME_SECONDS = 60 * 60;// 1 Hour
        public UserSession SessionForPerson(Person person, AuxillaryUserSessionInfo sessionInfo);
        public string EncryptedSessionForPerson(Person person, AuxillaryUserSessionInfo sessionInfo);

        public UserSession DecryptSessionForPerson(string token);
    }
}
