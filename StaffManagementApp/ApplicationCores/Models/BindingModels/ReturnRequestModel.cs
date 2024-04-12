namespace StaffManagementApp.ApplicationCores.Models.BindingModels
{
    public class ReturnRequestModel<T>
    {
        public bool IsDidProcess { get; set; }
        public string ReturnCommand { get; set; }
        public List<string> ReturnMessages = new List<string>();
        public T Data { get; set; }


        public ReturnRequestModel()
        {
        }
        public ReturnRequestModel(List<string> message)
        {
            this.ReturnMessages = message;
        }
        public ReturnRequestModel( bool IsDidProcess, List<string> Message)
        {
            this.IsDidProcess = IsDidProcess;
            this.ReturnMessages = Message;
        }
        public ReturnRequestModel(bool IsDidProcess, List<string> Message, T Data = default(T))
        {
            this.IsDidProcess = IsDidProcess;
            this.ReturnMessages = Message;
            this.Data = Data;
        }

        public void AddMessage(string message)
        {
            this.ReturnMessages.Add(message);
        }

        public void ClearMessage()
        {
            this.ReturnMessages.Clear();
        }
        public bool HasMessage()
        {
            return (this.ReturnMessages.Count <= 0) ? false : true;
        }
    }
}
