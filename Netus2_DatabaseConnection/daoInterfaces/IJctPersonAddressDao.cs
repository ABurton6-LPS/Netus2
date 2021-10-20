using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonAddressDao
    {
        /// <summary>
        /// Read the DataRow from the database that has the provided personId and addressId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        DataRow Read(int personId, int addressId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAddressDaos from the database that have the provided personId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<DataRow> Read_WithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAddressDaos from the database that have the provided addressId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<DataRow> Read_WithAddressId(int addressId, IConnectable connection);

        /// <summary>
        /// Read the temporary JctPresonAddressDaos from the database that have the provided personId.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="connection"></param>
        List<DataRow> Read_AddressIsNotInTempTable(IConnectable connection);

        /// <summary>
        /// Populates the database with a new DataRow record, linked to the provided
        /// personId and addressId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        DataRow Write(int personId, int addressId, IConnectable connection);

        /// <summary>
        /// Populates the temporary database table associated with jct_person_address with
        /// the person_id and address_id provided.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="addressId"></param>
        /// <param name="connection"></param>
        void Write_TempTable(int personId, int addressId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the DataRow which is linked to the
        /// personId and addressId provided.
        /// <param name="personId"/>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        void Delete(int personId, int addressId, IConnectable connection);
    }
}
