using System;

namespace StoreApp.Library
{
    public record Customer : ICustomer
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }


        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public Customer(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("A customer must have a first name that is not null or empty.");

            FirstName = firstName ?? throw new NullReferenceException();
            LastName = lastName ?? throw new NullReferenceException();
        }

        public Customer() { }
    }
}
