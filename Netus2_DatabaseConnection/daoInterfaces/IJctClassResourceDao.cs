using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassResourceDao
    {
        /// <summary>
        /// Read the JctClassResourceDao from the database that have the provided classId and resourceId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        JctClassResourceDao Read(int classId, int resourceId, IConnectable connection);

        /// <summary>
        /// Read the JctClassResourceDaos from the database that have the provided classId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<JctClassResourceDao> Read_WithClassId(int classId, IConnectable connection);

        /// <summary>
        /// Read the JctClassResources from the database that have the provided resourceId.
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<JctClassResourceDao> Read_WithResourceId(int resourceId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new JctClassResourceDao record, linked to the provided classId and resourceId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        JctClassResourceDao Write(int classId, int resourceId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctClassResourceDao which is linked to the classId and periodId provided.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        void Delete(int classId, int resourceId, IConnectable connection);
    }
}
