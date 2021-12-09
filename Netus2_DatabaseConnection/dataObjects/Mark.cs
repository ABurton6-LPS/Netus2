using Netus2_DatabaseConnection.enumerations;
using System;
using System.Text;

namespace Netus2_DatabaseConnection.dataObjects
{
    public class Mark
    {
        public int Id { get; set; }
        public LineItem LineItem { get; set; }
        public Enumeration ScoreStatus { get; set; }
        public double Score { get; set; }
        public DateTime ScoreDate { get; set; }
        public string Comment { get; set; }

        public Mark(LineItem lineItem, Enumeration scoreStatus, double score, DateTime scoreDate)
        {
            Id = -1;
            LineItem = lineItem;
            ScoreStatus = scoreStatus;
            Score = score;
            ScoreDate = scoreDate;
        }

        public override string ToString()
        {
            StringBuilder strMark = new StringBuilder();
            strMark.Append("LineItem: " + LineItem + "\n");
            strMark.Append("Score Status: " + ScoreStatus + "\n");
            strMark.Append("Score: " + Score + "\n");
            strMark.Append("Score Date: " + ScoreDate + "\n");
            strMark.Append("Comment: " + Comment + "\n");

            return strMark.ToString();
        }
    }
}