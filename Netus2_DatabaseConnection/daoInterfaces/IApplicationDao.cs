using Netus2.daoObjects;
using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface IApplicationDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of Applications, read from the database, which match the parameters provided in the 
        /// Application object passed in. Null values provided within the Application object will be ignored.
        /// </para>
        /// Also populates the returned object with the associated Provider object.
        /// </summary>
        /// <param name="application"/>
        /// <param name="connection"/>
        List<Application> Read(Application application, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of Applications, read from the database, which are linked to the primary database key 
        /// for a Provider object, providerId, passed in.
        /// </para>
        /// Also populates the returned object with the associated Provider object.
        /// </summary>
        /// <param name="providerId"/>
        /// <param name="connection"/>
        List<Application> Read_UsingProviderId(int providerId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific Application object from the database which is indicated by the primary database key, appId, passed in.
        /// </para>
        /// Also populates the returned object with the associated Provider object.
        /// </summary>
        /// <param name="appId"/>
        /// <param name="connection"/>
        Application Read_UsingAppId(int appId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided Application object to the database, then returns with that same object, but with the auto-generated Id 
        /// field populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with the associated Provider object. This assumes that the associated Provider object
        /// has already been written to the datbase.
        /// </summary>
        /// <param name="application"/>
        /// <param name="connection"/>
        Application Write(Application application, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Application object passed in, much like the Read 
        /// methods do, then, if found, updates the database to match the object passed in. If it cannot be found, 
        /// the Application object will be written to the database.
        /// </summary>
        /// <param name="application"/>
        /// <param name="connection"/>
        void Update(Application application, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Application from any associated Person
        /// objects.
        /// </para>
        /// Deletes the Application object passed in. All datapoints provided in the Application object must match 
        /// exactly what is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="appliction"/>
        /// <param name="connection"/>
        void Delete(Application appliction, IConnectable connection);
    }
}
