using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Configuration;
using System.Web.Hosting;

namespace XmlUserStore
{

    public class UserStore : IUserStore<ApplicationUser>, IUserPasswordStore<ApplicationUser>, IUserSecurityStampStore<ApplicationUser>
    {
        private XDocument doc;

        public UserStore()
            : this(ConfigurationManager.AppSettings["UserXml"])
        {
        }

        public UserStore(string documentPath)
        {
            string fullPath = documentPath;
            if (!System.IO.File.Exists(documentPath))
            {
                fullPath = HostingEnvironment.MapPath(documentPath);
            }
            if (!System.IO.File.Exists(fullPath))
                throw new Exception(string.Format("{0} not found", documentPath));

            doc = new System.Xml.Linq.XDocument();
            try
            {
                doc = XDocument.Load(fullPath);
            }
            catch { }
        }


        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            ApplicationUser appUser = null;
            var user = doc.Descendants("credentials").Descendants("user").Where(e => e.Attribute("name").Value == userName).FirstOrDefault();
            if (user != null)
            {
                appUser = new ApplicationUser()
                {
                    Email = user.Attribute("name").Value,
                    UserName = user.Attribute("name").Value,
                    PasswordHash = user.Attribute("passwordhash").Value //, SecurityStamp = user.Attribute("securitystamp").Value,
                };
                var roles = user.Attribute("roles").Value.Split(',');
                appUser.roles = roles.ToList();
            }

            var ret = Task.FromResult<ApplicationUser>(appUser);
            return ret;

        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user)
        {
            var task = Task.FromResult<string>(user.PasswordHash);
            return task;
        }

        public Task CreateAsync(ApplicationUser user)
        {
            throw new Exception("");
            //var context = userStore.Context as ApplicationDbContext;
            //context.Users.Add(user);
            //context.Configuration.ValidateOnSaveEnabled = false;
            //return context.SaveChangesAsync();
        }
        public Task DeleteAsync(ApplicationUser user)
        {
            throw new Exception("");
            //var context = userStore.Context as ApplicationDbContext;
            //context.Users.Remove(user);
            //context.Configuration.ValidateOnSaveEnabled = false;
            //return context.SaveChangesAsync();
        }
        public Task<ApplicationUser> FindByIdAsync(string userId)
        {
            throw new Exception("");
            //var context = userStore.Context as ApplicationDbContext;
            //return context.Users.Where(u => u.Id.ToLower() == userId.ToLower()).FirstOrDefaultAsync();
        }

        public Task UpdateAsync(ApplicationUser user)
        {
            throw new Exception("");
            //var context = userStore.Context as ApplicationDbContext;
            //context.Users.Attach(user);
            //context.Entry(user).State = EntityState.Modified;
            //context.Configuration.ValidateOnSaveEnabled = false;
            //return context.SaveChangesAsync();
        }
        public void Dispose()
        {
            doc = null;
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user)
        {
            throw new Exception("");
            //var identityUser = ToIdentityUser(user);
            //var task = userStore.HasPasswordAsync(identityUser);
            //SetApplicationUser(user, identityUser);
            //return task;
        }
        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash)
        {
            throw new Exception("");
            //var identityUser = ToIdentityUser(user);
            //var task = userStore.SetPasswordHashAsync(identityUser, passwordHash);
            //SetApplicationUser(user, identityUser);
            //return task;
        }
        public Task<string> GetSecurityStampAsync(ApplicationUser user)
        {
            throw new Exception("");
            //var identityUser = ToIdentityUser(user);
            //var task = userStore.GetSecurityStampAsync(identityUser);
            //SetApplicationUser(user, identityUser);
            //return task;
        }
        public Task SetSecurityStampAsync(ApplicationUser user, string stamp)
        {
            throw new Exception("");
            //var identityUser = ToIdentityUser(user);
            //var task = userStore.SetSecurityStampAsync(identityUser, stamp);
            //SetApplicationUser(user, identityUser);
            //return task;
        }

        //private static void SetApplicationUser(ApplicationUser user, IdentityUser identityUser)
        //{
        //    user.PasswordHash = identityUser.PasswordHash;
        //    user.SecurityStamp = identityUser.SecurityStamp;
        //    user.Id = identityUser.Id;
        //    user.UserName = identityUser.UserName;
        //}
        //private IdentityUser ToIdentityUser(ApplicationUser user)
        //{
        //    return new IdentityUser
        //    {
        //        Id = user.Id,
        //        PasswordHash = user.PasswordHash,
        //        SecurityStamp = user.SecurityStamp,
        //        UserName = user.UserName
        //    };
        //}

    }


}