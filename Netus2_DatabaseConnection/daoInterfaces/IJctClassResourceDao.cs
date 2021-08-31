using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassResourceDao
    {
        /// <summary>
        /// Read the DataRow from the database that have the provided classId and resourceId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        DataRow Read(int classId, int resourceId, IConnectable connection);

        /// <summary>
        /// Read the JctClassResourceDaos from the database that have the provided classId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<DataRow> Read_WithClassId(int classId, IConnectable connection);

        /// <summary>
        /// Read the JctClassResources from the database that have the provided resourceId.
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<DataRow> Read_WithResourceId(int resourceId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new DataRow record, linked to the provided classId and resourceId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        DataRow Write(int classId, int resourceId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the DataRow which is linked to the classId and periodId provided.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        void Delete(int classId, int resourceId, IConnectable connection);
    }
}
