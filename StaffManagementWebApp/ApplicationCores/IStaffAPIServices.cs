using AutoMapper;
using System.Net.Http.Headers;
using System.Net;
using System.Reflection;
using Newtonsoft.Json;
using StaffManagementWebApp.ViewModels;
using static StaffManagementWebApp.ApplicationCores.AppValueObjects;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using StaffManagementWebApp.ApplicationCores.Extensions;
using System.Text;
using Microsoft.AspNetCore.Http;
namespace StaffManagementWebApp.ApplicationCores
{
    public interface IStaffAPIServices
    {
        Task<ReturnRequestModel<List<DisplayStaffViewModel>>> GetAllStaffInformationAsync(CancellationToken cancellationToken);
        Task<ReturnRequestModel<DisplayStaffViewModel>> CreateStaffInformationAsync(CreateStaffModel model, CancellationToken cancellationToken);
        Task<ReturnRequestModel<DisplayStaffViewModel>> EditStaffInformationAsync(string Id, EditStaffModel model, CancellationToken cancellationToken);
        Task<ReturnRequestModel<DisplayStaffViewModel>> DeleteStaffInformationAsync(string Id, CancellationToken cancellationToken);
        Task<ReturnRequestModel<DisplayStaffViewModel>> GetStaffInformationAsync(string Id, CancellationToken cancellationToken);
        Task<ReturnRequestModel<List<DisplayStaffViewModel>>> SearchStaffInformationAsync(SearchStaffModel model, CancellationToken cancellationToken);
        Task<ReturnRequestModel<byte[]>> ExportStaffInformationAsExcelAsync(SearchStaffModel model, CancellationToken cancellationToken);
        Task<ReturnRequestModel<byte[]>> ExportStaffInformationAsPDFAsync(SearchStaffModel model, CancellationToken cancellationToken);
    }

    public class StaffAPIServices : IStaffAPIServices
    {
        private readonly IConfiguration _configuration;
        private IMapper _mapper { get; }

        private const string _EndPoint_Staff = "/api/staff";
        private const string _EndPoint_Search = "/api/staff/AdvancedSearch";
        private const string _EndPoint_Export_Excel = "/api/staff/ExportSearchExcel";
        private const string _EndPoint_Export_PDF = "/api/staff/ExportSearchPdf";


        public StaffAPIServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ReturnRequestModel<List<DisplayStaffViewModel>>> GetAllStaffInformationAsync(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Staff, null, SendRequestMethod.GET, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<List<DisplayStaffViewModel>>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                    
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<List<DisplayStaffViewModel>>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<DisplayStaffViewModel>> GetStaffInformationAsync(string Id, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Staff+"/"+ Id, null, SendRequestMethod.GET, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<DisplayStaffViewModel>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                    
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<DisplayStaffViewModel>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<DisplayStaffViewModel>> CreateStaffInformationAsync(CreateStaffModel model, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Staff, model, SendRequestMethod.POST, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<DisplayStaffViewModel>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                    
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<DisplayStaffViewModel>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<DisplayStaffViewModel>> EditStaffInformationAsync(string Id, EditStaffModel model, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Staff + "/" + Id, model, SendRequestMethod.PUT, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<DisplayStaffViewModel>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                    
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<DisplayStaffViewModel>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<DisplayStaffViewModel>> DeleteStaffInformationAsync(string Id, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Staff + "/" + Id, null, SendRequestMethod.DELETE, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<DisplayStaffViewModel>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<DisplayStaffViewModel>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<List<DisplayStaffViewModel>>> SearchStaffInformationAsync(SearchStaffModel model, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Search, model, SendRequestMethod.POST, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<List<DisplayStaffViewModel>>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                    
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<List<DisplayStaffViewModel>>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<byte[]>> ExportStaffInformationAsExcelAsync(SearchStaffModel model, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Export_Excel, model, SendRequestMethod.POST, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<byte[]>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<byte[]>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ReturnRequestModel<byte[]>> ExportStaffInformationAsPDFAsync(SearchStaffModel model, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await CallAPIServiceAsync(_EndPoint_Export_PDF, model, SendRequestMethod.POST, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<ReturnRequestModel<byte[]>>(await response.Content.ReadAsStringAsync());
                    result.ReturnRequestCode = (int)response.StatusCode;
                    result.IsSuccessStatusCode = response.IsSuccessStatusCode;
                    return result;
                    
                }

                //Fail
                else
                {
                    return ReturnDefaultHttpResponse<byte[]>(await response.Content.ReadAsStringAsync(), response.StatusCode);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private async Task<HttpResponseMessage> CallAPIServiceAsync(string APIEndPoint,object Content, SendRequestMethod Method, CancellationToken cancellationToken)
        {
            var EndPoint = $"{_configuration["API:Domain"]}{APIEndPoint}";

            HttpClientHandler httpClientHandler = new HttpClientHandler();

            using (var client = new HttpClient(httpClientHandler) { BaseAddress = new Uri(EndPoint) })
            {
                HttpResponseMessage response = new HttpResponseMessage();

                if (Method == SendRequestMethod.GET)
                {
                    return await client.GetAsync(EndPoint, cancellationToken);
                }
                else if (Method == SendRequestMethod.POST)
                {
                    var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                    var json = JsonConvert.SerializeObject(Content, settings).Replace("\\\"", "'");
                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    return await client.PostAsync(EndPoint, body, cancellationToken);
                }
                else if (Method == SendRequestMethod.PUT)
                {
                    var settings = new JsonSerializerSettings() { ContractResolver = new NullToEmptyStringResolver() };
                    var json = JsonConvert.SerializeObject(Content, settings).Replace("\\\"", "'");
                    var body = new StringContent(json, Encoding.UTF8, "application/json");
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    return await client.PutAsync(EndPoint, body, cancellationToken);
                }
                else if (Method == SendRequestMethod.DELETE)
                {
                    return await client.DeleteAsync(EndPoint, cancellationToken);
                }
                else
                {
                    throw new Exception("Hex: 77 68 61 74 20 74 68 65 20 68 65 6C 6C 20 61 72 65 20 79 6F 75 20 64 6F 69 6E 67");
                }
            }
            
        }
        private ReturnRequestModel<T> ReturnDefaultHttpResponse<T>(string response, HttpStatusCode StatusCode)
        {
            var result = new ReturnRequestModel<T>();
            result.ReturnRequestCode = (int)StatusCode;
            result.ReturnMessages.Add(response.Length > 500 ? response.Substring(0, 500) : response);
            return result;
        }
    }
}
