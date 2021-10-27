using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctCourseSubjectDao
    {
        /// <summary>
        /// Deletes the link between the two values passed in.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="subjectId"></param>
        /// <param name="connection"></param>
        public void Delete(int courseId, int subjectId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the two values passed in.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="subjectId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int courseId, int subjectId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithCourseId(int courseId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="subjectId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithSubjectId(int subjectId, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the database.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="subjectId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written.</returns>
        public DataRow Write(int courseId, int subjectId, IConnectable connection);
    }
}
