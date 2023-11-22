namespace CondominiumProject.Models
{
    public class VisitDetail
    {
        public string? DNI { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? VehicleMarc { get; set; }
        public string? VehicleModel { get; set; }
        public string? VehicleColor { get; set; }
        public string? VehiclePlate { get; set; }
        public int IDHabitation { get; set; }
        public DateTime VisitDate { get; set; }
        public TimeSpan VisitTime { get; set; }
        public int IDProject { get; set; }
    }
}
