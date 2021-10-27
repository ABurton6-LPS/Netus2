using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassPersonDao
    {
        /// <summary>
        /// Deletes the link between the two values passed in.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        public void Delete(int classId, int personId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the passed in classId and personId, with the roleId provided.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="roleId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int classId, int personId, int roleId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithClassId(int classId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithRoleId(int roleId, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the database.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="roleId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written.</returns>
        public DataRow Write(int classId, int personId, int roleId, IConnectable connection);
    }
}
