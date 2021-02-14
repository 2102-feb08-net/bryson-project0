using System;

namespace StoreApp.Library
{
    public record Customer : ICustomer
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
        }
    }
}
