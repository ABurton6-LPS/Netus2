using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface IProviderDao
    {
        /// <summary>
        /// Returns a list of Provider objects, read from the database, which match the parameters provided
        /// in the Provider object passed in. Null values provided within the Provider will be ignored. This
        /// method assumes that these Provider objects have no parent Provider in the database.
        /// </summary>
        /// <param name="provider"/>
        /// <param name="connection"/>
        List<Provider> Read(Provider provider, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of Provider objects, read from the database, which match the parameters provided
        /// in the Provider object passed in, and are linked to the primary key for the parent Provider 
        /// object from the database, parentId, passed in. Null values provided within the Provider will be ignored.
        /// </para>
        /// If the Provider object passed in is a null value, then this method will return a list of 
        /// provider objects, read from the database, which are linked to the primary key from the database
        /// for the associated parent Provider object, parentId, passed in.
        /// </summary>
        /// <param name="provider"/>
        /// <param name="connection"/>
        List<Provider> Read(Provider provider, int parentId, IConnectable connection);

        /// <summary>
        /// Returns the specific Provider object, which is linked to the primary key for an Application object, appId.
        /// </summary>
        /// <param name="appId"/>
        /// <param name="connection"/>
        Provider Read_WithAppId(int appId, IConnectable connection);

        /// <summary>
        /// Returns the specific Provider object, read from the database, which is refrenced by the primary key for the Provider object, providerId.
        /// </summary>
        /// <param name="providerId"/>
        /// <param name="connection"/>
        Provider Read_WithProviderId(int providerId, IConnectable connection);

        /// <summary>
        /// Writes the provided Provider object to the database, then returns with that same object, but with the 
        /// auto-generated Id field populated using the primary key from the database. This method assumes that 
        /// the Provider being written should have no parent Provider object.
        /// </summary>
        /// <param name="provider"/>
        /// <param name="connection"/>
        Provider Write(Provider provider, IConnectable connection);

        /// <summary>
        /// Writes the provided Provider object to the database, including a link to the primary key for a parent
        /// Provider object in the database, parentProviderId, then returns with that same object, but with the 
        /// auto-generated Id field populated using the primary key from the database.
        /// </summary>
        /// <param name="provider"/>
        /// <param name="parentProviderId"/>
        /// <param name="connection"/>
        Provider Write(Provider provider, int parentProviderId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Provider passed in, much like the Read methods do, 
        /// then, if found, updates the database to match the object passed in. If it cannot be found, the Provider 
        /// object will be written to the database.
        /// </summary>
        /// <param name="provider"/>
        /// <param name="connection"/>
        void Update(Provider provider, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Provider from any child
        /// Provider objects, and calls the Delete method for any associated Application objects.
        /// </para>
        /// Deletes the Provider object passed in. All datapoints in the Provider object must match 
        /// exactly whta is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="provider"/>
        /// <param name="connection"/>
        void Delete(Provider provider, IConnectable connection);
    }
}