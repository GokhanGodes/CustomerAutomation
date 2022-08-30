using CustomerAutomation.Common;
using CustomerAutomation.Data;
using CustomerAutomation.DTOs;
using CustomerAutomation.Mapping;
using CustomerAutomation.Models;
using KPS;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CustomerAutomation.Services
{
    public class CustomerService : ICustomerService
    {
        public readonly AppDbContext _context;
        public CustomerService(AppDbContext context)
        {
            _context = context;
        }
        public ResponseMessages<NoContent> Create(CustomerAddDto cusAddDto)
        {

            if(!IdentityVerify(cusAddDto))
            {
                return ResponseMessages<NoContent>.Fail("Girilen Bilgilerin Doğruluğunu Kontrol Ediniz.",400);
            }
            var cus = _context.Set<Customer>().Where(x => x.TCKN == cusAddDto.TCKN).AsNoTracking().FirstOrDefault();
            var entity = ObjectMapper.Mapper.Map<Customer>(cusAddDto);
            if (cus != null)
            {
                entity.Id = cus.Id;
                _context.Set<Customer>().Update(entity);
            }
            else
            {
                _context.Set<Customer>().AddAsync(entity);
            }
            _context.SaveChangesAsync();
            return ResponseMessages<NoContent>.SuccessNoContent();
        }

        public ResponseMessages<NoContent> Delete(CustomerDeleteDto cusDelDto)
        {
            var entity = _context.Set<Customer>().FirstOrDefault(x => x.Id == cusDelDto.Id && x.IsActive);
            if (entity != null)
            {
                entity.IsActive = false;
                _context.Set<Customer>().Update(entity);
                _context.SaveChangesAsync();
                return ResponseMessages<NoContent>.SuccessNoContent();

            }
            return ResponseMessages<NoContent>.Fail("Müşteri Bulunamadı.", 400);
        }

        public ResponseMessages<CustomerGetDto> Get(int id)
        {
            var entity = _context.Set<Customer>().FirstOrDefault(x => x.Id == id && x.IsActive);
            if (entity != null)
            {
                var dto = ObjectMapper.Mapper.Map<CustomerGetDto>(entity);
                return ResponseMessages<CustomerGetDto>.Success(dto, 200);
            }
            return ResponseMessages<CustomerGetDto>.Fail("Müşteri Bulunamadı.", 400);
        }
        public ResponseMessages<List<CustomerGetDto>> GetAll()
        {
            var entities = _context.Set<Customer>().Where(x => x.IsActive).ToList();
            if (!entities.Any())
            {
                return ResponseMessages<List<CustomerGetDto>>.Fail("Müşteriler Bulunamadı.", 400);

            }
            var dtos = ObjectMapper.Mapper.Map<List<CustomerGetDto>>(entities);
            return ResponseMessages<List<CustomerGetDto>>.Success(dtos, 200);

        }

        public ResponseMessages<List<CustomerGetDto>> Search(CustomerFilterDto cusFilDto)
        {

            var enities = _context.Set<Customer>()
                .Where(x => string.IsNullOrEmpty(cusFilDto.TCKN) || x.TCKN == cusFilDto.TCKN)
                .Where(x => string.IsNullOrEmpty(cusFilDto.Name) || x.Name == cusFilDto.Name)
                .Where(x => string.IsNullOrEmpty(cusFilDto.LastName) || x.LastName == cusFilDto.LastName)
                .Where(x => x.IsActive)
                .ToList();

            var dtos = ObjectMapper.Mapper.Map<List<CustomerGetDto>>(enities);
            if (dtos.Any())
            {
                return ResponseMessages<List<CustomerGetDto>>.Success(dtos, 200);
            }
            return ResponseMessages<List<CustomerGetDto>>.Fail("Müşteri Bulunamadı.", 400);

        }
        private bool IdentityVerify(CustomerAddDto input)
        {
            KPSPublicSoapClient _client = new KPSPublicSoapClient(KPSPublicSoapClient.EndpointConfiguration.KPSPublicSoap12, "https://tckimlik.nvi.gov.tr/Service/KPSPublic.asmx");

            var response = _client.TCKimlikNoDogrulaAsync(Convert.ToInt64(input.TCKN), input.Name, input.LastName, input.BirthDate.Year).Result;
            return response.Body.TCKimlikNoDogrulaResult;
        }

    }
}
