using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
//create view CarDetailsView as select RegNum, Brand, Model, ActualDate, NextDate, IsWorking from Cars as C inner join Exams as E on C.Car_ID = E.Car_ID

namespace CarsService.Migrations
{
    public partial class CreateCarDetailsView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.Sql(@"create view CarDetailsView as select RegNum, 
                Brand, Model, ActualDate, NextDate, IsWorking from [dbo].[Cars] as C inner join 
                [dbo].[Exams] as E on C.CarID = E.CarID");
        }

    protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
