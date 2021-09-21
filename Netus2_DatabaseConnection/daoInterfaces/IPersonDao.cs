using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IPersonDao
    {
        /// <summary>
        /// <para>
        /// Returns the specific Person object from the database which is indicated by the primary database key,
        /// personId, passed in.
        /// </para>
        /// Also populates the returned objects with any associated Application, Role, child Person, Address,
        /// PhoneNumber, UniqueIdentifier, EmploymentSession, Enrollment, and Mark objects.
        /// </summary>
        /// <param name="person"/>
        /// <param name="connection"/>
        Person Read_UsingPersonId(int personId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific Person object from the database which is indicated by the associated UniqueIdentifier
        /// primary key, passed in.
        /// </para>
        /// Also populates the returned objects with any associated Application, Role, child Person, Address,
        /// PhoneNumber, UniqueIdentifier, EmploymentSession, Enrollment, and Mark objects.
        /// </summary>
        /// <param name="uniqueId"></param>
        /// <param name="connection"></param>
        Person Read_UsingUniqueId(int uniqueId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of Person objects, read from the database, which match the parameters provided in the
        /// Person object passed in. Null values provided within the Person object will be ignored.
        /// </para>
        /// Also populates the returned objects with any associated Application, Role, child Person, Address,
        /// PhoneNumber, UniqueIdentifier, EmploymentSession, Enrollment, and Mark objects.
        /// </summary>
        /// <param name="person"/>
        /// <param name="connection"/>
        List<Person> Read(Person person, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided Person object to the database, then returns with that same object, but with the
        /// auto-generated Id field populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with any associated UniqueIdentifier, PhoneNumber, Address, Application,
        /// Role, Person, EmploymentSession, Enrollment, and Mark objects. This assumes that the associated
        /// objects have already been written into the database.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="connection"></param>
        Person Write(Person person, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Person object passed in, much like the Read methods do, then,
        /// if found, updates the database to match the object passed in. If it cannot be found, the Person object will be
        /// written to the database.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="connection"></param>
        void Update(Person person, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Person from any child Person, Role,
        /// Application, Address, and Class objects, and calls the Delete method for any PhoneNumber, EmploymentSession,
        /// UniqueIdentifier, Enrollment, and Mark objects that it is linked to.
        /// </para>
        /// Deletes the Person object passed in. All datapoints provided in the Person object must match exactly what is in
        /// the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="person"></param>
        /// <param name="connection"></param>
        void Delete(Person person, IConnectable connection);
    }
}
