using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctPersonPersonDao
    {
        /// <summary>
        /// Read a list of JctPersonPerson objects from the database that refrence the primary key of a Person object, personOneId.
        /// </summary>
        /// <param name="personOneId"/>
        /// <param name="connection"/>
        List<DataRow> Read(int personOneId, IConnectable connection);

        /// <summary>
        /// Read the specific JctPersonPerson object from the database that references the primary key of a Person object, personOneId, 
        /// and also references a separate Person object’s primary key, personTwoId.
        /// </summary>
        /// <param name="personOneId"/>
        /// <param name="personTwoId"/>
        /// <param name="connection"/>
        DataRow Read(int personOneId, int personTwoId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Write a record into the jct_person_person table which references the primary key for a Person object, personOneId, and also 
        /// references a separate Person object’s primary key, personTwoId.
        /// </para>
        /// There is a trigger that automatically fires in the background whenever this method used to write a record to the database. 
        /// It writes a second record to the database using the same values passed in, but reverses them so that the personTwoId is 
        /// utilized in the person_one_id field, and the personOneId is utilized in the person_two_id field.
        /// </summary>
        /// <param name="personOneId"/>
        /// <param name="personTwoId"/>
        /// <param name="connection"/>
        DataRow Write(int personOneId, int personTwoId, IConnectable connection);

        /// <summary>
        /// <para>
        /// Deletes the record from the jct_person_person table which references the primary key for a Person object, personOneId, and the 
        /// primary key for another Person object, personTwoId.
        /// </para>
        /// There is a trigger that automatically fires in the background whenever this method used to delete a record from the database. 
        /// It deletes any other record in the database referencing the same values passed in, but reverses them so that the personTwoId is 
        /// utilized in the person_one_id field, and the personOneId is utilized in the person_two_id field.
        /// </summary>
        /// <param name="personOneId"></param>
        /// <param name="personTwoId"></param>
        /// <param name="connection"></param>
        void Delete(int personOneId, int personTwoId, IConnectable connection);
    }
}