using Netus2.daoObjects;
using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface IJctPersonAddressDao
    {
        /// <summary>
        /// Read the JctPersonAddressDao from the database that has the provided personId and addressId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        JctPersonAddressDao Read(int personId, int addressId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAddressDaos from the database that have the provided personId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<JctPersonAddressDao> Read_WithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAddressDaos from the database that have the provided addressId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<JctPersonAddressDao> Read_WithAddressId(int addressId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new JctPersonAddressDao record, linked to the provided
        /// personId and addressId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        JctPersonAddressDao Write(int personId, int addressId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctPersonAddressDao which is linked to the
        /// personId and addressId provided.
        /// <param name="personId"/>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        void Delete(int personId, int addressId, IConnectable connection);
    }
}
