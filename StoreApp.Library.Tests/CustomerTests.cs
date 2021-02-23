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
        public void Customer_WhiteSpaceFirstName_Fail()
        {
            // arrange

            // act
            static Customer CreateCustomer() => new Customer("   ", "Doe", 1);

            // assert
            Assert.Throws<ArgumentException>(CreateCustomer);
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
            static Customer CreateCustomer() => new Customer(null, "Doe", 1);

            // assert
            Assert.Throws<ArgumentNullException>(CreateCustomer);
        }
    }
}