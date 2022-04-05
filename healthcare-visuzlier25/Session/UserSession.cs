using healthcare_visuzlier25.DTO;

namespace healthcare_visuzlier25.Session
{
    public record UserSession
    {
        public long ExpiryTime { get; init; }
        public UserType UserType { get; init; }
        public string UserName { get; init; }

        public string Email { get; init; }

        public int UserUUID { get; init; }

        public TenantDTO[] Tenants { get; init; }



        public UserSession(long expiryTime, string email, string userName, int userGuid, TenantDTO[] tenants, UserType userType = UserType.NormalUser)
        {
            UserType = userType;
            ExpiryTime = expiryTime;
            UserName = userName;
            Email = email;
            UserUUID = userGuid;
            Tenants = tenants;
        }
        public bool UserBelongsToTennat(int tenantId)
        {
            return Tenants.Select(tenant => tenant.Id == tenantId).Any();
        }
    }
    public enum UserType
    {
        NormalUser,
        Admin
    }
}
