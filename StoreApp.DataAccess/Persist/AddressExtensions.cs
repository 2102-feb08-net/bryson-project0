using StoreApp.Library.Model;
using System.Text;

namespace StoreApp.DataAccess
{
    public partial class Address
    {
        /// <summary>
        /// Converts the address fields into a single multi-line formatted adddress.
        /// </summary>
        /// <returns>Returns the formatted address.</returns>
        public string Print()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Address1);

            if (Address2 is not null)
                sb.AppendLine(Address2);
            sb.AppendLine($"{City}, {State} {ZipCode}");
            sb.AppendLine(Country);
            return sb.ToString();
        }
    }
}