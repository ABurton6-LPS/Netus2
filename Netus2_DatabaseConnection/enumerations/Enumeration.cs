
namespace Netus2.enumerations
{
    public class Enumeration
    {
        public int Id;
        public string Netus2Code;
        public string SisCode;
        public string HrCode;
        public string PipCode;
        public string Descript;

        public override string ToString()
        {
            return 
                "Id: " + Id + " " + 
                "Netus2Code: " + Netus2Code + " " +
                "SisCode: " + SisCode + " " +
                "HrCode: " + HrCode + " " +
                "PipCode: " + PipCode + " " +
                "Descript: " + Descript;
        }
    }
}
