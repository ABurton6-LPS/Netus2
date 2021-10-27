using Netus2_DatabaseConnection.dbAccess;
using System.Collections.Generic;
using System.Data;

namespace Netus2_DatabaseConnection.daoInterfaces
{
    public interface IJctClassResourceDao
    {
        public void Delete(int classId, int resourceId, IConnectable connection);

        public DataRow Read(int classId, int resourceId, IConnectable connection);

        public List<DataRow> Read_AllWithClassId(int classId, IConnectable connection);

        public List<DataRow> Read_AllWithResourceId(int resourceId, IConnectable connection);

        public DataRow Write(int classId, int resourceId, IConnectable connection);


    }
}
