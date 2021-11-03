namespace Netus2_DatabaseConnection.enumerations
{
    public class Enumeration
    {
        public int Id;
        public string Netus2Code;
        public string SisCode;
        public string HrCode;
        public string Descript;

        public override string ToString()
        {
            return 
                "Netus2Code: " + Netus2Code + " " +
                "SisCode: " + SisCode + " " +
                "HrCode: " + HrCode + " " +
                "Descript: " + Descript;
        }
    }
}
