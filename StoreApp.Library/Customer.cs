using System;

namespace StoreApp.Library
{
    public class Customer : ICustomer
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public Guid ID { get; init; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public Customer(string firstName, string lastName, Guid guid)
        {
            FirstName = firstName ?? throw new NullReferenceException();
            LastName = lastName ?? throw new NullReferenceException();

            if (guid == Guid.Empty)
                throw new ArgumentException("GUID cannot be 0");
            
            ID = guid;
        }

        public Customer() { }
    }
}
