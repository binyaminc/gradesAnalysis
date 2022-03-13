using System;
using System.Collections.Generic;
using System.Text;
using BE;

namespace DAL
{
    public interface Idal
    {
        void AddCourse(Course course);

        void UpdateCourse(Course course);

        void DeleteCourse(Course course);

        List<Course> GetCourses();

        Course getCourseByName(String Name);

    }
    //TODO: check how to get the information via lev net
}
