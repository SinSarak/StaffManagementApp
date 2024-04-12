using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using StaffManagementApp.ApplicationCores.DomainServices;
using StaffManagementApp.ApplicationCores.Models.DTOModels;
using StaffManagementApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagementApp.UnitTests.Repository
{
    public class StaffRepositoryUnitTests
    {
        private async Task<ApplicationDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();

            var rand = new Random();
            if (await databaseContext.Staffs.CountAsync() <= 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.Staffs.Add(
                        new Domain.Entities.Staff()
                        {
                            Id = 0,
                            StaffId = $"ID{i}",
                            FullName = $"NAME{i}",
                            Birthday = new DateTime(2000, 1, 1),
                            Gender = rand.Next(1, 2)
                        }
                        );
                    await databaseContext.SaveChangesAsync();
                }

                //Seed data for advanced search
                databaseContext.Staffs.Add(
                        new Domain.Entities.Staff()
                        {
                            Id = 0,
                            StaffId = $"ID10",
                            FullName = $"NAME10",
                            Birthday = DateTime.Now,
                            Gender = 1
                        }
                        );
                await databaseContext.SaveChangesAsync();
            }
            return databaseContext;
        }
        private readonly IMapper _mapper;

        public StaffRepositoryUnitTests()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new StaffManagementApp.Infrastructure.AutoMapperConfigureProfiles());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            _mapper = mapper;
        }

        [Fact]
        public async void StaffRepository_GetStaffInformation_ReturnStaff()
        {
            //Arrange
            var id = "ID1";
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.GetStaffInformationAsync(id);

            //Assert
            result.IsDidProcess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<DisplayStaffDTO>();
        }
        [Fact]
        public async void StaffRepository_GetStaffInformation_ReturnNullStaff()
        {
            //Arrange
            var id = "ID99";
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.GetStaffInformationAsync(id);

            //Assert
            result.IsDidProcess.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        [Fact]
        public async void StaffRepository_GetAllStaffInformation_ReturnListStaff()
        {
            //Arrange
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.GetAllStaffInformationAsync();

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async void StaffRepository_CreateStaffInformation_ReturnCreatedStaff()
        {
            //Arrange
            var createStaffModel = new CreateStaffDTO { StaffId = "ID11", FullName = "NAME11", Birthday = DateTime.Now, Gender = 1 };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.CreateStaffInformationAsync(createStaffModel);

            //Assert
            result.IsDidProcess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<DisplayStaffDTO>();
        }

        [Fact]
        public async void StaffRepository_EditStaffInformation_ReturnEditedStaff()
        {
            //Arrange
            var editStaffId = "ID2";
            var editStaffModel = new EditStaffDTO { EditId = "ID12", FullName = "NAME12", Birthday = DateTime.Now, Gender = 2 };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.EditStaffInformationAsync(editStaffId, editStaffModel);

            //Assert
            result.IsDidProcess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<DisplayStaffDTO>();
            result.Data.StaffId.Should().NotBeSameAs(editStaffId);
        }
        [Fact]
        public async void StaffRepository_EditStaffInformation_ReturnNullStaff()
        {
            //Arrange
            var editStaffId = "ID99";
            var editStaffModel = new EditStaffDTO { EditId = "ID12", FullName = "NAME12", Birthday = DateTime.Now, Gender = 2 };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.EditStaffInformationAsync(editStaffId, editStaffModel);

            //Assert
            result.IsDidProcess.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        [Fact]
        public async void StaffRepository_DeleteStaffInformation_ReturnDeletedStaff()
        {
            //Arrange
            var deleteStaffId = "ID3";
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.DeleteStaffInformationAsync(deleteStaffId);

            //Assert
            result.IsDidProcess.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.Should().BeOfType<DisplayStaffDTO>();
        }
        [Fact]
        public async void StaffRepository_DeleteStaffInformation_ReturnNullStaff()
        {
            //Arrange
            var deleteStaffId = "ID99";
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.DeleteStaffInformationAsync(deleteStaffId);

            //Assert
            result.IsDidProcess.Should().BeFalse();
            result.Data.Should().BeNull();
        }

        [Fact]
        public async void StaffRepository_SearchStaffInformation_ReturnListStaffByStaffId()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { StaffId = "ID10" };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.SearchStaffInformationAsync(searchModel);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }
        [Fact]
        public async void StaffRepository_SearchStaffInformation_ReturnListStaffByGender()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { Gender = 1 };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.SearchStaffInformationAsync(searchModel);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }
        [Fact]
        public async void StaffRepository_SearchStaffInformation_ReturnListStaffByBirthday()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { FromDate = DateTime.Now, ToDate = DateTime.Now };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.SearchStaffInformationAsync(searchModel);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }
        [Fact]
        public async void StaffRepository_SearchStaffInformation_ReturnListStaffByStaffIdAndGender()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { StaffId = "ID10", Gender = 1 };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.SearchStaffInformationAsync(searchModel);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }
        [Fact]
        public async void StaffRepository_SearchStaffInformation_ReturnListStaffByStaffIdAndBirthday()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { StaffId = "ID10", FromDate = DateTime.Now, ToDate = DateTime.Now };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.SearchStaffInformationAsync(searchModel);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }
        [Fact]
        public async void StaffRepository_SearchStaffInformation_ReturnListStaffByStaffIdAndGenderAndBirthday()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { StaffId = "ID10", Gender = 1, FromDate = DateTime.Now, ToDate = DateTime.Now };
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.SearchStaffInformationAsync(searchModel);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(1);
        }

        [Fact]
        public async void StaffRepository_GetSearchStaffAsFile_ReturnExcelByteArrayByStaffId()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { StaffId = "ID10" };
            var fileType = ReportRenderType.Excel;
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.GetSearchStaffAsFileAsync(searchModel, fileType);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(100);
        }
        [Fact]
        public async void StaffRepository_GetSearchStaffAsFile_ReturnPDFByteArrayByStaffId()
        {
            //Arrange
            var searchModel = new SearchStaffDTO() { StaffId = "ID10" };
            var fileType = ReportRenderType.Pdf;
            var dbContext = await GetDatabaseContext();
            var staffRepository = new StaffRepository(dbContext, _mapper);

            //Act
            var result = await staffRepository.GetSearchStaffAsFileAsync(searchModel, fileType);

            //Assert
            result.Data.Should().NotBeNull();
            result.Data.Should().HaveCountGreaterThanOrEqualTo(100);
        }

    }
}
