using Netus2_DatabaseConnection.dataObjects;
using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IEnrollmentDao
    {
        /// <summary>
        /// <para>
        /// Returns a list of Enrollment objects, read from the database, which match the parameters provided in the 
        /// Enrollment object passed in. Null values provided within the Enrollment will be ignored.
        /// </para>
        /// If the Enrollment object passed in is a null value, then this method will return a list of Enrollment objects, read
        /// from the database, which are linked to the primary key from the database for the associated Person object,
        /// personId, passed in.
        /// </summary>
        /// <param name="enrollment"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        List<Enrollment> Read(Enrollment enrollment, int personId, IConnectable connection);

        /// <summary>
        /// Returns a list of Enrollment objects, read from the database, which are linked to the ClassEnrolled 
        /// object primary key, classId, passedIn. 
        /// </summary>
        /// <param name="classId"/>
        /// <param name="connection"/>
        List<Enrollment> Read_WithClassId(int classId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Writes the provided Enrollment object to the database, including the primary key for the associated 
        /// Person object, personId, then returns with that same object, but with the auto-generated Id field 
        /// populated using the primary key from the database.
        /// </para>
        /// Also populates the returned object with any associated AcademicEnrollment objects. This assumes that
        /// the associated AcademicSession objects have already been written into the database.
        /// </summary>
        /// <param name="enrollment"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        Enrollment Write(Enrollment enrollment, int personId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Searches the database for the record matching the Enrollment which is linked to the personId passed in, 
        /// much like the Read methods do, then, if found, updates the database to match the object passed in. 
        /// If it cannot be found, the Enrollment object will be written to the database.
        /// </para>
        /// This method also will update the database with any relevant changes to the link between this Enrollment
        /// object and any associated AcademicSession objects.
        /// </summary>
        /// <param name="enrollment"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Update(Enrollment enrollment, int personId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Deletes the Enrollment object passed in. All datapoints in the Enrollment object must match exactly what 
        /// is in the database before the deletion can be successfully completed.
        /// </para>
        /// Also deletes the link to the associated AcademicSession.
        /// </summary>
        /// <param name="enrollment"/>
        /// <param name="connection"/>
        void Delete(Enrollment enrollment, IConnectable connection);

        /// <summary>
        /// <para>
        /// Before performing the deletion, this method unlinks the passed in Enrollment from any associated 
        /// AcademicSession objects
        /// </para>
        /// Deletes the Enrollment object passed in. All datapoints in the Enrollment object, including a link to the 
        /// personId passed in, must match exactly what is in the database before the deletion can be successfully completed.
        /// </summary>
        /// <param name="enrollment"/>
        /// <param name="personId"/>
        /// <param name="connection"/>
        void Delete(Enrollment enrollment, int personId, IConnectable connection);
    }
}
