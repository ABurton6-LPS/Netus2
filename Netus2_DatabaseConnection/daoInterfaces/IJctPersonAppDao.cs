using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonAppDao
    {
        /// <summary>
        /// Read the JctPersonAppDao from the database that has the provided personId and appId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="appId"/>
        /// <param name="connection"/>
        JctPersonAppDao Read(int personId, int appId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAppDaos from the database that have the provided personId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<JctPersonAppDao> Read_WithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Read the JctPresonAppDaos from the database that have the provided appId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<JctPersonAppDao> Read_WithAppId(int appId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new JctPersonAppDao record, linked to the provided
        /// personId and appId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="appId"/>
        /// <param name="connection"/>
        JctPersonAppDao Write(int personId, int appId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctPersonAppDao which is linked to the
        /// personId and appId provided.
        /// <param name="personId"/>
        /// <param name="appId"/>
        /// <param name="connection"/>
        void Delete(int personId, int appId, IConnectable connection);
    }
}
