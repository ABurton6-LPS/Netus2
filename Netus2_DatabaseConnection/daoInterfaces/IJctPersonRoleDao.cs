using Netus2.daoObjects;
using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface IJctPersonRoleDao
    {
        /// <summary>
        /// Read a list of JctPersonRoleDao objects from the database that have the personId, passed in.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<JctPersonRoleDao> Read(int personId, IConnectable connection);

        /// <summary>
        /// Read the JctPersonRoleDao object from the database that have the personId and roleId, passed in.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="roleId"/>
        /// <param name="connection"/>
        JctPersonRoleDao Read(int personId, int roleId, IConnectable connection);

        /// <summary>
        /// Write a record into the jct_person_role table referencing the primary key for a Person object, 
        /// personId, and the primary key for a Role enumeration value, roleId.
        /// </summary>
        /// <param name="personId"/>
        /// <param name="roleId"/>
        /// <param name="connection"/>
        JctPersonRoleDao Write(int personId, int roleId, IConnectable connection);

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
