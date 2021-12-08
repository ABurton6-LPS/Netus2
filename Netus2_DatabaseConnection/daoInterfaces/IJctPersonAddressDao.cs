using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonAddressDao
    {
        /// <summary>
        /// Deletes the link between the two values passed in.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="connection"></param>
        public void Delete(int personId, int addressId, IConnectable connection);

        /// <summary>
        /// Queries the database for the link between the two values passed in.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public DataRow Read(int personId, int addressId, IConnectable connection);

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
        /// <param name="addressId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_WithAllAddressId(int addressId, IConnectable connection);

        /// <summary>
        /// Queries the database for any records that are not in the temporary table.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<DataRow> Read_AllAddressIsNotInTempTable(IConnectable connection);

        /// <summary>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database.
        /// If so, then updates the database record to match this object.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="isPrimary"></param>
        /// <param name="enumAddressId"></param>
        /// <param name="connection"></param>
        public void Update(int personId, int addressId, bool isPrimary, int enumAddressId, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the database.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="isPrimary"></param>
        /// <param name="enumAddressId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written.</returns>
        public DataRow Write(int personId, int addressId, bool isPrimary, int enumAddressId, IConnectable connection);

        /// <summary>
        /// Writes a link between the provided data points to the temporary table.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="connection"></param>
        public void Write_ToTempTable(int personId, int addressId, IConnectable connection);
    }
}
