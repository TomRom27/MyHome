using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace MyHome.Controllers
{
    public class Commons
    {
        public const string HomeControllerName = "Addresses";
        public const string DefaultUserRoleId = "2"; // User
        public const string SuperUserRole = "Super User";
        public const string ClaimUserGroup = "UserGroup";

    }
}