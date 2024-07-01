using ClaimsPortal.Data.Entities;
using ClaimsPortal.Data.Repositories;
using ClaimsPortal.Service.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClaimsPortal.Service.Implementations
{
    public class VehicleService: IVehicleService
    {
        private readonly IVehicleRepository _repository;
        private readonly ILogger<VehicleService> _logger;

        public VehicleService(IVehicleRepository repository, ILogger<VehicleService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            try
            {
                return await _repository.GetAllVehiclesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching all vehicles.");
                throw;
            }
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(int id)
        {
            try
            {
                return await _repository.GetVehicleByIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching vehicle by Id.");
                throw;
            }
        }
        public async Task<Vehicle?> GetVehicleByPolicyIdAsync(int id)
        {
            try
            {
                return await _repository.GetVehicleByPolicyIdAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching vehicle by Id.");
                throw;
            }
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            try
            {
                await _repository.AddVehicleAsync(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding vehicle.");
                throw;
            }
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            try
            {
                await _repository.UpdateVehicleAsync(vehicle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating vehicle.");
                throw;
            }
        }

        public async Task DeleteVehicleAsync(int id)
        {
            try
            {
                await _repository.DeleteVehicleAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting vehicle.");
                throw;
            }
        }
       
    }
}
