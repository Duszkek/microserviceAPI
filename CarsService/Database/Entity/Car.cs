using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//CREATE TABLE Cars (Car_ID int IDENTITY(1,1) PRIMARY KEY, RegNum varchar(255), Brand varchar(255), Model varchar(255))

namespace CarsService.Database.Entity
{
    [Table("Cars")]
    public class Car
    {
        #region Properties

        [Key]
        public int Car_ID { get; private set; }
        public string? RegNum { get; private set; }
        public string? Brand { get; private set; }
        public string? Model { get; private set; }

        #endregion

        #region Ctor

        public Car(string? regNum, string? brand, string? model, int car_ID = 0)
        {
            Car_ID = car_ID; //we don't use it like that so its okay. I could also just never set it up
            RegNum=regNum;
            Brand=brand;
            Model=model;
        }

        #endregion
    }
}
