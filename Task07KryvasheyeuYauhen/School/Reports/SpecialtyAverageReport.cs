namespace School.Reports
{
    /// <summary>
    /// report of average score in each specialty
    /// </summary>
    public class SpecialtyAverageReport
    {
        public int SessionId { get; set; }

        public string Specialty { get; set; }

        public double Average { get; set; }
    }
}
