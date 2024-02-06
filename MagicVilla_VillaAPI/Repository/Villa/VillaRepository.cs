using MagicVilla_VillaAPI.Data;
using MagicVilla_VillaAPI.Generic;
using MagicVilla_VillaAPI.Models.Villa;

namespace MagicVilla_VillaAPI.Repository.Villa
{
    public class VillaRepository : Repository<VillaModel>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;
        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaModel> Update(VillaModel entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Villas.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }


    }
}
