using CustomerAutomationWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CustomerAutomationWeb.Services
{
    public interface ICustomerService
    {
        public bool Create(CustomerAddInput cusAddDto);
        public CustomerViewModel Get(int id);
        public List<CustomerViewModel> GetAll();
        public bool Delete(int id);
        public List<CustomerViewModel> Search(CustomerFilterInput cusFilDto);

    }
}
