using SRSWebApi.Models;

namespace SRSWebApi.Interfaces
{
    public interface ICourseDetailsRepository
    {
        bool AddCourseDetails(CourseDetail courseDetails);
        bool DeleteCourseDetails(int courseDetailsId);
        ICollection<CourseDetail> GetAllCourseDetails();
        CourseDetail GetCourseDetailsById(int courseDetailsId);
        bool UpdateCourseDetails(int courseDetailsId, CourseDetail courseDetails);
    }
}
