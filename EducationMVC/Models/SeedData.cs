using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MVCEdu.Data;
using MVCEdu.Models;
using Microsoft.EntityFrameworkCore.Internal;

namespace MVCEdu.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MVCEduContext(serviceProvider.GetRequiredService<DbContextOptions<MVCEduContext>>()))
            {
                if (context.Course.Any() || context.Student.Any() || context.Teacher.Any()) 
                {
                    return;
                }
                context.Student.AddRange(
                    new Student { /*Id = 1,*/ StudentId = "89", FirstName = "Филип",    LastName = "Панчевски",   EnrollmentDate = DateTime.Parse("2017-8-1"), AcquiredCredits = 200, CurrentSemester = 6, EducationLevel = "Додипломски", ProfilePicture = "" },
                    new Student { /*Id = 2,*/ StudentId = "00", FirstName = "Венко",      LastName = "Филипче",   EnrollmentDate = DateTime.Parse("2017-2-1"), AcquiredCredits = 105, CurrentSemester = 4, EducationLevel = "Додипломски", ProfilePicture = "" },
                    new Student { /*Id = 3,*/ StudentId = "42", FirstName = "Вилијам",      LastName = "Шејкспир",      EnrollmentDate = DateTime.Parse("1590-12-9"), AcquiredCredits = 365, CurrentSemester = 22, EducationLevel = "Доктор на книги", ProfilePicture = "" },
                    new Student { /*Id = 1,*/ StudentId = "247", FirstName = "Милан",       LastName = "Јуве",  EnrollmentDate = DateTime.Parse("2007-2-4"), AcquiredCredits = 63,  CurrentSemester = 5, EducationLevel = "Серија А", ProfilePicture = "" },
                    new Student { /*Id = 1,*/ StudentId = "222", FirstName = "Сунчица",    LastName = "Кала",      EnrollmentDate = DateTime.Parse("2017-2-5"), AcquiredCredits = 75,  CurrentSemester = 4, EducationLevel = "Додипломски", ProfilePicture = "" },
                    new Student { /*Id = 1,*/ StudentId = "333", FirstName = "Андреј", LastName = "Попоф", EnrollmentDate = DateTime.Parse("2019-8-11"), AcquiredCredits = 199, CurrentSemester = 6, EducationLevel = "Додипломски", ProfilePicture = "" },
                    new Student { /*Id = 1,*/ StudentId = "444", FirstName = "Хотел?", LastName = "Триваго", EnrollmentDate = DateTime.Parse("2000-1-1"), AcquiredCredits = 0, CurrentSemester = 7, EducationLevel = "Почетник", ProfilePicture = "" }
                    );
                context.SaveChanges();

                context.Teacher.AddRange(
                    new Teacher { /*Id = 1,*/ FirstName = "Анета",      LastName = "Бучковска",    AcademicRank = "Професор",     Degree = "PhD", HireDate = DateTime.Parse("2015-1-1"), OfficeNumber = "1001", ProfilePicture = "aneta.jpeg" },
                    new Teacher { /*Id = 2,*/ FirstName = "Биба",      LastName = "Начевска",   AcademicRank = "Професор",        Degree = "PhD", HireDate = DateTime.Parse("2015-11-2"), OfficeNumber = "1002" },
                    new Teacher { /*Id = 3,*/ FirstName = "Сања",  LastName = "Атанасовска",      AcademicRank = "Професор",     Degree = "PhD", HireDate = DateTime.Parse("2015-1-23"), OfficeNumber = "1003" },
                    new Teacher { /*Id = 4,*/ FirstName = "Ивана",    LastName = "Сандева",   AcademicRank = "Професор",     Degree = "PhD", HireDate = DateTime.Parse("2015-1-24"), OfficeNumber = "1004" },
                    new Teacher { /*Id = 5,*/ FirstName = "Маргарита",     LastName = "Гиновска",     AcademicRank = "Професор",     Degree = "PhD", HireDate = DateTime.Parse("2015-2-5"), OfficeNumber = "1005" },
                    new Teacher { /*Id = 6,*/ FirstName = "Перо",          LastName = "Латкоски",    AcademicRank = "Професор",             Degree = "PhD",   HireDate = DateTime.Parse("2015-1-6"), OfficeNumber = "1006" },
                    new Teacher { /*Id = 7,*/ FirstName = "Даниел",     LastName = "Денковски",        AcademicRank = "Професор",        Degree = "PhD",  HireDate = DateTime.Parse("2015-8-7"), OfficeNumber = "1007" },
                    new Teacher { /*Id = 8,*/ FirstName = "Борислав",   LastName = "Поповски",      AcademicRank = "Професор",        Degree = "PhD", HireDate = DateTime.Parse("2017-3-8"), OfficeNumber = "1008", ProfilePicture = "" },
                    new Teacher { /*Id = 9,*/ FirstName = "Владимир", LastName = "Атанасовски", AcademicRank = "Професор", Degree = "PhD", HireDate = DateTime.Parse("2017-3-8"), OfficeNumber = "1009", ProfilePicture = "vladimir.jpg" },
                    new Teacher { /*Id = 10,*/ FirstName = "Соња", LastName = "Зајкова", AcademicRank = "Професор", Degree = "PhD", HireDate = DateTime.Parse("2017-3-8"), OfficeNumber = "1010", ProfilePicture = "zajkova.jpg" }
                    );
                context.SaveChanges();

                context.Course.AddRange(
                    new Course { /*Id = 1,*/ Title = "Математика 1",          Credits = 7, Semester = 1, Programme = "Сите",               EducationLevel = "8", FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Анета" && d.LastName == "Бучковска").Id,    SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Сања" && d.LastName == "Атанасовска").Id },
                    new Course { /*Id = 2,*/ Title = "Физика 1",       Credits = 7, Semester = 1, Programme = "Сите",               EducationLevel = "7", FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Маргарита" && d.LastName == "Гиновска").Id,  SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Ивана" && d.LastName == "Сандева").Id },
                    new Course { /*Id = 3,*/ Title = "Програмирање и Алгоритми",        Credits = 5, Semester = 2, Programme = "КТИ",  EducationLevel = "9", FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Даниел" && d.LastName == "Денковски").Id,    SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Владимир" && d.LastName == "Атанасовски").Id },
                    new Course { /*Id = 4,*/ Title = "Математика 3",       Credits = 5, Semester = 3, Programme = "КТИ",  EducationLevel = "8", FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Биба" && d.LastName == "Начевска").Id,       SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Биба" && d.LastName == "Начевска").Id },
                    new Course { /*Id = 5,*/ Title = "Оптички мрежи",   Credits = 7, Semester = 4, Programme = "ТКИИ",          EducationLevel = "2", FirstTeacherId = context.Teacher.Single(d => d.FirstName == "Борислав" && d.LastName == "Поповски").Id,   SecondTeacherId = context.Teacher.Single(d => d.FirstName == "Перо" && d.LastName == "Латкоски").Id }
                    );
                context.SaveChanges();

                context.Enrollment.AddRange(
                    new Enrollment { /*Id = 1,*/ 
                        StudentId = context.Student.Single(d => d.StudentId == "89").Id,
                        CourseId = context.Course.Single(d => d.Title == "Математика 1").Id,
                        Semester = "1", 
                        Year = 2017, 
                        Grade = 5, 
                        SeminalUrl = "www.google.com", 
                        ProjectUrl = "www.google.com", 
                        ExamPoints = 40, 
                        SeminalPoints = 20, 
                        ProjectPoints = 10, 
                        AdditionalPoints = 5, 
                        FinishDate = DateTime.Parse("2020-3-1") 
                    }
                    //new Enrollment { /*Id = 2,*/ CourseId = 2, StudentId = 102, Semester = "2", Year = 2017, Grade = 4, SeminalUrl = "www.google.com", ProjectUrl = "www.google.com", ExamPoints = 50, SeminalPoints = 10, ProjectPoints = 15, AdditionalPoints = 2, FinishDate = DateTime.Parse("2017-3-2") },
                    //new Enrollment { /*Id = 3,*/ CourseId = 3, StudentId = 103, Semester = "3", Year = 2017, Grade = 4, SeminalUrl = "www.google.com", ProjectUrl = "www.google.com", ExamPoints = 30, SeminalPoints = 20, ProjectPoints = 15, AdditionalPoints = 3, FinishDate = DateTime.Parse("2017-3-3") }
                );
                context.SaveChanges();

                context.User.AddRange(
                    new User
                    {
                        /*Id = 1,*/
                        Username = "myadmin@dotnet.net",
                        Password = "iadmin",
                        Role = Role.Admin
                    },
                    new User
                    {
                        /*Id = 2,*/
                        Username = "profesorot@dotnet.net",
                        Password = "iteach",
                        Role = Role.Teacher,
                        TeacherId = context.Teacher.Single(d => d.FirstName == "Перо" && d.LastName == "Латкоски").Id
                    },
                    new User
                    {
                        /*Id = 3,*/
                        Username = "studentot@dotnet.net",
                        Password = "istudy",
                        Role = Role.Student,
                        StudentId = context.Student.Single(d => d.FirstName == "Филип" && d.LastName == "Панчевски").Id
                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
