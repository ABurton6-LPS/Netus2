using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IOrganizationDao
    {
        /// <summary>
        /// Return a list of Organization objects, read from the database, which match the parameters provided in the 
        /// Organization object passed in. Null values provided within the Organization will be ignored.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="connection"/>
        List<Organization> Read(Organization organization, IConnectable connection);

        /// <summary>
        /// Returns the specific Organization object, read from the database, which is refrenced by the primary key for the Organization object, orgId.
        /// </summary>
        /// <param name="organizationId"/>
        /// <param name="connection"/>
        Organization Read_WithOrganizationId(int organizationId, IConnectable connection);

        /// <summary>
        /// Returns the specific Organization object, which is linked to the primary key for an AcademicSession object, academicSessionId.
        /// </summary>
        /// <param name="academicSessionId"/>
        /// <param name="connection"/>
        Organization Read_WithAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// Returns the specific Organization object, which is linked to the provided sisBuildingCode.
        /// </summary>
        /// <param name="sisBuildingCode"></param>
        /// <param name="connection"></param>
        Organization Read_WithSisBuildingCode(string sisBuildingCode, IConnectable connection);

        /// <summary>
        /// Returns a list of Organization objects, read from the database, which match the parameters provided in the 
        /// Organization object passed in, and linked to the primary key of the parent Organization object, parentId. 
        /// Null values provided within the Organization will be ignored.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="parentId"/>
        /// <param name="connection"/>
        List<Organization> Read(Organization organization, int parentId, IConnectable connection);

        /// <summary>
        /// Writes the provided Organization object to the database, then returns with that same object, but with the 
        /// auto-generated Id field populated using the primary key from the database.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="connection"/>
        Organization Write(Organization organization, IConnectable connection);

        /// <summary>
        /// Writes the provided Organization object to the database, including a link to the parent Organization primary key, 
        /// parentOrganizationId, then returns with that same object, but with the auto-generated Id field populated using 
        /// the primary key from the database.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="connection"/>
        Organization Write(Organization organization, int parentId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Organization passed in, much like the Read methods do, 
        /// then, if found, updates the database to match the object passed in. If it cannot be found, the Organization 
        /// object will be written to the database.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="connection"/>
        void Update(Organization organization, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the Organization passed in, and linked to the primary key 
        /// of the parent Organization object, parentOrganizationId, much like the Read methods do, then, if found, 
        /// updates the database to match the object passed in. If it cannot be found, the Organization object will 
        /// be written to the database.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="parentOrganizationId"/>
        /// <param name="connection"/>
        void Update(Organization organization, int parentOrganizationId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Organization from any child
        /// Organization objects, and calls the Delete method for any associated BuildingCode, EmploymentSession,
        /// and AcademicSession objects.
        /// </para>
        /// Deletes the Organization object passed in. All datapoints in the Organization object must match exactly 
        /// what is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="organization"/>
        /// <param name="connection"/>
        void Delete(Organization organization, IConnectable connection);
    }
}
