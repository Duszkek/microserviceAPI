using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//CREATE TABLE Cars (Car_ID int IDENTITY(1,1) PRIMARY KEY, RegNum varchar(255), Brand varchar(255), Model varchar(255))

namespace CarsService.Database.Entity
{
    [Table("Cars")]
    public class Car
    {
        [Key]
        public int CarID { get; set; }
        public string RegNum { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Car(string regNum, string brand, string model)
        {
            CarID = 0; //we don't use it like that so its okay. I could also just never set it up
            RegNum=regNum;
            Brand=brand;
            Model=model;
        }
    }
}
