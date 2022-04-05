using SmallNetCore.Models.DBModels.FirstTestDb;

namespace SmallNetCore.IServices.Authority
{
    public interface IRoleService
    {
        public bool Add(string roleName);

        public List<Role> Query();
    }
}
