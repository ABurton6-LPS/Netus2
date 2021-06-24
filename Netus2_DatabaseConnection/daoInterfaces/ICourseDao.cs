using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface ICourseDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of Course objects, read from the database, which match the parameters provided in the 
        /// Course object passed in. Null values provided within the Course will be ignored.
        /// </para>
        /// Also populates the returned objects with any associated Subject and Grade objects.
        /// </summary>
        /// <param name="course"/>
        /// <param name="connection"/>
        List<Course> Read(Course course, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific Course object which is indicated by the primary database key, courseId, passed in.
        /// </para>
        /// Also populates the returned object with any associated Subject and Grade objects.
        /// </summary>
        /// <param name="courseId"/>
        /// <param name="connection"/>
        Course Read(int courseId, IConnectable connection);

        /// <summary>
        /// Writes the provided Course object to the database, then returns with that same object, but with the auto-generated Id field 
        /// populated using the primary key from the database.
        /// </summary>
        /// <param name="course"/>
        /// <param name="connection"/>
        Course Write(Course course, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Course object which is linked to the courseId, passed in, much like the 
        /// Read methods do, then, if found, updates the database to match the object passed in. If it cannot be found, the Course
        /// object will be written to the database.
        /// </summary>
        /// <param name="course"/>
        /// <param name="connection"/>
        void Update(Course course, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Course from any associated Subject and Grade objects, and
        /// calls the Delete method for any ClassEnrolled object that it is linked to.
        /// </para>
        /// Deletes the Course object passed in. All datapoints provided in the Course object must match exactly what is in the database 
        /// before the deletion can be successfully completed.
        /// </summary>
        /// <param name="course"/>
        /// <param name="connection"/>
        void Delete(Course course, IConnectable connection);
    }
}
