using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassPeriodDao
    {
        /// <summary>
        /// Read the JctClassPeriodDaos from the database that have the provided classId.
        /// </summary>
        /// <param name="classId"/>
        /// <param name="connection"/>
        List<DataRow> Read(int classId, IConnectable connection);

        /// <summary>
        /// Read the DataRow from the database that has the provided classId and periodId.
        /// </summary>
        /// <param name="classId"/>
        /// <param name="periodId"/>
        /// <param name="connection"/>
        DataRow Read(int classId, int periodId, IConnectable connection);

        /// <summary>
        /// Populates the database with a new DataRow record, linked to the provided
        /// classId and periodId.
        /// </summary>
        /// <param name="classId"/>
        /// <param name="periodId"/>
        /// <param name="connection"/>
        DataRow Write(int classId, int periodId, IConnectable connection);

        /// <summary>
        /// Deletes from the database, the DataRow which is linked to the
        /// classId and periodId provided.
        /// </summary>
        /// <param name="classId"/>
        /// <param name="periodId"/>
        /// <param name="connection"/>
        void Delete(int classId, int periodId, IConnectable connection);
    }
}
