using SmallNetCore.Models.DBModels.FirstTestDb;

namespace SmallNetCore.IServices.Authority
{
    public interface IRoleService
    {
        public bool Add(Role model);

        public List<Role> Query();
    }
}
