using Microsoft.AspNetCore.Identity;

namespace RuilwinkelVaals.WebApp.Data.Models
{
    public class Role : IdentityRole<int>
    {
        public Role(string name)
            :base(roleName: name)
        {
            NormalizedName = name.ToUpper();
        }
    }    
}
