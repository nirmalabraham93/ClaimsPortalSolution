using System;
using System.Threading.Tasks;
using ClaimsPortal.Web.Controllers;
using ClaimsPortal.Service.Interfaces;
using ClaimsPortal.Web.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.AspNetCore.Mvc;
using ClaimsPortal.Data.Entities;
using NUnit.Framework;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ClaimsPortal.Tests.Controllers
{
    [TestFixture]
    public class PolicyControllerTests
    {
        private Mock<IPolicyHolderService> _mockPolicyHolderService;
        private Mock<IPolicyService> _mockPolicyService;
        private Mock<IVehicleService> _mockVehicleService;
        private Mock<IMapper> _mockMapper;
        private Mock<ILogger<PolicyController>> _mockLogger;
        private PolicyController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockPolicyHolderService = new Mock<IPolicyHolderService>();
            _mockPolicyService = new Mock<IPolicyService>();
            _mockVehicleService = new Mock<IVehicleService>();
            _mockMapper = new Mock<IMapper>();
            _mockLogger = new Mock<ILogger<PolicyController>>();

            _controller = new PolicyController(
                _mockPolicyHolderService.Object,
                _mockPolicyService.Object,
                _mockVehicleService.Object,
                _mockMapper.Object,
                _mockLogger.Object
            );
        }

        [TearDown]
        public void TearDown()
        {
            _controller?.Dispose();
        }

        [Test]
        public void CreatePolicy_Get_ReturnsViewResult()
        {
            // Act
            var result = _controller.CreatePolicy();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public async Task CreatePolicy_Post_InvalidModel_ReturnsViewWithModel()
        {
            // Arrange
            _controller.ModelState.AddModelError("Error", "Model Error");
            var model = new CreatePolicyViewModel();

            // Act
            var result = await _controller.CreatePolicy(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(model, viewResult.Model);
        }

        [Test]
        public async Task CreatePolicy_Post_ValidModel_RedirectsToHomeIndex()
        {
            // Arrange
            var model = new CreatePolicyViewModel();
            var policyHolder = new PolicyHolder { Id = 1 };
            var policy = new Policy { Id = 1, PolicyHolderId = 1 };
            var vehicle = new Vehicle { Id = 1, PolicyId = 1 };

            _mockMapper.Setup(m => m.Map<PolicyHolder>(model)).Returns(policyHolder);
            _mockMapper.Setup(m => m.Map<Policy>(model)).Returns(policy);
            _mockMapper.Setup(m => m.Map<Vehicle>(model)).Returns(vehicle);

            // Simulate ModelState as valid
            _controller.ModelState.Clear(); // Clear any existing errors
            _controller.ModelState.SetModelValue("PropertyName", new ValueProviderResult("Valid Value"));

            // Act
            var result = await _controller.CreatePolicy(model);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result, "Expected RedirectToActionResult");

            // Further assert specific properties if needed
            var redirectResult = result as RedirectToActionResult;
            Assert.AreEqual("Index", redirectResult.ActionName);
            Assert.AreEqual("Home", redirectResult.ControllerName);
        }



        [Test]
        public async Task CreatePolicy_Post_ServiceThrowsException_ReturnsViewWithModel()
        {
            // Arrange
            var model = new CreatePolicyViewModel();
            _mockPolicyHolderService.Setup(s => s.AddPolicyHolderAsync(It.IsAny<PolicyHolder>())).ThrowsAsync(new Exception());

            // Act
            var result = await _controller.CreatePolicy(model);

            // Assert
            var viewResult = result as ViewResult;
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.AreEqual(model, viewResult.Model);
        }
    }
}
