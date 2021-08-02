using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface ILineItemDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of LineItem objects, read from the database, which match the parameters provided 
        /// in the LineItem object passed in. Null values provided within the LineItem will be ignored.
        /// </para>
        /// Also populates the returned objects with the associated ClassEnrolled object, respectively. This assumes
        /// that the associated ClassEnrolled object has been written into the database.
        /// </summary>
        /// <param name="lineItem"/>
        /// <param name="connection"/>
        List<LineItem> Read(LineItem lineItem, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific LineItem object that is refrenced by the primary key lineItemId, passed in.
        /// </para>
        /// Also populates the returned objects with the associated ClassEnrolled object, respectively. This assumes
        /// that the associated ClassEnrolled object has been written into the database.
        /// </summary>
        /// <param name="lineItemId"/>
        /// <param name="connection"/>
        LineItem Read(int lineItemId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of LineItem objects which are linked to the primary key value for a ClassEnrolled object, classEnrolledId, passed in.
        /// </para>
        /// Also populates the returned objects with the associated ClassEnrolled object, respectively. This assumes
        /// that the associated ClassEnrolled object has been written into the database.
        /// </summary>
        /// <param name="classEnrolledId"/>
        /// <param name="connection"/>
        List<LineItem> Read_WithClassEnrolledId(int classEnrolledId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided LineItem object to the database, then returns with that same object, but with the 
        /// auto-generated Id field populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with the associated ClassEnrolled object. This assumes that the
        /// associated ClassEnrolled object has already been written into the database.
        /// </summary>
        /// <param name="lineItem"/>
        /// <param name="connection"/>
        LineItem Write(LineItem lineItem, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the LineItem passed in, much like the Read methods do, then, 
        /// if found, updates the database to match the object passed in. If it cannot be found, the LineItem object 
        /// will be written to the database.
        /// </summary>
        /// <param name="lineItem"/>
        /// <param name="connection"/>
        void Update(LineItem lineItem, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method calls the Delete method of any Mark objects that it is linked to.
        /// </para>
        /// Deletes the LineItem object passed in. All datapoints in the LineItem object must match exactly what is 
        /// in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="lineItem"/>
        /// <param name="connection"/>
        void Delete(LineItem lineItem, IConnectable connection);
    }
}
