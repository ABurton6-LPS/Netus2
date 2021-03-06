using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IClassEnrolledDao
    {
        /// <summary>
        /// Sets the taskId value to be used in "write" and "update" statements, 
        /// to indicate which process made the modification to the database in the 
        /// "created_by" and "changed_by" fields, respectively.
        /// </summary>
        /// <param name="taskId"></param>
        void SetTaskId(int taskId);

        /// <summary>
        /// Returns the taskId value, used in "write" and "update" statements, 
        /// to indicate which process made the modification to the database in the 
        /// "created_by" and "changed_by" fields, respectively.
        /// </summary>
        /// <returns>Null, if no value has been set.</returns>
        int? GetTaskId();

        /// <summary>
        /// <para>
        /// Deletes the provided record from the database.
        /// </para>
        /// Unlinks any Period, Resource, and/or Person (staff) records.
        /// Deletes any associated LineItem and/or Enrollment records.
        /// </summary>
        /// <param name="classEnrolled"></param>
        /// <param name="connection"></param>
        public void Delete(ClassEnrolled classEnrolled, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the records associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated AcademicSession, Course, Resource, and/or Period records.
        /// </summary>
        /// <param name="academicSessionId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<ClassEnrolled> Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the records associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated AcademicSession, Course, Resource, and/or Period records.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<ClassEnrolled> Read_UsingCourseId(int courseId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for a specific record, using the primary key value.
        /// </para>
        /// Populates the returned records with any associated AcademicSession, Course, Resource, and/or Period records.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public ClassEnrolled Read_UsingClassId(int classId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the record associated with the passed-in data.
        /// </para>
        /// Populates the returned record with any associated AcademicSession, Course, Resource, and/or Period records.
        /// </summary>
        /// <param name="classEnrolledCode"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public ClassEnrolled Read_UsingClassEnrolledCode(string classEnrolledCode, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for any records that match what is provided. Null datapoints are ignored.
        /// </para>
        /// Populates the returned records with any associated AcademicSession, Course, Resource, and/or Period records.
        /// </summary>
        /// <param name="classEnrolled"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<ClassEnrolled> Read(ClassEnrolled classEnrolled, IConnectable connection);

        /// <summary>
        /// <para>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database.
        /// If so, then updates the database record to match this object.
        /// </para>
        /// Also updates any associated Resource, Period, and/or Person (staff) records, if needed.
        /// </summary>
        /// <param name="classEnrolled"></param>
        /// <param name="connection"></param>
        public void Update(ClassEnrolled classEnrolled, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided record to the database.
        /// </para>
        /// Populates the returned record with any associated AcademicSession, Course, Resource, and/or Period records.
        /// </summary>
        /// <param name="classEnrolled"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written, including the object Id, generated by the database.</returns>
        public ClassEnrolled Write(ClassEnrolled classEnrolled, IConnectable connection);
    }
}
