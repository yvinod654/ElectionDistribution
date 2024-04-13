using Microsoft.Extensions.Configuration;

namespace ElectionDistribution.Common_Utility
{
    public class SqlQueries
    {
        public static IConfiguration configuration = new ConfigurationBuilder().AddXmlFile(path: "SqlQueries.xml", optional: true, reloadOnChange: true).Build();
        public static string UserRegistration { get { return configuration[key: "UserRegistration"]; } }
        public static string UserLogin
        {
            get { return configuration[key: "UserLogin"]; }
        }
        public static string UserBySubdivision
        {
            get { return configuration[key: "UserBySubdivision"]; }
        }
        public static string SubdivisionRegistration
        {
            get { return configuration[key: "SubDivisionRegistration"]; }
        }
        public static string GetSubdivision
        {
            get { return configuration[key: "GetSubDivision"]; }
        }
        public static string GetAllSubDivision
        {
            get { return configuration[key: "GetAllSubDivision"]; }
        }
        public static string GetVoterDetails
        {
            get { return configuration[key: "GetVoterDetails"]; }
        }
        public static string AddVoter
        {
            get { return configuration[key: "AddVoter"]; }
        }
    }
}
