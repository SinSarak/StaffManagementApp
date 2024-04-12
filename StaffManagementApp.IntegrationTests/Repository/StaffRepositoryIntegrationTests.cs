using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using StaffManagementApp.ApplicationCores.Models.BindingModels;
using StaffManagementApp.ApplicationCores.Models.DTOModels;
using StaffManagementApp.Data;
using StaffManagementApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

namespace StaffManagementApp.IntegrationTests.Repository
{
    public class StaffRepositoryIntegrationTests
    {
        private const string _EndPoint_Staff = "/api/staff";
        private const string _EndPoint_Search = "/api/staff/AdvancedSearch";
        private const string _EndPoint_Export_Excel = "/api/staff/ExportSearchExcel";
        private const string _EndPoint_Export_PDF = "/api/staff/ExportSearchPdf";

        [Theory]
        [InlineData($"{_EndPoint_Staff}/KeepLive")]
        public async Task Get_KeepLive_200_If_successful(string url)
        {
            // Arrange
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299

            response.IsSuccessStatusCode.Should().BeTrue();
        }

        [Fact]
        public async void Get_all_Staffs_If_Successful()
        {
            //Arrange
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.GetAsync(_EndPoint_Staff);

            //Assert
            response.EnsureSuccessStatusCode();

            var staffResponse = await response.Content.ReadFromJsonAsync<ReturnRequestModel<List<DisplayStaffDTO>>>();
            staffResponse?.IsDidProcess.Should().BeTrue();
            staffResponse?.Data.Should().HaveCountGreaterThanOrEqualTo(1);
            staffResponse?.Data.Should().BeOfType<List<DisplayStaffDTO>>();
        }

        [Fact]
        public async void Get_one_staff_If_successful()
        {
            //Arrange
            var staffId = "ID1";
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.GetAsync(_EndPoint_Staff + "/" + staffId);

            //Assert
            response.EnsureSuccessStatusCode();

            var staffResponse = await response.Content.ReadFromJsonAsync<ReturnRequestModel<DisplayStaffDTO>>();
            staffResponse?.IsDidProcess.Should().BeTrue();
            staffResponse?.Data.Should().NotBeNull();
            staffResponse?.Data.Should().BeOfType<DisplayStaffDTO>();
        }

        [Fact]
        public async void Get_returns_400_If_empty_staffId()
        {
            //Arrange
            var staffId = " ";
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.GetAsync(_EndPoint_Staff + "/" + staffId);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Get_returns_404_If_not_found_staffId()
        {
            //Arrange
            var staffId = "IDD99";
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.GetAsync(_EndPoint_Staff + "/" + staffId);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Post_staff_created_If_successful()
        {
            //Arrange
            var createStaffDTO = new CreateStaffDTO() { StaffId = "ID12", FullName = "NAME12", Gender = 1 , Birthday = DateTime.Now};
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PostAsJsonAsync(_EndPoint_Staff, createStaffDTO);

            //Assert
            response.EnsureSuccessStatusCode();

            var staffResponse = await response.Content.ReadFromJsonAsync<ReturnRequestModel<DisplayStaffDTO>>();
            staffResponse?.IsDidProcess.Should().BeTrue();
            staffResponse?.Data.Should().NotBeNull();
            staffResponse?.Data.Should().BeOfType<DisplayStaffDTO>();
        }

        [Fact]
        public async void Post_returns_400_If_not_valid()
        {
            //Arrange
            var createStaffDTO = new CreateStaffDTO() { StaffId = "ID12" };
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PostAsJsonAsync(_EndPoint_Staff, createStaffDTO);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Post_returns_404_If_not_found()
        {
            //Arrange
            var createStaffDTO = new CreateStaffDTO() { StaffId = "ID2", FullName = "NAME2", Gender = 1, Birthday = DateTime.Now };
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PostAsJsonAsync(_EndPoint_Staff, createStaffDTO);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Put_staff_edited_If_successful()
        {
            //Arrange
            var editId = "ID2";
            var editStaffDTO = new EditStaffDTO() { EditId = "ID2", FullName = "NAME22", Gender = 1, Birthday = DateTime.Now };
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PutAsJsonAsync(_EndPoint_Staff + "/" + editId, editStaffDTO);

            //Assert
            response.EnsureSuccessStatusCode();

            var staffResponse = await response.Content.ReadFromJsonAsync<ReturnRequestModel<DisplayStaffDTO>>();
            staffResponse?.IsDidProcess.Should().BeTrue();
            staffResponse?.Data.Should().NotBeNull();
            staffResponse?.Data.Should().BeOfType<DisplayStaffDTO>();
        }

        [Fact]
        public async void Put_returns_400_If_not_valid()
        {
            //Arrange
            var editId = "ID99";
            var editStaffDTO = new EditStaffDTO() { EditId = "ID2" };
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PutAsJsonAsync(_EndPoint_Staff + "/" + editId, editStaffDTO);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async void Put_returns_404_If_not_found_staffId()
        {
            //Arrange
            var editId = "ID99";
            var editStaffDTO = new EditStaffDTO() { EditId = "ID2", FullName = "NAME22", Gender = 1, Birthday = DateTime.Now };
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PutAsJsonAsync(_EndPoint_Staff + "/" + editId, editStaffDTO);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Delete_staff_deleted_If_successful()
        {
            //Arrange
            var deleteId = "ID3";
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.DeleteAsync(_EndPoint_Staff + "/" + deleteId);

            //Assert
            response.EnsureSuccessStatusCode();

            var staffResponse = await response.Content.ReadFromJsonAsync<ReturnRequestModel<DisplayStaffDTO>>();
            staffResponse?.IsDidProcess.Should().BeTrue();
            staffResponse?.Data.Should().NotBeNull();
            staffResponse?.Data.Should().BeOfType<DisplayStaffDTO>();
        }

        [Fact]
        public async void Delete_returns_404_If_not_found_staffId()
        {
            //Arrange
            var deleteId = "ID89";
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.DeleteAsync(_EndPoint_Staff + "/" + deleteId);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void Post_search_staffs_If_Successful()
        {
            //Arrange
            var searchStaffDTO = new SearchStaffDTO() { StaffId = "ID10", Gender = 1, FromDate = DateTime.Now, ToDate = DateTime.Now };
            var application = new StaffWebApplicationFactory();
            var client = application.CreateClient();

            //Act
            var response = await client.PostAsJsonAsync(_EndPoint_Search, searchStaffDTO);

            //Assert
            response.EnsureSuccessStatusCode();

            var staffResponse = await response.Content.ReadFromJsonAsync<ReturnRequestModel<List<DisplayStaffDTO>>>();
            staffResponse?.IsDidProcess.Should().BeTrue();
            staffResponse?.Data.Should().HaveCountGreaterThanOrEqualTo(1);
            staffResponse?.Data.Should().BeOfType<List<DisplayStaffDTO>>();
        }

        
    }
}
