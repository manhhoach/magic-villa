using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.VillaNumber;

namespace MagicVilla_VillaAPI.Repository.VillaNumber
{
    public interface IVillaNumberRepository: IRepository<VillaNumberModel>
    {
        Task<VillaNumberModel> UpdateAsync(VillaNumberModel e);
    }
}
