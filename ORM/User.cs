namespace ORM
{
    public class User
    {
        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string AccountImage { get; set; }

        public string PersonalInformation { get; set; }

        public virtual Role Role { get; set; }

        public int RoleId { get; set; }
    }
}
