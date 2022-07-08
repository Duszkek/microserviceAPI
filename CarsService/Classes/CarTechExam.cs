namespace CarsService.Classes
{
    public class CarTechExam
    {
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime NextDate { get; set; }
        public bool IsWorking { get; set; }
    }
}
