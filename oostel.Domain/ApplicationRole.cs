using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Oostel.Domain
{
    public class ApplicationRole : IdentityRole<string>
    {
        public ApplicationRole(string name)
        {
            Name = name;
            NormalizedName = name.ToUpper();
        }
    }
}
