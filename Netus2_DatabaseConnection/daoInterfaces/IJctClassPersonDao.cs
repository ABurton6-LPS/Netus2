using Netus2_DatabaseConnection.daoObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassPersonDao
    {
        /// <summary>
        /// Read the JctClassPersonDao from the database which is linked to the provided classId, and personId. 
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        JctClassPersonDao Read(int classId, int personId, IConnectable connection);

        /// <summary>
        /// Read the JctClassPersonDaos from the database which are linked to the provided classId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<JctClassPersonDao> Read_WithClassId(int classId, IConnectable connection);

        /// <summary>
        /// Read the JctClassPersonDaos from the database which are linked to the provided personId.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<JctClassPersonDao> Read_WithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Populates the database with the provided classId and personId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="roleId"></param>
        /// <param name="connection"></param>
        /// <returns>The JctClassPersonDao that was written.</returns>
        JctClassPersonDao Write(int classId, int personId, int roleId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctClasssPersonDao which is linked to the provided classId AND personId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        void Delete(int classId, int personId, IConnectable connection);
    }
}
