using SmallNetCore.IRepository.FirstTestDb;
using SmallNetCore.IServices.Authority;
using SmallNetCore.Models.DBModels.FirstTestDb;

namespace SmallNetCore.Services.Authority
{
    public class RoleService : IRoleService
    {
        IRoleRepository roleRepository;

        public RoleService(IRoleRepository _roleRepository)
        {
            this.roleRepository = _roleRepository;
        }

        public bool Add(string roleName)
        {
            Role model = new()
            {
                RoleName = roleName,
            };
            var result = roleRepository.Insert(model);

            return result;
        }

        public List<Role> Query()
        {
            var result = roleRepository.GetList();
            return result;
        }
    }
}
