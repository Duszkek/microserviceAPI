using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarsService.Database.Entity
{
    /// <summary>
    /// CREATE TABLE Exams (TechId int IDENTITY(1,1) PRIMARY KEY, Car_ID int, IsWorking bit, ActualDate datetime2, NextDate datetime2, FOREIGN KEY (Car_ID) REFERENCES dbo.Cars(Car_ID))
    /// </summary>

    [Table("Exams")]
    public class TechExam
    {
        #region Properties

        [Key]
        public int TechId { get; set; }

        [ForeignKey("Car")]
        public int Car_ID { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime NextDate { get; set; }
        public bool IsWorking { get; set; }

        #endregion

        #region Ctor

        public TechExam(int car_ID, DateTime actualDate, DateTime nextDate, bool isWorking)
        {
            TechId=0; //we wont use it anywhere so its okay to stay at 0
            Car_ID=car_ID;
            ActualDate=actualDate;
            NextDate=nextDate;
            IsWorking=isWorking;
        }

        #endregion
    }
}
