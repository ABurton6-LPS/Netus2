using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonRoleDao
    {
        /// <summary>
        /// Read a list of DataRow objects from the database that have the personId, passed in.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<DataRow> Read(int personId, IConnectable connection);

        /// <summary>
        /// Read the DataRow object from the database that have the personId and roleId, passed in.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="roleId"/>
        /// <param name="connection"/>
        DataRow Read(int personId, int roleId, IConnectable connection);

        /// <summary>
        /// Write a record into the jct_person_role table referencing the primary key for a Person object, 
        /// personId, and the primary key for a Role enumeration value, roleId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="roleId"/>
        /// <param name="connection"/>
        DataRow Write(int personId, int roleId, IConnectable connection);

        /// <summary>
        /// Deletes the record from the jct_person_role table which references the primary key for a Person 
        /// object, personId, and the primary key for a Role enumeration value, roleId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="roleId"/>
        /// <param name="connection"/>
        void Delete(int personId, int roleId, IConnectable connection);
    }
}
