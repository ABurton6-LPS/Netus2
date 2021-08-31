using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassPersonDao
    {
        /// <summary>
        /// Read the DataRow from the database which is linked to the provided classId, and personId. 
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        DataRow Read(int classId, int personId, IConnectable connection);

        /// <summary>
        /// Read the JctClassPersonDaos from the database which are linked to the provided classId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<DataRow> Read_WithClassId(int classId, IConnectable connection);

        /// <summary>
        /// Read the JctClassPersonDaos from the database which are linked to the provided personId.
        /// </summary>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        List<DataRow> Read_WithPersonId(int personId, IConnectable connection);

        /// <summary>
        /// Populates the database with the provided classId and personId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="roleId"></param>
        /// <param name="connection"></param>
        /// <returns>The DataRow that was written.</returns>
        DataRow Write(int classId, int personId, int roleId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the JctClasssPersonDao which is linked to the provided classId AND personId.
        /// </summary>
        /// <param name="classId"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        void Delete(int classId, int personId, IConnectable connection);
    }
}
