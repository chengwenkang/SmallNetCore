using SmallNetCore.IServices.Authority;
using SmallNetCore.Models.DBModels.FirstTestDb;
using SmallNetCore.Services.Base;

namespace SmallNetCore.Services.Authority
{
    public class RoleService : BaseService<Role>, IRoleService
    {
        public bool Add(Role model)
        {
            //var result = dbContext.RoleDb.Insert(model);
            var result = Insert(model);

            return true;
        }

        public List<Role> Query()
        {
            var result = GetList();
            return result;
        }
    }
}
