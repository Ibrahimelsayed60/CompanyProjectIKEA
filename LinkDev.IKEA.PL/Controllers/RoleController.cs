using AutoMapper;
using LinkDev.IKEA.PL.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.PL.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(string SearchValue)
        {
            if(string.IsNullOrEmpty(SearchValue))
            {
                var roles = await _roleManager.Roles.ToListAsync();
                var MappedRoles = _mapper.Map<IEnumerable<IdentityRole>, IEnumerable<RoleViewModel>>(roles);
                return View(MappedRoles);
            }
            else
            {
                var Role = await _roleManager.FindByNameAsync(SearchValue);
                var MappedRole = _mapper.Map<IdentityRole, RoleViewModel>(Role);
                return View(new List<RoleViewModel>() { MappedRole });
            }
        }
    }
}
