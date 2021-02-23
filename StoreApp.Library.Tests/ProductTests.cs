using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library.Model;
using Xunit;

namespace StoreApp.Library.Tests
{
    public class ProductTests
    {
        [Fact]
        public void Product_Contructor_Pass()
        {
            // arrange

            // act
            Product product = new Product(name: "Apple", category: "Food", unitPrice: 0.79m, id: 1);

            // assert
            Assert.NotNull(product);
        }

        [Fact]
        public void Product_NullName_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: null, category: "Food", unitPrice: 0.79m, id: 1);

            // assert
            Assert.Throws<ArgumentNullException>(CreateProduct);
        }

        [Fact]
        public void Product_NullCategory_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: "Apple", category: null, unitPrice: 0.79m, id: 1);

            // assert
            Assert.Throws<ArgumentNullException>(CreateProduct);
        }

        [Fact]
        public void Product_WhiteSpaceName_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: "     ", category: "Food", unitPrice: 0.79m, id: 1);

            // assert
            Assert.Throws<ArgumentException>(CreateProduct);
        }

        [Fact]
        public void Product_WhiteSpaceCategory_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: "Apple", category: "    ", unitPrice: 0.79m, id: 1);

            // assert
            Assert.Throws<ArgumentException>(CreateProduct);
        }

        [Fact]
        public void Product_PriceLessThan0_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: "Apple", category: "Food", unitPrice: -4m, id: 1);

            // assert
            Assert.Throws<ArgumentException>(CreateProduct);
        }

        [Fact]
        public void Product_PriceEqual0_Pass()
        {
            // arrange

            // act
            Product product = new Product(name: "Apple", category: "Food", unitPrice: 0m, id: 1);

            // assert
            Assert.NotNull(product);
        }

        [Fact]
        public void Product_IdLessThan0_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: "Apple", category: "Food", unitPrice: 0.79m, id: -4);

            // assert
            Assert.Throws<ArgumentException>(CreateProduct);
        }

        [Fact]
        public void Product_IdEqual0_Fail()
        {
            // arrange

            // act
            static Product CreateProduct() => new Product(name: "Apple", category: "Food", unitPrice: 0.79m, id: 0);

            // assert
            Assert.Throws<ArgumentException>(CreateProduct);
        }
    }
}