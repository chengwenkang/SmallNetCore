using SmallNetCore.IRepository.SecondTestDb;
using SmallNetCore.Models.DBModels.SecondTestDb;
using SmallNetCore.Repository.Base;

namespace SmallNetCore.Repository.SecondTestDb
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {

    }
}
