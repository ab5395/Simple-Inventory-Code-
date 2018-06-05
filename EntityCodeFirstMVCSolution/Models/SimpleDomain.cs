using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityCodeFirstMVCSolution.Controllers;

namespace EntityCodeFirstMVCSolution.Models
{
    public class SimpleDomain
    {
        public SchoolContext Sc = new SchoolContext();

        //Stadard 
        public void AddStandard(Standard st)
        {
            Standard standard = new Standard()
            {
                StandardName = st.StandardName
            };
            Sc.Standards.Add(standard);
            Sc.SaveChanges();
        }

        public List<Standard> GetStandardList()
        {
            return Sc.Standards.ToList();
        }

        public void UpdateStandard(Standard st)
        {
            Standard standard = Sc.Standards.FirstOrDefault(x => x.StandardId == st.StandardId);
            if (standard != null) standard.StandardName = st.StandardName;
            Sc.SaveChanges();
        }

        public void DeleteStandard(int standardId)
        {
            Standard standard = Sc.Standards.FirstOrDefault(x => x.StandardId == standardId);
            if (standard != null) Sc.Standards.Remove(standard);
            Sc.SaveChanges();
        }



        //Student
        public void AddStudent(Student st)
        {
            Student student = new Student()
            {
                StudentName = st.StudentName,
                DateOfBirth = st.DateOfBirth,
                StandardId = st.StandardId,
                TeacherId = st.TeacherId
            };
            Sc.Students.Add(student);
            Sc.SaveChanges();
        }

        public List<Student> GetStudentList()
        {
            return Sc.Students.ToList();
        }

        public void UpdateStudent(Student st)
        {
            Student student = Sc.Students.FirstOrDefault(x => x.StudentId == st.StudentId);
            if (student != null)
            {
                student.StudentName = st.StudentName;
                student.DateOfBirth = st.DateOfBirth;
                student.StandardId = st.StandardId;
                student.TeacherId = st.TeacherId;
            }
            Sc.SaveChanges();
        }

        public void DeleteStudent(int studentId)
        {
            Student student = Sc.Students.FirstOrDefault(x => x.StudentId == studentId);
            if (student != null) Sc.Students.Remove(student);
            Sc.SaveChanges();
        }




        //Teacher
        public List<Teacher> GetTeacherList()
        {
            return Sc.Teachers.ToList();
        }

        public void AddTeacher(Teacher tc)
        {
            Teacher teacher = new Teacher()
            {
                TeacherName = tc.TeacherName
            };
            Sc.Teachers.Add(teacher);
            Sc.SaveChanges();
        }

        public void UpdateTeacher(Teacher tc)
        {
            Teacher teacher = Sc.Teachers.FirstOrDefault(x => x.TeacherId == tc.TeacherId);
            if (teacher != null) teacher.TeacherName = tc.TeacherName;
            Sc.SaveChanges();
        }

        public void DeleteTeacher(int teacherid)
        {
            Teacher teacher = Sc.Teachers.FirstOrDefault(x => x.TeacherId == teacherid);
            if (teacher != null) Sc.Teachers.Remove(teacher);
            Sc.SaveChanges();
        }


        //State
        public List<State> GetStateList()
        {
            return Sc.States.ToList();
        }

        public State GetStateByid(int id)
        {
            return Sc.States.FirstOrDefault(x => x.StateId == id);
        }

        public void AddState(State st)
        {
            State state = new State()
            {
                StateName = st.StateName,
                CountryId = st.CountryId
            };
            Sc.States.Add(state);
            Sc.SaveChanges();
        }

        public void UpdateState(State st)
        {
            State state = Sc.States.FirstOrDefault(x => x.StateId == st.StateId);
            if (state != null)
            {
                state.StateName = st.StateName;
                state.CountryId = st.CountryId;
            }
            Sc.SaveChanges();
        }

        public void DeleteState(int stateid)
        {
            State state = Sc.States.FirstOrDefault(x => x.StateId == stateid);
            Sc.States.Remove(state);
            Sc.SaveChanges();
        }

        //Country
        public List<Country> GetCountryList()
        {
            return Sc.Countries.ToList();
        }

        public Country GetCountryByid(int id)
        {
            return Sc.Countries.FirstOrDefault(x => x.CountryId == id);
        }

        public void AddCountry(Country co)
        {
            Country country = new Country()
            {
                CountryName = co.CountryName
            };
            Sc.Countries.Add(country);
            Sc.SaveChanges();
        }

        public void UpdateCountry(Country co)
        {
            Country country = Sc.Countries.FirstOrDefault(x => x.CountryId == co.CountryId);
            if (country != null)
            {
                country.CountryName = co.CountryName;
            }
            Sc.SaveChanges();
        }

        public void DeleteCountry(int countryid)
        {
            Country country = Sc.Countries.FirstOrDefault(x => x.CountryId == countryid);
            Sc.Countries.Remove(country);
            Sc.SaveChanges();
        }

        //City
        public List<City> GetCityList()
        {
            return Sc.Cities.ToList();
        }

        public List<CityClass> GetCityClassList()
        {
            CommonController.CityClassList.Clear();
            foreach (var data in GetCityList())
            {
                var state = GetStateByid(data.StateId);
                var country = GetCountryByid(state.CountryId);
                CityClass cc=new CityClass()
                {
                    CityId = data.CityId,
                    CityName = data.CityName,
                    CountryName = country.CountryName,
                    StateName = state.StateName
                };
                CommonController.CityClassList.Add(cc);
            }
            return CommonController.CityClassList;
        }

        public void AddCity(City ci)
        {
            City city = new City()
            {
                CityName = ci.CityName,
                StateId = ci.StateId
            };
            Sc.Cities.Add(city);
            Sc.SaveChanges();
        }

        public void UpdateCity(City ci)
        {
            City city = Sc.Cities.FirstOrDefault(x => x.CityId == ci.CityId);
            if (city != null)
            {
                city.CityName = ci.CityName;
                city.StateId = ci.StateId;
            }
            Sc.SaveChanges();
        }

        public void DeleteCity(int cityid)
        {
            City city = Sc.Cities.FirstOrDefault(x => x.CityId == cityid);
            Sc.Cities.Remove(city);
            Sc.SaveChanges();
        }
    }

    public class CityClass
    {
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }

    public class EditStandardClass
    {
        public int StandardId { get; set; }
        public string StandardName { get; set; }
    }

    public class EditStudentClass
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }

        public int StandardId { get; set; }
        public int TeacherId { get; set; }
    }

    public class EditTeahcerClass
    {
        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
    }

    public class EditCountryCityStateClass
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int StateId { get; set; }
        public string StateName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
    }

}