using Microsoft.Reporting.NETCore;
using System.Reflection;
using System.Text;

namespace StaffManagementApp.ApplicationCores.DomainServices
{
    public enum ReportRenderType
    {
        Word,
        WordOpenXml,
        Excel,
        ExcelOpenXml,
        Pdf,
        Image,
        Html,
        Rpl,
    }
    public class LocalReportServices
    {
        private LocalReport _Report;

        public LocalReportServices(string ReportName)
        {
            try
            {
                string fileDirPath = Assembly.GetExecutingAssembly().Location.Replace("StaffManagementApp.dll", string.Empty);
                string rdlcFilePath = string.Format("{0}Reports\\{1}.rdlc", fileDirPath, ReportName);

                Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                Encoding.GetEncoding("windows-1252");

                _Report = new LocalReport();
                _Report.ReportPath = rdlcFilePath;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddDataSource(string ReportSourceName, object DataSource)
        {
            _Report.DataSources.Add(new ReportDataSource(ReportSourceName, DataSource));
        }


        public void AddParameter(Dictionary<string, string> Parameters)
        {
            List<ReportParameter> parameters = new List<ReportParameter>();
            foreach (var param in Parameters)
            {
                parameters.Add(new ReportParameter(param.Key, param.Value));
            }
            _Report.SetParameters(parameters);
        }

        public byte[] GenerateReport(ReportRenderType FileType)
        {
            return _Report.Render(FileType.ToString().ToUpper());
        }
    }
}
