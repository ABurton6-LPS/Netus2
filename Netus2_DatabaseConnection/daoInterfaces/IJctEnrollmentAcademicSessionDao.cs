using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctEnrollmentAcademicSessionDao
    {
        /// <summary>
        /// Deletes the link between the two values passed in.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="academicSessionId"></param>
        /// <param name="connection"></param>
        public void Delete(int enrollmentId, int academicSessionId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the two values passed in.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="academicSessionId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int enrollmentId, int academicSessionId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithEnrollmentId(int enrollmentId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="academicSessionId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the database.
        /// </summary>
        /// <param name="enrollmentId"></param>
        /// <param name="academicSessionId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written.</returns>
        public DataRow Write(int enrollmentId, int academicSessionId, IConnectable connection);
    }
}
