using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IProviderDao
    {
        /// <summary>
        /// Sets the taskId value to be used in "write" and "update" statements, 
        /// to indicate which process made the modification to the database in the 
        /// "created_by" and "changed_by" fields, respectively.
        /// </summary>
        /// <param name="taskId"></param>
        void SetTaskId(int taskId);

        /// <summary>
        /// Returns the taskId value, used in "write" and "update" statements, 
        /// to indicate which process made the modification to the database in the 
        /// "created_by" and "changed_by" fields, respectively.
        /// </summary>
        /// <returns>Null, if no value has been set.</returns>
        int? GetTaskId();

        /// <summary>
        /// <para>
        /// Deletes the provided record from the database.
        /// </para>
        /// Unlinks any child records.
        /// Deletes any associated Application records.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connection"></param>
        public void Delete(Provider provider, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for a specific record, using the primary key value.
        /// </para>
        /// Populates the returned record with any associated child records.
        /// </summary>
        /// <param name="providerId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public Provider Read_UsingProviderId(int providerId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for a specific record, using the primary key value.
        /// </para>
        /// Populates the returned record with any associated child records.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public Provider Read_UsingAppId(int appId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the children of the record associated with the passed-in data.
        /// </para>
        /// Populates the returned record with any associated child records.
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no child records are found.</returns>
        public List<Provider> Read_AllChildrenWithParentId(int parentId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for any records that match what is provided. Null datapoints are ignored.
        /// </para>
        /// Populates the returned record with any associated child records.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<Provider> Read(Provider provider, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for any records that match what is provided, with the provided
        /// parent record. Null datapoints are ignored.
        /// </para>
        /// Populates the returned record with any associated child records.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<Provider> Read(Provider provider, int parentId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database, not linked to any parent record.
        /// If so, then updates the database record to match this object, setting any parentId to null.
        /// </para>
        /// The parentId is not used as part of the lookup phase.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connection"></param>
        public void Update(Provider provider, IConnectable connection);

        /// <summary>
        /// <para>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database, linked to the provided parentId.
        /// If so, then updates the database record to match this object, including a link to the provided parentId.
        /// </para>
        /// The parentId is not used as part of the lookup phase.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        public void Update(Provider provider, int parentId, IConnectable connection);

        /// <summary>
        /// Writes the provided record to the database.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written, including the object Id, generated by the database.</returns>
        public Provider Write(Provider provider, IConnectable connection);

        /// <summary>
        /// Writes the provided record to the database, linked to the provided parentId.
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written, including the object Id, generated by the database.</returns>
        public Provider Write(Provider provider, int parentId, IConnectable connection);
    }
}