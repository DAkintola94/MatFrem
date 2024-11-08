namespace MatFrem.Models.ViewModel
{
    public class EditViewTable
    {
        public Guid LocationReportID { get; set; } = new Guid();
        public string GeoJson { get; set; }
        public string LocationMessage { get; set; }
        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    }
}
