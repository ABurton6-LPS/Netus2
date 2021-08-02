using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IMarkDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of Mark objects, read from the database, which match the parameters provided in the Mark object 
        /// passed in, and are linked to the Person primary key, personId. Null values provided within the Mark will be ignored.
        /// </para>
        /// If the Mark object passed in is a null value, then this method will return a list of Marks, read
        /// from the database, which are linked to the primary key from the database for the associated Person object,
        /// personId, passed in.
        /// </summary>
        /// <param name="mark"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<Mark> Read(Mark mark, int personId, IConnectable connection);

        /// <summary>
        /// Returns a list of Mark objects, read from the database, which are linked to the primary key of the associated
        /// LineItem object, lineItemId, passed in.
        /// </summary>
        /// <param name="lineItemId"/>
        /// <param name="connection"/>
        List<Mark> Read_WithLineItemId(int lineItemId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided Mark object to the database, including the primary key for the associated Person object, 
        /// personId, then returns with that same object, but with the auto-generated Id field populated using the 
        /// primary key from the database.
        /// </para>
        /// Also populates the returned object with the associated LineItem object. This assumes that the associated
        /// LineItem object has already been written into the database.
        /// </summary>
        /// <param name="mark"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        Mark Write(Mark mark, int personId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Mark passed in, much like the Read methods do, then, if 
        /// found, updates the database to match the object passed in. If it cannot be found, the Mark object will 
        /// be written to the database.
        /// </summary>
        /// <param name="mark"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Update(Mark mark, int personId, IConnectable connection);

        /// <summary>
        /// Deletes the Mark object passed in. All datapoints in the Mark object must match exactly what is 
        /// in the database before the deletion can be successfully completed. This method assumes that there is
        /// no Person object associated with this Mark.
        /// <param name="mark"/>
        /// <param name="connection"/>
        void Delete(Mark mark, IConnectable connection);

        /// <summary>
        /// Deletes the Mark object passed in. All datapoints in the Mark object must match exactly what is in the 
        /// database, including the link to the personId passed in, before the deletion can be successfully completed.
        /// <param name="mark"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Delete(Mark mark, int personId, IConnectable connection);
    }
}
