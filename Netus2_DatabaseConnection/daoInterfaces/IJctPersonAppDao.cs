using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonAppDao
    {
        /// <summary>
        /// Read the DataRow from the database that has the provided personId and appId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="appId"/>
        /// <param name="connection"/>
        DataRow Read(int personId, int appId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAppDaos from the database that have the provided personId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<DataRow> Read_WithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAppDaos from the database that have the provided appId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<DataRow> Read_WithAppId(int appId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new DataRow record, linked to the provided
        /// personId and appId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="appId"/>
        /// <param name="connection"/>
        DataRow Write(int personId, int appId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the DataRow which is linked to the
        /// personId and appId provided.
        /// <param name="personId"/>
        /// <param name="appId"/>
        /// <param name="connection"/>
        void Delete(int personId, int appId, IConnectable connection);
    }
}
