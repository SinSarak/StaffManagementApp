namespace StaffManagementWebApp.Models
{
    public class IntStringPairObject
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public IntStringPairObject() { }
        public IntStringPairObject(int key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
