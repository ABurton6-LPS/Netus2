using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IAcademicSessionDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of AcademicSessions, read from the database, which match the parameters provided in the 
        /// AcademicSession object passed in. Null values provided within the AcademicSession object will be ignored. 
        /// This method will only return records that have no AcademicSession parentId in the database.
        /// </para>
        /// Also populates the returned objects with the associated Organization object, and any associated child AcademicSession objects.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="connection"/>
        List<AcademicSession> Read(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of AcademicSessions, read from the database, which match the parameters provided in the 
        /// AcademicSession object passed in, and are linked to the AcademicSession parentId passed in. Null values 
        /// provided within the AcademicSession object will be ignored.
        /// </para>
        /// Also populates the returned object with the associated Organization object, and any associated child AcademicSession objects.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="parentId"/>
        /// <param name="connection"/>
        List<AcademicSession> Read(AcademicSession academicSession, int parentId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific AcademicSession object from the database which is indicated by the primary 
        /// database key, academicSessionId, passed in.
        /// </para>
        /// Also populates the returned object with the associated Organization object, and any associated child AcademicSession objects.
        /// </summary>
        /// <param name="academicSessionId"/>
        /// <param name="connection"/>
        AcademicSession Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns the specific AcademicSession object from the database which is linked to the 
        /// primary database key for a ClassEnrolled object, classEnrolledId, passed in.
        /// </para>
        /// Also populates the returned object with the associated Organization object, and any associated child AcademicSession objects.
        /// </summary>
        /// <param name="classEnrolledId"/>
        /// <param name="connection"/>
        AcademicSession Read_UsingClassEnrolledId(int classEnrolledId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Returns a list of AcademicSessions, read from the database, which are linked to the 
        /// primary database key for an Organization object, organizationId, passed in.
        /// </para>
        /// Also populates the returned object with the associated Organization object, and any associated child AcademicSession objects.
        /// </summary>
        /// <param name="organizationId"/>
        /// <param name="connection"/>
        List<AcademicSession> Read_UsingOrganizationId(int organizationId, IConnectable connection);

        /// <summary>
        /// Returns the AcademicSession object which is linked to the data provided. The schoolCode is the buildingCode for the Organization table
        /// that this record will be linked to. The termCode and the schoolYear, in conjunction with the organizationId (for the Organization object
        /// with the buildingCode which matches the provided schoolCode value) create a unique identifier for a given AcademicSession object.
        /// </summary>
        /// <param name="schoolCode"></param>
        /// <param name="termCode"></param>
        /// <param name="schoolYear"></param>
        /// <param name="connection"></param>
        AcademicSession Read_UsingBuildingCode_TermCode_Schoolyear(string schoolCode, string termCode, int schoolYear, IConnectable connection);
        
        /// <summary>
        /// Returns a list containing the child Academic Sessions for the parent Academic Session passed in.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="connection"></param>
        List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided AcademicSession object to the database, then returns with that same object, 
        /// but with the auto-generated Id field populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with the associated Organization object. This assumes that 
        /// the associated Organization object has already been written into the database.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="connection"/>
        AcademicSession Write(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided AcademicSession object to the database, linking it to the parent AcademicSession 
        /// with the parentId passed in, then returns with that same object, but with the auto-generated Id field 
        /// populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with the associated Organization object. This assumes that the 
        /// associated Organization object has already been written into the database.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="parentId"/>
        /// <param name="connection"/>
        AcademicSession Write(AcademicSession academicSession, int parentId, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the AcademicSession object passed in, much like the Read 
        /// methods do, then, if found, updates the database to match the object passed in. If it cannot be found, 
        /// the AcademicSession object will be written to the database.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="connection"/>
        void Update(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// Searches the database for the record matching the AcademicSession object passed in, much like the Read 
        /// methods do, then, if found, updates the database to match the object passed in, linked to the parent 
        /// AcademicSession with the parentId passed in. If it cannot be found, the AcademicSession object, along 
        /// with the parentId, will be written to the database.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="parentId"/>
        /// <param name="connection"/>
        void Update(AcademicSession academicSession, int parentId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in AcademicSession from any child 
        /// AcademicSession objects, and calls the Delete method for the ClassEnrolled object that it is linked to.
        /// </para>
        /// Deletes the AcademicSession object passed in. All datapoints provided in the AcademicSession object must 
        /// match exactly what is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="academicSession"/>
        /// <param name="connection"/>
        void Delete(AcademicSession academicSession, IConnectable connection);
    }
}
