using System;

namespace StoreApp.Library.Model
{
    public record Customer : ICustomer
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public int Id { get; init; }

        public override string ToString() => $"{FirstName} {LastName}";

        public Customer(string firstName, string lastName, int id)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentException("A customer must have a first name that is not null or empty.");

            FirstName = firstName ?? throw new NullReferenceException();
            LastName = lastName;

            Id = id;
        }

        public Customer() { }
    }
}