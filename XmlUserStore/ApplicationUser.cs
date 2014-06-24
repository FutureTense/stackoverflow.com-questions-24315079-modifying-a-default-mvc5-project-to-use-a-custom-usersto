using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;

namespace XmlUserStore
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        public ApplicationUser()
        {
            this.Id = System.Guid.NewGuid().ToString();
        }
        public ApplicationUser(string userName)
            : this()
        {
            UserName = userName;
        }
        public virtual string Id { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual string SecurityStamp { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Email { get; set; }
        public virtual List<String> roles { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var claims = new System.Collections.Generic.List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, this.UserName));
            claims.Add(new Claim(ClaimTypes.Email, this.Email));
            foreach (var role in this.roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var id = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            var task = await Task.FromResult<ClaimsIdentity>(id);
            return task;
        }
    }
}