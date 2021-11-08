using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonPhoneNumberDao
    {
        /// <summary>
        /// Deletes the link between the two values passed in.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="phoneNumberId"></param>
        /// <param name="connection"></param>
        public void Delete(int personId, int phoneNumberId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the two values passed in.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="phoneNumberId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int personId, int phoneNumberId, IConnectable connection);

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
        /// <param name="phoneNumberId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllWithPhoneNumberId(int phoneNumberId, IConnectable connection);

        /// <summary>
        /// Queries the database for any records that are not in the temporary table.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllPhoneNumberIsNotInTempTable(IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the database.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="phoneNumberId"></param>
        /// <param name="isPrimaryId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written.</returns>
        public DataRow Write(int personId, int phoneNumberId, int isPrimaryId, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the temporary table.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="phoneNumberId"></param>
        /// <param name="connection"></param>
        public void Write_ToTempTable(int personId, int phoneNumberId, IConnectable connection);
    }
}
