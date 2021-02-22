using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;
using StoreApp.Library.Model;
using StoreApp.DataAccess.Repository;

namespace StoreApp.IO.Terminal
{
    public class OrderMenu : Menu
    {
        private IOrder _currentOrder;

        private readonly MainDatabase _database;

        public OrderMenu(IIOController io, Menu previousMenu, MainDatabase database) : base(io, previousMenu)
        {
            _database = database;
        }

        private bool buildingOrder = false;

        public override async Task Open()
        {
            buildingOrder = true;
            await CreateOrder();
        }

        private async Task CreateOrder()
        {
            Customer customer = await AttemptAddCustomerToNewOrder();
            if (customer is null)
                return;

            ILocation location = await AttemptAddLocationToOrder();

            if (location is null)
                return;

            _currentOrder = new Order(customer, location);

            while (buildingOrder)
            {
                ResponseChoice response = new ResponseChoice(_io);
                response.Options.Add(new ChoiceOption("Add Product", AddNewProductToOrder));
                response.Options.Add(new ChoiceOption("Check Order Status", CheckStatus));
                response.Options.Add(new ChoiceOption("Cancel Order", CancelOrder));
                response.Options.Add(new ChoiceOption("Submit Order", TrySubmitOrder));
                await response.ShowAndInvokeOptions();
            }
        }

        private async Task<Customer> AttemptAddCustomerToNewOrder()
        {
            Customer customer = null;
            bool tryAgain;

            do
            {
                tryAgain = false;

                _io.Output.Write("Please select a customer to order as:");

                customer = await CustomerMenuHelper.LookUpCustomer(_io, _database);

                if (customer is null)
                    await TryAgain(() => tryAgain = true);
            } while (tryAgain);

            return customer;
        }

        private async Task<ILocation> AttemptAddLocationToOrder()
        {
            ILocation location = null;
            bool tryAgain;

            do
            {
                tryAgain = false;

                location = await LocationMenuHelper.LookUpLocation(_io, _database);

                if (location is null)
                    await TryAgain(() => tryAgain = true);
            } while (tryAgain);

            return location;
        }

        private async Task TryAgain(Action tryAgain)
        {
            ResponseChoice response = new ResponseChoice(_io);
            response.Options.Add(new ChoiceOption("Try again with a different value", tryAgain));
            response.Options.Add(new ChoiceOption("Cancel order"));
            await response.ShowAndInvokeOptions();
        }

        private async Task AddNewProductToOrder()
        {
            IProduct product = await ProductMenuHelper.LookUpProductAsync(_io, _database);
            if (product is null)
                return;

            if (!ProductMenuHelper.TryEnterProductQuantity(_io, out int quantity))
                return;

            AttemptResult successAttempt = _currentOrder.TryAddProductToOrder(product, quantity);
            if (!successAttempt)
            {
                _io.Output.Write($"ERROR: {successAttempt.FailureMessage}");
                _io.Output.Write("No additional products were added to the order.");
                _io.PressEnterToContinue();
            }
        }

        private void CancelOrder()
        {
            buildingOrder = false;
            _io.Output.Write("Order canceled.");
        }

        private void CheckStatus()
        {
            string display = OrderDisplayer.GetOrderDisplay(_currentOrder);
            _io.Output.Write(display);
        }

        public async Task TrySubmitOrder()
        {
            IOrderRepository repo = _database.OrderRepository;

            try
            {
                await repo.SendOrderTransaction(_currentOrder);
                _io.Output.Write("Order submitted successfully!");
                buildingOrder = false;
            }
            catch (OrderException e)
            {
                _io.Output.Write($"Order Failed: {e.Message}");
            }
        }
    }
}