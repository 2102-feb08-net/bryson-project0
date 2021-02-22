using System;
using System.Collections.Generic;
using System.Text;

namespace StoreApp.Library.Model
{
    public interface ICustomer : IIdentifiable
    {
        string FirstName { get; }

        string LastName { get; }

        public string DisplayName() => $"{FirstName} {LastName}";
    }
}