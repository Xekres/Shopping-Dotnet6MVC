using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MvcWebUI.Entities
{
    public class CustomIdentityDbContext:IdentityDbContext<CustomIdentityUser,CustomIdentityRole,string>
    {
        //String= unique Identifier istiyor bizden Identity Db Context bu yüzden de kodsal karşılığı stringtir.
        public CustomIdentityDbContext(DbContextOptions<CustomIdentityDbContext> options):base (options)
        {

        }
    }
}
