using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToyStore.DAL;
using ToyStore.Model;

namespace ToyStore.ViewModel
{
    class CustomerViewModel
    {
        private CustomerModel _customer { get; set; }
        private CustomerDAO _customerDAO;

        private ObservableCollection<CustomerModel> _customers { get; set; }

    }
}
