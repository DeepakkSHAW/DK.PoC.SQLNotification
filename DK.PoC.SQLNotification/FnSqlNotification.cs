using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Azure.WebJobs.Extensions.Sql;
using System.Collections.Generic;

namespace DK.PoC.SQLNotification
{
    public static class FnSqlNotification
    {
        [FunctionName("SqlNotification")]
        public static void Run(
            [SqlTrigger("TUser", ConnectionStringSetting = "SQLDBConnection")]
            IReadOnlyList<SqlChange<User>> changes,
            ILogger log)
        {
            log.LogWarning($"SQL notification triggered at {DateTime.Now}");

            foreach (var change in changes)
            {
                log.LogInformation($"Operation: {change.Operation}");
                log.LogInformation($"ID: {change.Item.user_id}");
                log.LogInformation($"Object:  {JsonSerializer.Serialize(change.Item)}");
            }


        }
    }
}
