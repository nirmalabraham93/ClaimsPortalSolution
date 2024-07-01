using AutoMapper;
using ClaimsPortal.Data.Entities;
using ClaimsPortal.Service.Interfaces;
using ClaimsPortal.Web.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;

namespace ClaimsPortal.Web.Controllers
{
    public class PolicyController : Controller
    {
        private readonly IPolicyHolderService _policyHolderService;
        private readonly IPolicyService _policyService;
        private readonly IVehicleService _vehicleService;
        private readonly IMapper _mapper;
        private readonly ILogger<PolicyController> _logger;
        public PolicyController(IPolicyHolderService policyHolderService, IPolicyService policyService, IVehicleService vehicleService, IMapper mapper, ILogger<PolicyController> logger)
        {
            _policyHolderService = policyHolderService;
            _policyService = policyService;
            _vehicleService = vehicleService;
            _mapper = mapper;
            _logger = logger;
        }
        public IActionResult CreatePolicy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePolicy(CreatePolicyViewModel policyViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(policyViewModel);
            }
            try
            {
                // Use AutoMapper to map the ViewModel to the domain entities
                var policyHolder = _mapper.Map<PolicyHolder>(policyViewModel);
                await _policyHolderService.AddPolicyHolderAsync(policyHolder);

                var policy = _mapper.Map<Policy>(policyViewModel);
                policy.PolicyHolderId = policyHolder.Id;
                await _policyService.AddPolicyAsync(policy);

                var vehicle = _mapper.Map<Vehicle>(policyViewModel);
                vehicle.PolicyId = policy.Id;
                await _vehicleService.AddVehicleAsync(vehicle);

                TempData["SuccessMessage"] = "Policy Added Successfully!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing your request.");
                return View(policyViewModel);
            }
        }
        public async Task<IActionResult> ViewPolicy()
        {
            try
            {
                _logger.LogInformation("Fetching policies.");

                List<PolicyViewModel> policyList = new List<PolicyViewModel>();

                var policies = await _policyService.GetAllPoliciesAsync();

                foreach (var policy in policies)
                {
                    var policyViewModel = new PolicyViewModel
                    {
                        PolicyId = policy.Id,
                        PolicyType = policy.PolicyType,
                        PolicyNumber = policy.PolicyNumber,
                        CoverageStartDate = policy.CoverageStartDate,
                        CoverageEndDate = policy.CoverageEndDate,
                        PolicyHolderId = policy.PolicyHolderId
                    };

                    // Retrieve PolicyHolder details
                    var policyHolder = await _policyHolderService.GetPolicyHolderByIdAsync(policy.PolicyHolderId);
                    if (policyHolder != null)
                    {
                        policyViewModel.PolicyHolderName = $"{policyHolder.FirstName} {policyHolder.LastName}";
                        policyViewModel.PolicyHolderEmail = policyHolder.Email;
                        policyViewModel.PolicyHolderAddress = policyHolder.Address;
                    }

                    // Retrieve Vehicle details
                    var vehicle = await _vehicleService.GetVehicleByPolicyIdAsync(policy.Id);
                    if (vehicle != null)
                    {
                        policyViewModel.VehicleId = vehicle.Id;
                        policyViewModel.VehicleMake = vehicle.Make;
                        policyViewModel.VehicleModel = vehicle.Model;
                    }

                    policyList.Add(policyViewModel);
                }

                return View(policyList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching policies.");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> UpdatePolicy(int pid)
        {
            try
            {
                var policy = await _policyService.GetPolicyByIdAsync(pid);

                if (policy == null)
                {
                    return NotFound();
                }

                var vehicle = await _vehicleService.GetVehicleByPolicyIdAsync(pid);

                var policyHolder = await _policyHolderService.GetPolicyHolderByIdAsync(policy.PolicyHolderId);

                if (policyHolder == null || vehicle == null)
                {
                    return NotFound();
                }

                var policymodel = new UpdatePolicyViewModel
                {
                    PolicyHolderId = policyHolder.Id,
                    FirstName = policyHolder.FirstName,
                    LastName = policyHolder.LastName,
                    Email = policyHolder.Email,
                    PhoneNumber = policyHolder.PhoneNumber,
                    PolicyId = policy.Id,
                    CoverageAmount = policy.CoverageAmount,
                    PremiumAmount = policy.PremiumAmount,
                    VehicleId = vehicle.Id,
                    Make = vehicle.Make,
                    Model = vehicle.Model,
                    EngineNumber = vehicle.EngineNumber
                };
                return View(policymodel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching policies.");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdatePolicy(UpdatePolicyViewModel updatePolicymodel)
        {
            if (!ModelState.IsValid)
            {
                return View(updatePolicymodel);
            }

            try
            {
                var policyToUpdate = await _policyService.GetPolicyByIdAsync(updatePolicymodel.PolicyId);
                if (policyToUpdate == null)
                {
                    return NotFound();
                }

                var vehicleToUpdate = await _vehicleService.GetVehicleByIdAsync(updatePolicymodel.VehicleId);
                if (vehicleToUpdate == null)
                {
                    return NotFound();
                }

                var policyHolderToUpdate = await _policyHolderService.GetPolicyHolderByIdAsync(updatePolicymodel.PolicyHolderId);
                if (policyHolderToUpdate == null)
                {
                    return NotFound();
                }

                // Update properties of the entity based on the view model
                policyHolderToUpdate.FirstName = updatePolicymodel.FirstName;
                policyHolderToUpdate.LastName = updatePolicymodel.LastName;
                policyHolderToUpdate.Email = updatePolicymodel.Email;
                policyHolderToUpdate.PhoneNumber = updatePolicymodel.PhoneNumber;

                await _policyHolderService.UpdatePolicyHolderAsync(policyHolderToUpdate);

                policyToUpdate.CoverageAmount = updatePolicymodel.CoverageAmount;
                policyToUpdate.PremiumAmount = updatePolicymodel.PremiumAmount;

                await _policyService.UpdatePolicyAsync(policyToUpdate);

                vehicleToUpdate.Make = updatePolicymodel.Make;
                vehicleToUpdate.Model = updatePolicymodel.Model;
                vehicleToUpdate.EngineNumber = updatePolicymodel.EngineNumber;

                await _vehicleService.UpdateVehicleAsync(vehicleToUpdate);

                TempData["SuccessMessage"] = "Policy Updated Successfully!";

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating policy.");
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeletePolicy(int pid, int phid, int vid)
        {
            try
            {
                // Delete the policy
                await _policyService.DeletePolicyAsync(pid);

                // Delete the policy holder
                await _policyHolderService.DeletePolicyHolderAsync(phid);

                // Delete the vehicle
                await _vehicleService.DeleteVehicleAsync(vid);

                // Set success message for user feedback
                TempData["SuccessMessage"] = "Policy and related information deleted successfully!";

                // Redirect to a relevant view or action after deletion
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the policy, policy holder, and vehicle.");
                return RedirectToAction("Error", "Home");
            }
        }

    }
}
