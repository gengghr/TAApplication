using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PS4.Data;
using PS4.Models;
using PS4.Areas.Identity.Data;
using System.Globalization;

namespace PS4.Controllers
{
    [Authorize]
    public class AdministratorController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly TAUsersRolesDB _context;
        private readonly UserManager<TAUser> _userManager;
        private readonly TAAPPContext TA_context;
        public AdministratorController(TAUsersRolesDB context, RoleManager<IdentityRole> roleManager, UserManager<TAUser> userManager, TAAPPContext _TA_contex)
        {
            _context = context;
            TA_context = _TA_contex;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Authorize(Roles = "Administrator")]
        public IActionResult Index()
        {
            return View();
        }

        public async Task<bool> IsAdmin(TAUser user)
        {
            return await _userManager.IsInRoleAsync(user, "Administrator");
        }

        /*Parameters:
         * id : user id
         * role: the target role
         * add: add(true) or remove(false)
         */
        public async Task<IdentityResult> ChangeRole(string id, string role, bool add)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (add)
                return await _userManager.AddToRoleAsync(user, role);
            else 
                return await _userManager.RemoveFromRoleAsync(user, role);
            
        }

        public IActionResult EnrollmentTrends()
        {
            return View();
        }

        //get selected data from DB
        public List<enrolls> GetEnrollmentData(string start_date,string end_date, string course )
        {
            var EList = TA_context.Enrollments.ToList();
            var tmp = EList.FindAll(s => s.CourseName == course);
            if (tmp == null)
                return null;
            var EL = tmp.OrderBy(s => s.EnrollmentDate);
            List<enrolls> result = new List<enrolls>();
            List<int> nums = new List<int>();
            string[] sd = start_date.Split(' ');
            string[] ed = end_date.Split(' ');
            string _sd = sd[1] + ' '+ sd[2];
            string _ed = ed[1] + ' ' + ed[2];
            //string this_name = null;
            foreach (var er in EL)
            {
                if(er.EnrollmentDate >= DateTime.Parse(_sd, CultureInfo.InvariantCulture) && er.EnrollmentDate <= DateTime.Parse(_ed, CultureInfo.InvariantCulture))
                {
                    nums.Add(er.EnrollmentNUmber);
                }
            }
            result.Add(new enrolls { name = course, number = nums.ToArray() });
            return result;
        }

        //get all data from DB
        public List<enrolls> GetAllEnrollmentData()
        {
            var EList = TA_context.Enrollments.ToList();
            var EL = EList.OrderBy(s => s.CourseName).ThenBy(s=>s.EnrollmentDate).ThenBy(s=>s.EnrollmentNUmber);
            List<enrolls> result = new List<enrolls>();
            List<int> nums = null;
            string this_name = null;
            foreach(var er in EL)
            {
                if(this_name != er.CourseName)
                {
                    if(this_name == null)
                    {
                        this_name = er.CourseName;
                        nums = new List<int>();
                        nums.Add(er.EnrollmentNUmber);
                    }
                    else
                    {
                        result.Add(new enrolls { name = this_name, number = nums.ToArray()});
                        this_name = er.CourseName;
                        nums = new List<int>();
                        nums.Add(er.EnrollmentNUmber);
                    }
                }
                else
                {
                    nums.Add(er.EnrollmentNUmber);
                }
            }
            return result;
        }

        //public List<string> GetDates()
        //{
        //    List<string> result = new List<string>();
        //    var EList = TA_context.Enrollments.ToList();
        //    foreach(var e in EList)
        //    {
        //        if(result.Count==0)
        //        {
        //            result.Add(e.EnrollmentDate);
        //        }
        //        else if(!result.Contains(e.EnrollmentDate))
        //        {
        //            result.Add(e.EnrollmentDate);
        //        }
        //    }
        //    result.Sort();
        //    return result;
        //}

    }


    //a data stucture to hold return data
    public class enrolls
    {
        public string name { get; set; }
        public int[] number { get; set; }

    }
}
