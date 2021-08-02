using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IEmploymentSessionDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of EmploymentSession objects, read from the database, which match the parameters provided in the 
        /// EmploymentSession object, including the link to the Person object primary key, personId, passed in. Null values 
        /// provided within the EmploymentSession will be ignored.
        /// </para>
        /// Also populates the returned objects with their associated Organization object.
        /// </summary>
        /// <param name="employmentSession"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<EmploymentSession> Read_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of EmploymentSession objects, read from the database, which match the parameters provided in the 
        /// EmploymentSession object, including the link to the Organization object primary key, organizationId, passed in. Null 
        /// values provided within the EmploymentSession will be ignored.
        /// </para>
        /// Also populates the returned objects with their associated Organization object.
        /// </summary>
        /// <param name="employmentSession"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<EmploymentSession> Read_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided EmploymentSession object to the database, including the primary key for the associated 
        /// Person object, personId, then returns with that same object, but with the auto-generated Id field populated 
        /// using the primary key from the database.
        /// </para>
        /// Also populates the returned object with the associated Organization object. This assumes that the associated
        /// Organization object has already been written into the database.
        /// </summary>
        /// <param name="employmentSession"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        EmploymentSession Write(EmploymentSession employmentSession, int personId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the EmploymentSession which is linked to the personId passed in, 
        /// much like the Read methods do, then, if found, updates the database to match the object passed in. If it cannot 
        /// be found, the EmploymentSession object will be written to the database.
        /// </summary>
        /// <param name="employmentSession"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Update(EmploymentSession employmentSession, int personId, IConnectable connection);

        /// <summary>
        /// Deletes the EmploymentSession object passed in. All datapoints in the EmploymentSession 
        /// object must match exactly what is in the database, including the link to the personId passed in, 
        /// before the deletion can be successfully completed.
        /// <param name="employmentSession"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Delete_WithPersonId(EmploymentSession employmentSession, int personId, IConnectable connection);

        /// <summary>
        /// Deletes the EmploymentSession object passed in. All datapoints in the EmploymentSession object must 
        /// match exactly what is in the database, including the link to the organizationId passed in, before the 
        /// deletion can be successfully completed.
        /// <param name="employmentSession"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Delete_WithOrganizationId(EmploymentSession employmentSession, int organizationId, IConnectable connection);
    }
}
