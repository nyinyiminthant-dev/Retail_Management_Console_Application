using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Retail_Management_Console_Application.Database;

public static class MyConnection
{
    public static SqlConnectionStringBuilder connection = new SqlConnectionStringBuilder
    {
        DataSource = "192.168.0.184",
        InitialCatalog = "NyiNyiMinThant_RetailManagement",
        UserID = "thetys",
        Password = "P@ssw0rd",
        TrustServerCertificate = true,
    };
}
