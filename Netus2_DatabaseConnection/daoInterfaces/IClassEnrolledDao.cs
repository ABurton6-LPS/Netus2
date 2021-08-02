using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IClassEnrolledDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of ClassEnrolled objects, read from the database, which match the parameters provided in 
        /// the ClassEnrolled object passed in. Null values provided within the ClassEnrolled will be ignored.
        /// </para>
        /// Also populates the returned objects with any associated Resource, Period, and Person (staff) objects.
        /// </summary>
        /// <param name="classEnrolled"/>
        /// <param name="connection"/>
        List<ClassEnrolled> Read(ClassEnrolled classEnrolled, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific ClassEnrolled object which is indicated by the primary key for the ClassEnrolled object, classId, passed in.
        /// </para>
        /// Also populates the returned objects with any associated Resource, Period, and Person (staff) objects.
        /// </summary>
        /// <param name="classId"/>
        /// <param name="connection"/>
        ClassEnrolled Read(int classId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of ClassEnrolled objects, read from the database, which are linked to the primary key for the 
        /// associated AcademicSession object, academicSessionId, passed in.
        /// </para>
        /// Also populates the returned objects with any associated Resource, Period, and Person (staff) objects.
        /// </summary>
        /// <param name="academicSessionId"/>
        /// <param name="connection"/>
        List<ClassEnrolled> Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of ClassEnrolled objects, read from the database, which are linked to the primary key for 
        /// the associated Course object, courseId, passed in.
        /// </para>
        /// Also populates the returned objects with any associated Resource, Period, and Person (staff) objects.
        /// </summary>
        /// <param name="courseId"/>/
        /// <param name="connection"/>/
        List<ClassEnrolled> Read_UsingCourseId(int courseId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided ClassEnrolled object to the database, then returns with that same object, but with the 
        /// auto-generated Id field populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with any associated Resource, Period and Person (staff) objects. This assumes 
        /// that these associated objects have already been written to the database.
        /// </summary>
        /// <param name="classEnrolled"/>
        /// <param name="connection"/>
        ClassEnrolled Write(ClassEnrolled classEnrolled, IConnectable connection);

        /// <summary>
        /// <para>
        /// Searches the database for the record matching the ClassEnrolled object passed in, much like the Read methods do, then, 
        /// if found, updates the database to match the object passed in. If it cannot be found, the ClassEnrolled object will be 
        /// written to the database.
        /// </para>
        /// Also changes the connection to any Period, Resource, or Person (staff) objects, as needed.
        /// </summary>
        /// <param name="classEnrolled"/>
        /// <param name="connection"/>
        void Update(ClassEnrolled classEnrolled, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in ClassEnrolled from any associated Period, Resource,
        /// or Person (staff) objects, and calls the Delete method for any associated LineItem or enrollment objects.
        /// </para>
        /// Deletes the ClassEnrolled object passed in. All datapoints provided in the ClassEnrolled object must match 
        /// exactly what is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="classEnrolled"/>
        /// <param name="connection"/>
        void Delete(ClassEnrolled classEnrolled, IConnectable connection);
    }
}
