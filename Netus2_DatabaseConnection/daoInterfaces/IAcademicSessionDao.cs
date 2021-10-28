using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IAcademicSessionDao
    {
        /// <summary>
        /// Sets the taskId value to be used in "write" and "update" statements, 
        /// to indicate which process made the modification to the database in the 
        /// "created_by" and "changed_by" fields, respectively.
        /// </summary>
        /// <param name="taskId"></param>
        void SetTaskId(int taskId);

        /// <summary>
        /// Returns the taskId value, used in "write" and "update" statements, 
        /// to indicate which process made the modification to the database in the 
        /// "created_by" and "changed_by" fields, respectively.
        /// </summary>
        /// <returns>Null, if no value has been set.</returns>
        int? GetTaskId();

        /// <summary>
        /// <para>
        /// Deletes the provided record from the database.
        /// </para>
        /// Unlinks any child records and/or Enrollment records.
        /// Deletes any associated ClassEnrolled records.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="connection"></param>
        public void Delete(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the records associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<AcademicSession> Read_AllWithOrganizationId(int organizationId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the databse for the record associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="classEnrolledId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public AcademicSession Read_UsingClassEnrolledId(int classEnrolledId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for a specific record, using the primary key value.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="academicSessionId"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public AcademicSession Read_UsingAcademicSessionId(int academicSessionId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the record associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="sisBuildingCode"></param>
        /// <param name="termCode"></param>
        /// <param name="trackCode"></param>
        /// <param name="schoolYear"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no record is found.</returns>
        public AcademicSession Read_UsingSisBuildingCode_TermCode_TrackCode_Schoolyear(string sisBuildingCode, string termCode, string trackCode, int schoolYear, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the parent of the record associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="child"></param>
        /// <param name="connection"></param>
        /// <returns>Null, if no parent record is found.</returns>
        public AcademicSession Read_Parent(AcademicSession child, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for the children of the record associated with the passed-in data.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no child records are found.</returns>
        public List<AcademicSession> Read_Children(AcademicSession parent, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for any records that match what is provided. Null datapoints are ignored.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<AcademicSession> Read(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// <para>
        /// Queries the database for any records that match what is provided, with the provided
        /// parent record. Null datapoints are ignored.
        /// </para>
        /// Populates the returned records with any associated Organization and/or child records.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        /// <returns>Empty list, if no records are found.</returns>
        public List<AcademicSession> Read(AcademicSession academicSession, int parentId, IConnectable connection);


        /// <summary>
        /// <para>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database, not linked to any parent record.
        /// If so, then updates the database record to match this object, setting any parentId to null.
        /// </para>
        /// The parentId is not used as part of the lookup phase.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="connection"></param>
        public void Update(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// <para>
        /// Checks to see if the provided data is associated to any record currently in the database.
        /// If not, then writes this record to the database, linked to the provided parentId.
        /// If so, then updates the database record to match this object, including a link to the provided parentId.
        /// </para>
        /// The parentId is not used as part of the lookup phase.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        public void Update(AcademicSession academicSession, int parentId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided record to the database.
        /// </para>
        /// Populates the returned records with any associated Organization records.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written, including the object Id, generated by the database.</returns>
        public AcademicSession Write(AcademicSession academicSession, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided record to the database, linked to the provided parentId.
        /// </para>
        /// Populates the returned record with any associated Organization records.
        /// </summary>
        /// <param name="academicSession"></param>
        /// <param name="parentId"></param>
        /// <param name="connection"></param>
        /// <returns>A copy of the record that was written, including the object Id, generated by the database.</returns>
        public AcademicSession Write(AcademicSession academicSession, int parentId, IConnectable connection);
    }
}
