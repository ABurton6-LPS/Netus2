using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface ILineItemDao
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
        /// Deletes any associated Mark records.
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="connection"></param>
        public void Delete(LineItem lineItem, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the databse for the record associated with the passed-in data.
        /// </para>
        /// Populates the returned record with any associated ClassEnrolled record.
        /// </summary>
        /// <param name="lineItemId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public LineItem Read_UsingLineItemId(int lineItemId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the records associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated ClassEnrolled record.
        /// </summary>
        /// <param name="classEnrolledId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<LineItem> Read_AllWithClassEnrolledId(int classEnrolledId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for any records that match what is provided. Null datapoints are ignored.
        /// </para>
        /// Populates the returned records with any associated ClassEnrolled record.
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<LineItem> Read(LineItem lineItem, IConnectable connection);

        /// <summary>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database.
        /// If so, then updates the database record to match this object.
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="connection"></param>
        public void Update(LineItem lineItem, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided record to the database.
        /// </para>
        /// Populates the returned record with any associated ClassEnrolled record.
        /// </summary>
        /// <param name="lineItem"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written, including the object Id, generated by the database.</returns>
        public LineItem Write(LineItem lineItem, IConnectable connection);
    }
}
