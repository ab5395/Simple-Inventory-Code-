using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityCodeFirstMVCSolution.Models
{
    public class Student
    {
        public Student()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StudentId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [Column(TypeName = "int")]
        public int StandardId { get; set; }
        [Column(TypeName = "int")]
        public int TeacherId { get; set; }
        [Column(TypeName = "int")]
        public int CityId { get; set; }

        [ForeignKey("StandardId")]
        public Standard Standard { get; set; }

        [ForeignKey("TeacherId")]
        public Teacher Teacher { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }
    }

    public class Standard
    {
        public Standard()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StandardId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string StandardName { get; set; }

        public ICollection<Student> Students { get; set; }

    }

    public class Teacher
    {
        public Teacher()
        {

        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeacherId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string TeacherName { get; set; }

        public ICollection<Student> Students { get; set; }
    }

    public class Country
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string CountryName { get; set; }

        public ICollection<State> States { get; set; }
    }

    public class State
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StateId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string StateName { get; set; }
        [Column(TypeName = "int")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }

    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityId { get; set; }
        [Column(TypeName = "varchar"), MaxLength(50)]
        public string CityName { get; set; }
        [Column(TypeName = "int")]
        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public State State { get; set; }


        public ICollection<Student> Students { get; set; }
    }
}