using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoreApp.Library;


namespace StoreApp.IO.Terminal
{
    public class OrderMenu : Menu
    {
        private IOrder _currentOrder;

        MainDatabase _database;

        public OrderMenu(IIOController io, Menu previousMenu, MainDatabase database) : base(io, previousMenu) 
        {
            _database = database;
        }

        bool buildingOrder = false;

        public override void Open()
        {
            buildingOrder = true;
            CreateOrder();
        }

        private void CreateOrder()
        {
            if(!TryAddCustomerToNewOrder(out Customer customer))
                return;

            if (!TryAddLocationToOrder(out Location location))
                return;

            _currentOrder = new Order(customer, location);

            while (buildingOrder)
            {
                ResponseChoice response = new ResponseChoice(_io);
                response.Options.Add(new ChoiceOption("Add Product", AddNewProductToOrder));
                response.Options.Add(new ChoiceOption("Check Order Status", CheckStatus));
                response.Options.Add(new ChoiceOption("Cancel Order", CancelOrder));
                response.Options.Add(new ChoiceOption("Submit Order", TrySubmitOrder));
                response.ShowAndInvokeOptions();
            }
        }

        private bool TryAddCustomerToNewOrder(out Customer customer)
        {
            bool tryAgain;

            do
            {
                tryAgain = false;

                _io.Output.Write("Please select a customer to order as.");

                customer = CustomerMenuHelper.LookUpCustomer(_io, _database.CustomerDatabase);

                if (customer == null)
                    TryAgain(() => tryAgain = true);

            } while (tryAgain);

            return customer != null;
        }

        private bool TryAddLocationToOrder(out Location location)
        {
            bool tryAgain;

            do
            {
                tryAgain = false;

                location = LocationMenuHelper.LookUpLocation(_io, _database.LocationDatabase);

                if(location == null)
                    TryAgain(() => tryAgain = true);

            } while (tryAgain);

            return location != null;
        }

        private void TryAgain(Action tryAgain)
        {
            ResponseChoice response = new ResponseChoice(_io);
            response.Options.Add(new ChoiceOption("Try again with different values", tryAgain));
            response.Options.Add(new ChoiceOption("Cancel order", null));
            response.ShowAndInvokeOptions();
        }

        private void AddNewProductToOrder()
        {
            _io.Output.Write("Enter the name of the product you wish to add to the order:");
            string productName = _io.Input.ReadInput();
        }

        private void CancelOrder()
        {
            buildingOrder = false;
            _io.Output.Write("Order canceled.");
        }

        private void CheckStatus()
        {
            OrderDisplayer displayer = new OrderDisplayer();
            string display = displayer.GetOrderDisplay(_currentOrder);
            _io.Output.Write(display);
        }

        public void TrySubmitOrder()
        {
            _currentOrder.ProcessOrder();
            buildingOrder = false;
        }
    }
}
