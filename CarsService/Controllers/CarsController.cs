using CarsService.Classes;
using CarsService.Database;
using CarsService.Database.Entity;
using CarsService.Database.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase 
    {
        #region Members

        private readonly DatabaseContext db;

        #endregion

        #region Ctor

        public CarsController()
        {
            db = new DatabaseContext();
        }

        #endregion

        #region GET

        #region Log in

        [Route("[controller]/LogIn")]
        [HttpGet()]
        public IActionResult LogIn(string? login, string? password)
        {
            if (db.Users != null && login != null && password != null && db.Users.Any(o => o.Login == login))
            {
                string? hashedPassword = db.Users.FirstOrDefault(o => o.Login == login).Password;
                User loginUser = new User(login, password);

                if (hashedPassword != null && loginUser.CheckSalt(password, hashedPassword))
                {
                    return StatusCode(StatusCodes.Status200OK);
                }

            }
            return StatusCode(StatusCodes.Status401Unauthorized);
        }

        #endregion

        [HttpGet()]
        public IEnumerable<CarDetailsView> GetCarByBrand(string? searching, int bywhat)
        {
            if (db.CarDetailsView == null)
            {
                throw new ArgumentNullException(nameof(db.CarDetailsView));
            }

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

        #endregion

        #region POST

        #region REGISTER

        [Route("[controller]/Register")]
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            if (db.Users != null)
            {
                if (user != null)
                {
                    if (db.Users.Any(o => o.Login == user.Login) && user.Password != null)
                    {
                        return StatusCode(StatusCodes.Status304NotModified);// checks if this Login is already in database and if password is null
                    }

                    var newUser = new User(user.Login, user.Password);

                    var createdHash = newUser.CreateSalt(newUser.Password);

                    if (createdHash == "")
                    {
                        return StatusCode(StatusCodes.Status304NotModified);
                    }

                    newUser.Password = createdHash;

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    return StatusCode(StatusCodes.Status201Created, user);
                }
                else
                {
                    ArgumentNullException e = new ArgumentNullException(nameof(user));
                    Debug.WriteLine(e);
                }
            }
            else
            {
                ArgumentNullException e = new ArgumentNullException(nameof(db.Users));
                Debug.WriteLine(e);
            }
            return StatusCode(StatusCodes.Status304NotModified);
        }

        #endregion

        #region CREATE

        [Route("[controller]/Create")]
        // POST api/<CarsController>
        [HttpPost]
        public IActionResult Post([FromBody] CarTechExam entity)
        {
            if (db.Cars != null)
            {
                if (db.Exams != null)
                {
                    if (entity.HasValue())
                    {
                        try
                        {
                            if (db.Cars.Any(o => o.RegNum == entity.RegNum))
                            {
                                return StatusCode(StatusCodes.Status304NotModified);// checks if this RegNumber is already in database
                            }

                            Car newCar = new Car(entity.RegNum, entity.Brand, entity.Model);

                            db.Cars.Add(newCar);
                            db.SaveChanges();

                            Car? check = db.Cars.FirstOrDefault(acc => acc.RegNum == newCar.RegNum) ?? null;

                            if (check == null)
                            {
                                throw new ArgumentNullException(nameof(check));
                            }

                            TechExam newExam = new TechExam(check.Car_ID, entity.ActualDate, entity.NextDate, entity.IsWorking);

                            db.Exams.Add(newExam);
                            db.SaveChanges();

                            return StatusCode(StatusCodes.Status201Created, newCar);
                        }
                        catch (Exception ex)
                        {
                            return StatusCode(StatusCodes.Status500InternalServerError, ex);
                        }
                    }
                    else
                    {
                        ArgumentNullException e = new ArgumentNullException(nameof(entity));
                        Debug.WriteLine(e);
                    }
                }
                else
                {
                    ArgumentNullException e = new ArgumentNullException(nameof(db.Exams));
                    Debug.WriteLine(e);
                }
            }
            else
            {
                ArgumentNullException e = new ArgumentNullException(nameof(db.Cars));
                Debug.WriteLine(e);
            } 
            return StatusCode(StatusCodes.Status304NotModified);
        }

        #endregion

        #region UPDATE

        [Route("[controller]/Update")]
        // PUT api/<CarsController>/5
        [HttpPost]
        public void Update([FromBody] CarTechExam entity)
        {
            if(db.Cars == null || db.Exams == null)
            {
                return;
            }
            if (db.Cars.Any(o => o.RegNum == entity.RegNum))
            {
                Car? car = db.Cars.FirstOrDefault(acc => acc.RegNum == entity.RegNum) ?? null;
                
                if (car != null)
                {
                    TechExam? carExam = db.Exams.FirstOrDefault(acc => acc.Car_ID == car.Car_ID) ?? null;

                    if (carExam != null)
                    {
                        Car newCar = new Car(entity.RegNum, entity.Brand, entity.Model, car.Car_ID);
                        TechExam newCarExam = new TechExam(carExam.TechId, entity.ActualDate, entity.NextDate, entity.IsWorking);
                        db.Cars.Update(car);
                        db.Exams.Update(carExam);
                        db.SaveChanges();
                    }
                }
            }
            return;
        }

        #endregion

        #endregion
    }
}
