namespace UserApiMicroservice.Dtos
{
    public class UserUpdateDto
    {
        public int Id { get; set; }
        public string Nickname { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
    }
}
