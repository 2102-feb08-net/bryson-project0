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

        CustomerDatabase _database;

        public OrderMenu(IIOController io, Menu previousMenu, CustomerDatabase database) : base(io, previousMenu) 
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

            _currentOrder = new Order(customer);

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

                customer = CustomerMenuHelper.LookUpCustomer(_io, _database);

                if (customer == null)
                {
                    ResponseChoice response = new ResponseChoice(_io, waitForEnterKeyAfterResponse: false);
                    response.Options.Add(new ChoiceOption("Try again with a different customer", () => tryAgain = true));
                    response.Options.Add(new ChoiceOption("Cancel order", null));
                    response.ShowAndInvokeOptions();
                }

            } while (tryAgain);

            return customer != null;
        }

        private void AddNewProductToOrder()
        {
            _io.Output.Write("Enter the name of the product you wish to add to the order:");
            string productName = _io.Input.ReadInput();
        }

        private void CancelOrder()
        {
            buildingOrder = false;
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
