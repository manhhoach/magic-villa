using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.Villa;

namespace MagicVilla_VillaAPI.Repository.Villa
{
    public interface IVillaRepository : IRepository<VillaModel>
    {
        Task<VillaModel> Update(VillaModel entity);
    }
}
