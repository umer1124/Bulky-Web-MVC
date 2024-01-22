using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.Models.ViewModels
{
    public class RoleManagementViewModel
    {
        public string UserId { get; set; }

        public string Name { get; set; }

        public string RoleId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> RoleList { get; set; }

        public int? CompanyId { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
    }
}
