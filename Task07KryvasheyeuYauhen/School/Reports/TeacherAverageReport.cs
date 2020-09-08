namespace School.Reports
{
    /// <summary>
    /// report of average score in each teacher
    /// </summary>
    public class TeacherAverageReport
    {
        public int SessionId { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public double Average { get; set; }

    }
}
