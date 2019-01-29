using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Function_EntityFramework_Demo
{
    public static class GetAllClients
    {
        [FunctionName("GetAllClients")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            List<Clients> clientsList = new List<Clients>();
            using (var db = new ClientsContext())
            {
                clientsList = await db.clientEntities
                    .ToListAsync<Clients>();
            }
            if (clientsList.Any())
            {
                return (ActionResult)new OkObjectResult(clientsList);
            }
            else
            {
                return (ActionResult)new NoContentResult();
            }
        }
    }

    public class ClientsContext : DbContext
    {
        public DbSet<Clients> clientEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MyServer; Database=MyDatabase; Trusted_Connection=True;");
        }
    }

    [Table("Clients")]
    public class Clients
    {
        [Key]
        [Column("ClientId")]
        public int ClientId { get; set; }
        [Column("Name")]
        public string Name { get; set; }
        [Column("Specialist")]
        public long Specialist { get; set; }
    }
}
