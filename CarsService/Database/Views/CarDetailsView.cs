namespace CarsService.Database.Views
{
    public class CarDetailsView
    {
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime NextDate { get; set; }
        public bool IsWorking { get; set; }

    }
}
