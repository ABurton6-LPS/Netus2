using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IResourceDao
    {
        /// <summary>
        /// Returns a list of Resource objects, read from the database, which match the parameters provided
        /// in the Resource object passed in. Null values provided within the Resource will be ignored.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="connection"></param>
        List<Resource> Read(Resource resource, IConnectable connection);

        /// <summary>
        /// Returns the specific Resource object, which is linked to the primary key for a Resource object, resourceId.
        /// </summary>
        /// <param name="resourceId"></param>
        /// <param name="connection"></param>
        Resource Read_UsingResourceId(int resourceId, IConnectable connection);

        /// <summary>
        /// Writes the provided Resource object to the database, then returns with that same object, but with the
        /// auto-generated Id field populated using the primary key from the database.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="connection"></param>
        Resource Write(Resource resource, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Resource passed in, much like the Read methods do,
        /// if found, updates the database to match the object passed in. If it cannot be found, the Resource
        /// object will be written to the database.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="connection"></param>
        void Update(Resource resource, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Resource from any associated
        /// Class objects.
        /// </para>
        /// Deletes the Resource object passed in, which is not linked to a Resource object in the database.
        /// All datapoints in the Resource object must match exactly what is in the database before the
        /// deletion can be successfully completed.
        /// </summary>
        /// <param name="resource"></param>
        /// <param name="connection"></param>
        void Delete(Resource resource, IConnectable connection);
    }
}
