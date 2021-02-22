using System;
using Xunit;
using StoreApp.Library.Model;

namespace StoreApp.Library.Tests
{
    public class CustomerTests
    {
        [Fact]
        public void Customer_Constructor_Pass()
        {
            // arrange

            // act
            Customer customer = new Customer("John", "Doe", 1);

            // assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void Customer_NullLastName_Pass()
        {
            // arrange

            // act
            Customer customer = new Customer("John", null, 1);

            // assert
            Assert.NotNull(customer);
        }

        [Fact]
        public void Customer_NullFirstName_Fail()
        {
            // arrange

            // act
#pragma warning disable CA1806
            static void createCustomer() => new Customer(null, "Doe", 1);
#pragma warning restore CA1806

            // assert
            Assert.Throws<ArgumentNullException>(createCustomer);
        }
    }
}