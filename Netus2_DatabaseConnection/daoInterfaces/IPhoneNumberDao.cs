using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IPhoneNumberDao
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
        /// Returns a list of PhoneNumber objects, read from the database, which match the parameters provided
        /// in the PhoneNumber object passed in, and are linked to the primary key from the database for the
        /// associated Person object, personId. Null values provided within the PhoneNumber will be ignored.
        /// </para>
        /// If the PhoneNumber object passed in is a null value, then this method will return a list of 
        /// PhoneNumber objects, read from the database, which are linked to the primary key from the database
        /// for the associated Person object, personId, passed in.
        /// </summary>
        /// <param name="phoneNumber"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<PhoneNumber> Read(PhoneNumber phoneNumber, int personId, IConnectable connection);

        /// <summary>
        /// Returns a list of PhoneNumber objects, read from the database, which match the parameters provided
        /// in the PhoneNumber object passed in. Null values provided within the PhoneNumber will be ignored.
        /// This method assumes that the PhoneNumber passed in is not currently linked to any Person 
        /// objects within the database.
        /// </summary>
        /// <param name="phoneNumber"/>
        /// <param name="connection"/>
        List<PhoneNumber> Read(PhoneNumber phoneNumber, IConnectable connection);

        /// <summary>
        /// Writes the provided PhoneNumber object to the database, including the primary key for the associated
        /// Person object, personId, then returns with that same object, but with the auto-generated Id field
        /// populated using the primary key from the database.
        /// </summary>
        /// <param name="phoneNumber"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        PhoneNumber Write(PhoneNumber phoneNumber, int personId, IConnectable connection);

        /// <summary>
        /// Writes the provided PhoneNumber object to the database, including the primary key for the associated
        /// Person object, personId, then returns that same object, but with the auto-generated Id field
        /// populated using the primary key from the database. This method assumes that the phone number being
        /// written should not be linked to any Person object.
        /// </summary>
        /// <param name="phoneNumber"/>
        /// <param name="connection"/>
        PhoneNumber Write(PhoneNumber phoneNumber, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the PhoneNumber which is linked to the personId passed in,
        /// much like the Read methods do, if found, updates the database to match the object passed in.
        /// If it cannot be found, the PhoneNumber object will be written to the database.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        void Update(PhoneNumber phoneNumber, int personId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the PhoneNumber which is not linked to any Person object written
        /// to the database, much like the Read methods do, then, if found, updates the database to match the object passed in.
        /// If it cannot be found, the PhoneNumber object will be written to the database.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="connection"></param>
        void Update(PhoneNumber phoneNumber, IConnectable connection);

        /// <summary>
        /// Deletes the PhoneNumber object passed in, which is linked to the Person object in the database, 
        /// indicated by the primary key, personId, passed in. All datapoints in the PhoneNumber object 
        /// must match exactly what is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="personId"></param>
        /// <param name="connection"></param>
        void Delete(PhoneNumber phoneNumber, int personId, IConnectable connection);

        /// <summary>
        /// Deletes the PhoneNumber object passed in, which is not linked to a Person object in the database. 
        /// All datapoints in the PhoneNumber object must match exactly what is in the database before the 
        /// deletion can be successfully completed.
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="connection"></param>
        void Delete(PhoneNumber phoneNumber, IConnectable connection);
    }
}