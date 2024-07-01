using ClaimsPortal.Data.DBContext;
using ClaimsPortal.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Data.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ClaimsPortalDbContext _context;
        private readonly ILogger<VehicleRepository> _logger;

        public VehicleRepository(ClaimsPortalDbContext context, ILogger<VehicleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            try
            {
                return await _context.Vehicles.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vehicles.");
                throw;
            }
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            try
            {
                return await _context.Vehicles.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vehicle by Id.");
                throw;
            }
        }

        public async Task<Vehicle?> GetVehicleByPolicyIdAsync(int pid)
        {
            try
            {
                return await _context.Vehicles.FirstOrDefaultAsync(v => v.PolicyId == pid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching vehicle by Id.");
                throw;
            }
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            try
            {
                await _context.Vehicles.AddAsync(vehicle);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding vehicle.");
                throw;
            }
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.Vehicles.Update(vehicle);
                    await _context.SaveChangesAsync();

                    // Commit the transaction if everything succeeds
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    await transaction.RollbackAsync();
                    _logger.LogError(ex, "Error occurred while updating vehicle.");
                    throw;
                }
            }
        }

        public async Task DeleteVehicleAsync(int id)
        {
            try
            {
                var vehicle = await _context.Vehicles.FindAsync(id);
                if (vehicle != null)
                {
                    _context.Vehicles.Remove(vehicle);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting vehicle.");
                throw;
            }
        }
    }
}
