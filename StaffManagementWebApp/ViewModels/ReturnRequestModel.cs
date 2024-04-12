namespace StaffManagementWebApp.ViewModels
{
    public class ReturnRequestModel<T>
    {
        public bool IsDidProcess { get; set; }
        public string ReturnCommand { get; set; }
        public bool IsSuccessStatusCode { get; set; }
        public int ReturnRequestCode { get; set; }
        public List<string> ReturnMessages = new List<string>();
        public T Data { get; set; }

        public ReturnRequestModel()
        {
            ReturnMessages = new List<string>();
        }
    }
}
