using StoreApp.Library.Model;
using System.Text;

namespace StoreApp.DataAccess
{
    public partial class Address
    {
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Address1);

            if (Address2 != null)
                sb.AppendLine(Address2);
            sb.AppendLine($"{City}, {State} {ZipCode}");
            sb.AppendLine(Country);
            return sb.ToString();
        }
    }
}