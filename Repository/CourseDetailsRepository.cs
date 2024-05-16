using SRSWebApi.Data;
using SRSWebApi.Interfaces;
using SRSWebApi.Models;

namespace SRSWebApi.Repository
{
    public class CourseDetailsRepository: ICourseDetailsRepository
    {
        private readonly SrsContext _context;

        public CourseDetailsRepository(SrsContext context)
        {
            _context = context;
        }

        public bool AddCourseDetails(CourseDetail courseDetails)
        {
            _context.CourseDetails.Add(courseDetails);
            _context.SaveChanges();
            return true;
        }

        public bool DeleteCourseDetails(int courseDetailsId)
        {
            var courseDetails = _context.CourseDetails.FirstOrDefault(cd => cd.CourseDetailsId == courseDetailsId);
            if (courseDetails != null)
            {
                _context.CourseDetails.Remove(courseDetails);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public ICollection<CourseDetail> GetAllCourseDetails()
        {
            return _context.CourseDetails.ToList();
        }

        public CourseDetail GetCourseDetailsById(int courseDetailsId)
        {
            return _context.CourseDetails.FirstOrDefault(cd => cd.CourseDetailsId == courseDetailsId);
        }

        public bool UpdateCourseDetails(int courseDetailsId, CourseDetail courseDetails)
        {
            var existingCourseDetails = _context.CourseDetails.FirstOrDefault(cd => cd.CourseDetailsId == courseDetailsId);
            if (existingCourseDetails != null)
            {
                existingCourseDetails.CourseCode = courseDetails.CourseCode;
                existingCourseDetails.CourseName = courseDetails.CourseName;
                existingCourseDetails.CourseDescription = courseDetails.CourseDescription;
                existingCourseDetails.CreditHours = courseDetails.CreditHours;
                existingCourseDetails.DepartmentId = courseDetails.DepartmentId;
                existingCourseDetails.PrerequisiteId = courseDetails.PrerequisiteId;
                // Update other properties as needed
                _context.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
