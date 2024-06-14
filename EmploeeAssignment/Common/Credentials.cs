﻿namespace EmploeeAssignment.Common
{
    public class Credentials
    {
        public static string databaseName = Environment.GetEnvironmentVariable("databaseName");
        public static string containerName = Environment.GetEnvironmentVariable("containerName");
        public static string cosmosEndpoint = Environment.GetEnvironmentVariable("cosmosUrl");
        public static string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");
        internal static string docType = "employee";
        internal static string CreatedBy = "Mahesh";
    }
}
