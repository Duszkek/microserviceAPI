using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarsService.Database.Entity
{
    /// <summary>
    /// CREATE TABLE Exams (TechId int IDENTITY(1,1) PRIMARY KEY, Car_ID int, IsWorking bit, ActualDate datetime2, NextDate datetime2, FOREIGN KEY (Car_ID) REFERENCES dbo.Cars(Car_ID))
    /// </summary>
    public class TechExam
    {
        [Key]
        public int TechId { get; set; }
        [ForeignKey("Car")]
        public int CarID { get; set; }
        public DateTime ActualDate { get; set; }
        public DateTime NextDate { get; set; }
        public bool IsWorking { get; set; }
        public TechExam(int carID, DateTime actualDate, DateTime nextDate, bool isWorking)
        {
            TechId=0; //again we wont use it anywhere so its okay to stay at 0
            CarID=carID;
            ActualDate=actualDate;
            NextDate=nextDate;
            IsWorking=isWorking;
        }
    }
}
