using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonPersonDao
    {
        /// <summary>
        /// Deletes both the links between the two values passed in.
        /// </summary>
        /// <param name="personOneId"></param>
        /// <param name="personTwoId"></param>
        /// <param name="connection"></param>
        public void Delete(int personOneId, int personTwoId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the two values passed in.
        /// </summary>
        /// <param name="personOneId"></param>
        /// <param name="personTwoId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int personOneId, int personTwoId, IConnectable connection);

        /// <summary>
        /// Queries the database for the records associated with the passed-in data.
        /// </summary>
        /// <param name="personOneId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithPersonOneId(int personOneId, IConnectable connection);

        /// <summary>
        /// Writes two links between the provided data points, one for each directrion, to the database.
        /// </summary>
        /// <param name="personOneId"></param>
        /// <param name="personTwoId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of one of the two records that were written, the one which matches the orentation of
        /// the passed in values.</returns>
        public DataRow Write(int personOneId, int personTwoId, IConnectable connection);


    }
}