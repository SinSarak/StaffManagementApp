using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using StaffManagementWebApp.ApplicationCores;
using StaffManagementWebApp.Models;
using StaffManagementWebApp.ViewModels;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace StaffManagementWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStaffAPIServices _staffAPIServices;
        private readonly IMapper _mapper;
        public HomeController(ILogger<HomeController> logger, IStaffAPIServices staffAPIServices, IMapper mapper)
        {
            _logger = logger;
            _staffAPIServices = staffAPIServices;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> StaffManagementAsync(CancellationToken cancellationToken)
        {
            var result = await _staffAPIServices.GetAllStaffInformationAsync(cancellationToken);
            if (!result.IsDidProcess)
            {
                foreach (var message in result.ReturnMessages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

            }
            return View(result);
        }
        public IActionResult CreateStaff()
        {
            ViewData["ListGender"] = new List<IntStringPairObject>() { new IntStringPairObject { Key = 1, Value = "Male" }, new IntStringPairObject() { Key = 2, Value = "Female" } };
            return View(new CreateStaffModel());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateStaffAsync(CreateStaffModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _staffAPIServices.CreateStaffInformationAsync(model, cancellationToken);

                //Return failed message
                if (!result.IsDidProcess)
                {
                    foreach (var message in result.ReturnMessages)
                    {
                        ModelState.AddModelError(string.Empty, message);
                    }

                }

                //Completed create staff
                else
                {
                    return RedirectToAction("StaffManagement");
                }
            }

            ViewData["ListGender"] = new List<IntStringPairObject>() { new IntStringPairObject { Key = 1, Value = "Male" }, new IntStringPairObject() { Key = 2, Value = "Female" } };
            return View(model);
        }
       

        public async Task<IActionResult> EditStaffAsync(string Id, CancellationToken cancellationToken)
        {
            var result = await _staffAPIServices.GetStaffInformationAsync(Id, cancellationToken);

            //Fail
            if(!result.IsDidProcess){
                return NotFound();
            }

            ViewData["ListGender"] = new List<IntStringPairObject>() { new IntStringPairObject { Key = 1, Value = "Male" }, new IntStringPairObject() { Key = 2, Value = "Female" } };
            return View(_mapper.Map<DisplayStaffViewModel, EditStaffModel>(result.Data));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditStaffAsync(string StaffId, EditStaffModel model, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _staffAPIServices.EditStaffInformationAsync(StaffId, model, cancellationToken);

                //Return failed message
                if (!result.IsDidProcess)
                {
                    foreach (var message in result.ReturnMessages)
                    {
                        ModelState.AddModelError(string.Empty, message);
                    }

                }

                //Completed edit staff
                else
                {
                    return RedirectToAction("StaffManagement");
                }
            }

            ViewData["ListGender"] = new List<IntStringPairObject>() { new IntStringPairObject { Key = 1, Value = "Male" }, new IntStringPairObject() { Key = 2, Value = "Female" } };
            return View(model);
        }

        public async Task<IActionResult> DeleteStaffAsync(string Id, CancellationToken cancellationToken)
        {
            var result = await _staffAPIServices.GetStaffInformationAsync(Id, cancellationToken);

            //Fail
            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return View(result.Data);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("DeleteStaff")]
        public async Task<IActionResult> DeleteStaffPostAsync(string StaffId, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var result = await _staffAPIServices.DeleteStaffInformationAsync(StaffId, cancellationToken);

                //Return failed message
                if (!result.IsDidProcess)
                {
                    foreach (var message in result.ReturnMessages)
                    {
                        ModelState.AddModelError(string.Empty, message);
                    }

                }

                //Completed edit staff
                else
                {
                    return RedirectToAction("StaffManagement");
                }
            }
            var staff = await _staffAPIServices.GetStaffInformationAsync(StaffId, cancellationToken);
            return View(staff.Data);
        }
        public async Task<IActionResult> AdvancedSearchAsync(SearchStaffModel model, CancellationToken cancellationToken)
        {
            var result = await _staffAPIServices.SearchStaffInformationAsync(model, cancellationToken);
            if (!result.IsDidProcess)
            {
                foreach (var message in result.ReturnMessages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }
            }
            var d = ModelState.ErrorCount;
            ViewData["RequestResult"] = result;
            ViewData["ListGender"] = new List<IntStringPairObject>() { new IntStringPairObject { Key = 1, Value = "Male" }, new IntStringPairObject() { Key = 2, Value = "Female" } };
            return View(model);
        }
        public async Task<IActionResult> ExportExcelAsync(SearchStaffModel model, CancellationToken cancellationToken)
        {
            var result = await _staffAPIServices.ExportStaffInformationAsExcelAsync(model, cancellationToken);
            if (!result.IsDidProcess)
            {
                foreach (var message in result.ReturnMessages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

            }
            return File(result.Data, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExportSearchStaff.xls");
        }
        public async Task<IActionResult> ExportPDFAsync(SearchStaffModel model, CancellationToken cancellationToken)
        {
            var result = await _staffAPIServices.ExportStaffInformationAsPDFAsync(model, cancellationToken);
            if (!result.IsDidProcess)
            {
                foreach (var message in result.ReturnMessages)
                {
                    ModelState.AddModelError(string.Empty, message);
                }

            }
            return File(result.Data, "application/pdf", "ExportSearchStaff.pdf");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
