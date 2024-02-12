using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.VillaNumber;

namespace MagicVilla_VillaAPI.Repository.VillaNumber
{
    public class VillaNumberRepository : Repository<VillaNumberModel>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumberModel> UpdateAsync(VillaNumberModel e)
        {
            e.UpdatedDate = DateTime.Now;
            _db.VillaNumbers.Update(e);
            await _db.SaveChangesAsync();
            return e;
            
        }
    }
}
