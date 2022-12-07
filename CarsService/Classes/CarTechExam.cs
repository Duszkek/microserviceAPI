namespace CarsService.Classes
{
    public class CarTechExam
    {
        #region Properties

        public string? RegNum { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime NextDate { get; set; }
        public bool IsWorking { get; set; }

        #endregion

        #region Methods

        public bool HasValue()
        {
            if (RegNum == null || Brand == null || Model == null)
            {
                return false;
            }

            return true;
        }

        #endregion
    }
}
