namespace healthcare_visuzlier25.DTO
{
    public class CreateDatabaseUserRequest
    {
        public int? TenantId { get; set; }
        public string Department { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }

    }
}
