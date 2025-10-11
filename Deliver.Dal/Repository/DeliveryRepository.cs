using Deliver.Dal.Data;
using Deliver.Entities.Interfaces;
using Deliver.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Deliver.Dal.Abstractions;

namespace Deliver.Dal.Repository
{
    public class DeliveryRepository(ApplicationDbContext context,IRepository<Delivery> repository) : IDeliveryRepository
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IRepository<Delivery> _repository = repository;

        public async Task<VehicleType?> GetVehicleTypeByEnumAsync(VehicleTypeenum vehicleType)
        {
            return await _context.vehicleTypes
                .FirstOrDefaultAsync(x =>x.vehicleType_name==vehicleType);
        }

        public async Task<bool> UserHasVehicleTypeAsync(int userId, int vehicleTypeId)
        {
            return await _context.Deliveries
                .AnyAsync(x => x.ApplicationUserId == userId && x.vehicle_type_id == vehicleTypeId);
        }

        public async Task AddDeliveryAsync(Delivery delivery)
        {
           await _repository.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        public async Task<Delivery> getDeliveryAsync(int userid)
        {
          return await _context.Deliveries.Include(x=>x.ApplicationUser).FirstOrDefaultAsync(x=>x.ApplicationUserId == userid);
        }

        public async Task updateDeliveryAsync(Delivery delivery)
        {
            _repository.Update(delivery);
            await _context.SaveChangesAsync();  
        }

        public async Task<bool> Checkemail(int userid, string email)
        {
            var user =await _context.Users.FirstOrDefaultAsync(x=>x.Id == userid);
            if (user != null)
            {
                 return await _context.Users.AnyAsync(x=>x.Email == email);

            }
            return false;
        }

    }
}
