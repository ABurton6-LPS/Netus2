using Netus2.dbAccess;
using System.Collections.Generic;

namespace Netus2.daoInterfaces
{
    public interface IAddressDao
    {
        /// <summary>
        /// Returns a list of Addresses, read from the database, which match the parameters provided in the Address object passed in. 
        /// Null values provided within the Address object will be ignored.
        /// </summary>
        /// <param name="address"/>
        /// <param name="connection"/>
        List<Address> Read(Address address, IConnectable connection);

        /// <summary>
        /// Returns the specific Address object from the database which is indicated by the primary database key, addressId, passed in.
        /// </summary>
        /// <param name="addressId"/>
        /// <param name="connection"/>
        Address Read_UsingAdddressId(int addressId, IConnectable connection);

        /// <summary>
        /// Writes the provided Address object to the database, then returns with that same object, but with the auto-generated Id field 
        /// populated using the primary key from the database.
        /// </summary>
        /// <param name="address"/>
        /// <param name="connection"/>
        Address Write(Address address, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Address object passed in, much like the Read methods do, then, if found, 
        /// updates the database to match the object passed in. If it cannot be found, the Address object will be written to the database.
        /// </summary>
        /// <param name="address"/>
        /// <param name="connection"/>
        void Update(Address address, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Address from any associated Person objects.
        /// </para>
        /// Deletes the Address object passed in. All datapoints provided in the Address object must match exactly what is in the database 
        /// before the deletion can be successfully completed.
        /// </summary>
        /// <param name="address"/>
        /// <param name="connection"/>
        void Delete(Address address, IConnectable connection);
    }
}