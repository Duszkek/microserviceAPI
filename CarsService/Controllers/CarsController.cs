using CarsService.Classes;
using CarsService.Database;
using CarsService.Database.Entity;
using CarsService.Database.Views;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        DatabaseContext db;

        public CarsController()
        {
            db = new DatabaseContext();
        }

        // GET: api/<CarsController>
        [HttpGet()]
        public IEnumerable<CarDetailsView> GetCarByBrand(string? searching, int bywhat)
        {
            if (searching == null)
            {
                return db.CarDetailsView.ToList();
            }
            switch (bywhat)
            {
                case 0:
                        return db.CarDetailsView.Where(x => x.RegNum == searching).ToList();
                case 1:
                        return db.CarDetailsView.Where(x => x.Model == searching).ToList();
                case 2:
                        return db.CarDetailsView.Where(x => x.Brand == searching).ToList();
                default:
                        return db.CarDetailsView.ToList();

            }
           
        }
        //[HttpGet()]
        //public IEnumerable<CarDetailsView> GetCar()
        //{
        //    return db.CarDetailsView.Where(x => x.Brand == "City").ToList();
        //}

        [Route("api/[controller]/Create")]
        // POST api/<CarsController>
        [HttpPost]
        public IActionResult Post([FromBody] CarTechExam entity)
        {
            try
            {
                if (db.Cars.Any(o => o.RegNum == entity.RegNum))
                {
                    return StatusCode(StatusCodes.Status304NotModified);// checks if this RegNumber is already in database
                }
                var newCar = new Car(entity.RegNum, entity.Brand, entity.Model);
                db.Cars.Add(newCar);
                db.SaveChanges();
                var check = db.Cars.FirstOrDefault(acc => acc.RegNum == newCar.RegNum);
                var newExam = new TechExam(check.CarID, entity.ActualDate, entity.NextDate, entity.IsWorking);
                db.Exams.Add(newExam);
                db.SaveChanges();
                return StatusCode(StatusCodes.Status201Created, newCar);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }
        [Route("api/[controller]/Update")]
        // PUT api/<CarsController>/5
        [HttpPost]
        public void Update([FromBody] CarTechExam entity)
        {
            if (db.Cars.Any(o => o.RegNum == entity.RegNum))
            {
                var car = db.Cars.FirstOrDefault(acc => acc.RegNum == entity.RegNum);
                var carExam = db.Exams.FirstOrDefault(acc => acc.CarID == car.CarID);
                car.CarID = car.CarID;
                car.Brand = entity.Brand;
                car.Model = entity.Model;
                car.RegNum = entity.RegNum;
                carExam.TechId = carExam.TechId;
                carExam.CarID = car.CarID;
                carExam.IsWorking = entity.IsWorking;
                carExam.ActualDate = entity.ActualDate;
                carExam.NextDate = entity.NextDate;
                db.Cars.Update(car);
                db.Exams.Update(carExam);
                db.SaveChanges();

            }
            else
            {
                return;
            }
            
        }
        /*
        // DELETE api/<CarsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
