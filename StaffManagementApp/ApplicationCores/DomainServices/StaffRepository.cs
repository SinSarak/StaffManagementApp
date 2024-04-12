using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using StaffManagementApp.ApplicationCores.Extensions;
using StaffManagementApp.ApplicationCores.Models.BindingModels;
using StaffManagementApp.ApplicationCores.Models.DTOModels;
using StaffManagementApp.ApplicationCores.ValueObjects;
using StaffManagementApp.Data;
using StaffManagementApp.Domain.Entities;
using System.ComponentModel.Design;

namespace StaffManagementApp.ApplicationCores.DomainServices
{
    public interface IStaffRepository
    {
        Task<ReturnRequestModel<List<DisplayStaffDTO>>> GetAllStaffInformationAsync();
        Task<ReturnRequestModel<DisplayStaffDTO>> GetStaffInformationAsync(string Id);
        Task<ReturnRequestModel<DisplayStaffDTO>> CreateStaffInformationAsync(CreateStaffDTO model);
        Task<ReturnRequestModel<DisplayStaffDTO>> EditStaffInformationAsync(string Id,EditStaffDTO model);
        Task<ReturnRequestModel<DisplayStaffDTO>> DeleteStaffInformationAsync(string Id);
        Task<ReturnRequestModel<List<DisplayStaffDTO>>> SearchStaffInformationAsync(SearchStaffDTO model);
        Task<ReturnRequestModel<byte[]>> GetSearchStaffAsFileAsync(SearchStaffDTO searchModel, ReportRenderType outputFileType);
    }

    public class StaffRepository : IStaffRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public StaffRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public async Task<ReturnRequestModel<List<DisplayStaffDTO>>> GetAllStaffInformationAsync()
        {
            var listGender = Enum<AppValueObjects.StaffGender>.GetEnumList();

            var result = new ReturnRequestModel<List<DisplayStaffDTO>>();
            result.IsDidProcess = true;

            result.Data = (from staffs in await _dbContext.Staffs.OrderBy(p => p.Id).ToListAsync()
                    join gender in listGender on staffs.Gender equals gender.Key
                    select (new DisplayStaffDTO
                    {
                        StaffId = staffs.StaffId,
                        Birthday = staffs.Birthday,
                        FullName = staffs.FullName,
                        Gender = staffs.Gender,
                        GenderText = gender.Value
                    })).ToList();
            return result;
        }
        public async Task<ReturnRequestModel<DisplayStaffDTO>> GetStaffInformationAsync(string Id)
        {
            var listGender = Enum<AppValueObjects.StaffGender>.GetEnumList();

            var result = new ReturnRequestModel<DisplayStaffDTO>();
            var staffInfo = _mapper.Map<Staff, DisplayStaffDTO>(await _dbContext.Staffs.FirstOrDefaultAsync(p => p.StaffId == Id));

            //Not found staff by Id
            if (staffInfo == null)
            {
                return result;
            }

            staffInfo.GenderText = listGender.FirstOrDefault(p=>p.Key == staffInfo.Gender).Value;

            result.IsDidProcess = true;
            result.Data = staffInfo;
            return result;
        }
        public async Task<ReturnRequestModel<DisplayStaffDTO>> CreateStaffInformationAsync(CreateStaffDTO model)
        {
            var result = new ReturnRequestModel<DisplayStaffDTO>();

            //Check existing staff id before create
            if (IsExistingStaffId(model.StaffId, null))
            {
                return result;
            }

            var staffDb = _mapper.Map<Staff>(model);

            _dbContext.Staffs.Add(staffDb);
            await _dbContext.SaveChangesAsync();

            result.IsDidProcess = true;
            result.Data = _mapper.Map<DisplayStaffDTO>(staffDb);
            return result;
        }

        public async Task<ReturnRequestModel<DisplayStaffDTO>> EditStaffInformationAsync(string Id, EditStaffDTO model)
        {
            var result = new ReturnRequestModel<DisplayStaffDTO>();

            //Validation
            var staffDb = await _dbContext.Staffs.FirstOrDefaultAsync(p=>p.StaffId == Id);
            if (staffDb == null)
            {
                result.IsDidProcess = false;
                result.AddMessage("Not found staff");
                return result;
            }

            //Check existing staff id before create
            if (IsExistingStaffId(model.EditId, Id))
            {
                return result;
            }

            _mapper.Map(model, staffDb);
            
            _dbContext.Entry(staffDb).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            result.IsDidProcess = true;
            result.Data = _mapper.Map<DisplayStaffDTO>(staffDb);
            return result;
        }
        public async Task<ReturnRequestModel<DisplayStaffDTO>> DeleteStaffInformationAsync(string Id)
        {
            var result = new ReturnRequestModel<DisplayStaffDTO>();

            //Validation
            var staffDb = _dbContext.Staffs.FirstOrDefault(p => p.StaffId == Id);
            if (staffDb == null)
            {
                result.IsDidProcess = false;
                result.AddMessage("Not found staff");
                return result;
            }

            _dbContext.Remove(staffDb);
            await _dbContext.SaveChangesAsync();

            result.IsDidProcess = true;
            result.Data = _mapper.Map<DisplayStaffDTO>(staffDb);
            return result;
        }

        public async Task<ReturnRequestModel<List<DisplayStaffDTO>>> SearchStaffInformationAsync(SearchStaffDTO  model)
        {
            var listGender = Enum<AppValueObjects.StaffGender>.GetEnumList();
            var result = new ReturnRequestModel<List<DisplayStaffDTO>>();

            //Search Query

            var searchStaff = _dbContext.Staffs.AsNoTracking();

            if (!string.IsNullOrEmpty(model.StaffId))
            {
                searchStaff = searchStaff.Where(p => p.StaffId.Contains(model.StaffId.TrimNullable()));
            }
            if (model.Gender >= 1 && model.Gender <= 2)
            {
                searchStaff = searchStaff.Where(p => p.Gender == model.Gender);
            }
            if (model.FromDate.HasValue && model.ToDate.HasValue)
            {
                //Go 00:00:00
                var FromDate = model.FromDate.Value.MinDateTime();
                //Go 23:59:59
                var ToDate = model.ToDate.Value.MaxDateTime();

                searchStaff = searchStaff.Where(p => p.Birthday >= FromDate && p.Birthday <= ToDate);
            }

            

            //Result
            result.IsDidProcess = true;
            result.Data = (from staffs in await searchStaff.ToListAsync()
                           join gender in listGender on staffs.Gender equals gender.Key
                           select (new DisplayStaffDTO
                           {
                               StaffId = staffs.StaffId,
                               Birthday = staffs.Birthday,
                               FullName = staffs.FullName,
                               Gender = staffs.Gender,
                               GenderText = gender.Value
                           })).ToList();
            return result;
        }

        public async Task<ReturnRequestModel<byte[]>> GetSearchStaffAsFileAsync(SearchStaffDTO searchModel, ReportRenderType outputFileType)
        {
            var result = new ReturnRequestModel<byte[]>();
            try
            {
                
                LocalReportServices report = new LocalReportServices("StaffAdvancedSearchRpt");

                var searchResult = await this.SearchStaffInformationAsync(searchModel);
                report.AddDataSource("Header", new List<SearchStaffDTO> { searchModel });
                report.AddDataSource("Lines", searchResult.Data);

                result.IsDidProcess = true;
                result.Data = report.GenerateReport(outputFileType);

                return result;
            }
            catch(Exception ex)
            {
                result.IsDidProcess = false;
                result.AddMessage(ex.Message);
                return result;
            }
        }

        private bool IsExistingStaffId(string StaffId, string IgnoreStaffId)
        {
            //Filter by staff Id and Ignore another staff Id
            if (!string.IsNullOrEmpty(IgnoreStaffId))
            {
                var staff = _dbContext.Staffs.FirstOrDefault(p => p.StaffId == StaffId && p.StaffId != IgnoreStaffId);
                return (staff != null) ? true : false;
            }
            else
            {
                var staff = _dbContext.Staffs.FirstOrDefault(p => p.StaffId == StaffId);
                return (staff != null) ? true : false;
            }
        }
    }
}
