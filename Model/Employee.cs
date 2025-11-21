namespace TestProject.Model
{
    public class Employee
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age  { get; set; }

        public DateTime CreateAt { get; set; }= DateTime.Now;

    }
}
