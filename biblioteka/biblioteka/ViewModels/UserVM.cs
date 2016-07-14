namespace biblioteka.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PESEL { get; set; }
        public int BooksBorrowedCount { get; set; }
    }
}