using CustomerAutomation.Common;
using CustomerAutomation.DTOs;
using System.Collections.Generic;

namespace CustomerAutomation.Services
{
    public interface ICustomerService
    {
        public ResponseMessages<NoContent> Create(CustomerAddDto cusAddDto);
        public ResponseMessages<CustomerGetDto> Get(int id);
        public ResponseMessages<List<CustomerGetDto>> GetAll();
        public ResponseMessages<NoContent> Delete(CustomerDeleteDto cusDelDto);
        public ResponseMessages<List<CustomerGetDto>> Search(CustomerFilterDto cusFilDto);


    }
}
