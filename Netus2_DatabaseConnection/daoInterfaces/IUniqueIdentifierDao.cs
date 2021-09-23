using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IUniqueIdentifierDao
    {
        /// <summary>
        /// Sets the taskId value to be used in "write" and "update" statements to indicate which process made the
        /// modification to the database.
        /// </summary>
        /// <param name="taskId"></param>
        void SetTaskId(int taskId);

        /// <summary>
        /// Returns the taskId value that was set with the SetTaskId method. If the value has not been set, will
        /// retun null;
        /// </summary>
        int? GetTaskId();

        /// <summary>
        /// <para>
        /// Returns a list of UniqueIdentifider objects, read from the database, which match the parameters provided
        /// in the UniqueIdentifier object passed in, and the primary key from the database for the associated Person object,
        /// personId, passed in. Null values provided with the UniqueIdentifier will be ignored.
        /// </para>
        /// If the UniqueIdentifier object passed in is a null value, then this method will return a list of 
        /// UniqueIdentifiers, read from the database, which are linked to the primary key from the database
        /// for the associated Person object, personId, passed in.
        /// </summary>
        /// <param name="uniqueId"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<UniqueIdentifier> Read(UniqueIdentifier uniqueId, int personId, IConnectable connection);

        /// <summary>
        /// Writes the provided UniqueIdentifier object to the database, including the primary key for the associated
        /// UniqueIdentifier object, uniqueId, then returns with that same object, but with the auto-generated Id
        /// field populated using the primary key from the database.
        /// </summary>
        /// <param name="uniqueId"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        UniqueIdentifier Write(UniqueIdentifier uniqueId, int personId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the UniqueIdentifier object which is linked to the personId,
        /// pased in, much like the Read method does, then, if found, updates the database to match the object passed in.
        /// If it cannot be found, the UniqueIdentifier object will be written to the database.
        /// </summary>
        /// <param name="uniqueId"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Update(UniqueIdentifier uniqueId, int personId, IConnectable connection);

        /// <summary>
        /// Deletes the UniqueIdentifier object passed in, which is linked to the personId passed in. All datapoints provided
        /// in the UniqueIdentifier object must match exactly what is in the database, including being linked to the provided
        /// personId, before the deletion can be successfully completed.
        /// </summary>
        /// <param name="uniqueId"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Delete(UniqueIdentifier uniqueId, int personId, IConnectable connection);
    }
}