using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StaffManagementApp.ApplicationCores.DomainServices;
using StaffManagementApp.ApplicationCores.Models.BindingModels;
using StaffManagementApp.ApplicationCores.Models.DTOModels;
using StaffManagementApp.Data;
using StaffManagementApp.Domain.Entities;

namespace StaffManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _staffRepository;

        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;
        }

        [HttpGet]
        [Route("KeepLive")]
        public IActionResult KeepLive()
        {
            return Ok();
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<ActionResult<ReturnRequestModel<List<DisplayStaffDTO>>>> GetStaffs()
        {
            var staffs = await _staffRepository.GetAllStaffInformationAsync();

            return Ok(staffs);
        }

        // GET: api/Staff/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReturnRequestModel<DisplayStaffDTO>>> GetStaff(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest();
            }
            var staff = await _staffRepository.GetStaffInformationAsync(id);

            if (!staff.IsDidProcess)
            {
                return NotFound();
            }

            return Ok(staff);
        }
        
        // PUT: api/Staff/5
        [HttpPut("{id}")]
        public async Task<ActionResult<ReturnRequestModel<DisplayStaffDTO>>> PutStaff(string Id, EditStaffDTO staff)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _staffRepository.EditStaffInformationAsync(Id, staff);

            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return Ok(result);
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<ActionResult<ReturnRequestModel<DisplayStaffDTO>>> PostStaff(CreateStaffDTO staff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _staffRepository.CreateStaffInformationAsync(staff);
            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return result;
        }

        // DELETE: api/Staff/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ReturnRequestModel<DisplayStaffDTO>>> DeleteStaff(string Id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _staffRepository.DeleteStaffInformationAsync(Id);
            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return result;
        }

        [HttpPost]
        [Route("AdvancedSearch")]
        public async Task<ActionResult<ReturnRequestModel<List<DisplayStaffDTO>>>> SearchStaff(SearchStaffDTO staff)
        {
            var result = await _staffRepository.SearchStaffInformationAsync(staff);

            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return result;
        }
        [HttpPost]
        [Route("ExportSearchExcel")]
        public async Task<ActionResult<ReturnRequestModel<byte[]>>> ExportSearchStaffAsExcel(SearchStaffDTO staff)
        {
            var result = await _staffRepository.GetSearchStaffAsFileAsync(staff,ReportRenderType.Excel);

            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return result;
        }
        [HttpPost]
        [Route("ExportSearchPdf")]
        public async Task<ActionResult<ReturnRequestModel<byte[]>>> ExportSearchStaffAsPdf(SearchStaffDTO staff)
        {
            var result = await _staffRepository.GetSearchStaffAsFileAsync(staff, ReportRenderType.Pdf);

            if (!result.IsDidProcess)
            {
                return NotFound();
            }

            return result;
        }
    }
}
