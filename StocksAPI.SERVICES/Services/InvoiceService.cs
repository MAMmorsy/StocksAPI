using AutoMapper;
using StocksAPI.CORE.Enums;
using StocksAPI.CORE.Interfaces.Repositories;
using StocksAPI.CORE.Interfaces.Services;
using StocksAPI.CORE.Models.DTOs;
using StocksAPI.CORE.Models.Entities;
using StocksAPI.SERVICES.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace StocksAPI.SERVICES.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IContextStockRepository<Invoice> _invoiceRepository;
        private readonly IContextStockRepository<InvoiceItem> _itemRepository;
        private readonly IStockUnitOfWork _unitOfWork;
        public IMapper _mapper;

        public InvoiceService(IMapper mapper, IStockUnitOfWork unitOfWork,
            IContextStockRepository<Invoice> invoiceRepository,
            IContextStockRepository<InvoiceItem> itemRepository)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _invoiceRepository = invoiceRepository;
            _itemRepository = itemRepository;
        }
        public async Task<Response<bool>> CreateInvoice(InvoiceCreateDTO createDTO)
        {
            Response<bool> response = new Response<bool>();
            try
            {
                response.Errors = ValidatorHandler.Validate(createDTO, (InvoiceCreateValidator)Activator.CreateInstance(typeof(InvoiceCreateValidator)));
                if (response.Errors.Any())
                {
                    response.ResponseCode = (int)ResponseCodesEnum.InvalidParameters;
                    response.IsSucceded = false;
                    return response;
                }
                User user = _mapper.Map<User>(createDTO);
                Expression<Func<User, bool>> condition = e => e.UserName == createDTO.UserName && e.Deleted==false;
                // Check if user name existing before
                User? userData = await _userRepository.FindAsync(condition);
                if (userData == null)
                {
                    using (var transaction = _unitOfWork.BeginTransaction())
                    {
                        try
                        {
                            // Insert new Details
                            user.Datein = DateTime.Now;
                            user.Deleted = false;
                            await _userRepository.AddAsync(user);
                            response.Data = _unitOfWork.SaveChanges() >= 1;
                            response.Errors?.Add(new Error() { ErrorMessage = "User created." });
                            response.ResponseCode = (int)ResponseCodesEnum.SaveAllRecords;
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            response.ResponseCode = (int)ResponseCodesEnum.DbException;
                            response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                            response.IsSucceded = false;
                        }
                    }
                }
                else
                {
                    response.ResponseCode = (int)ResponseCodesEnum.ExistingData;
                    response.Errors?.Add(new Error() { ErrorMessage = "User Name already exists." });
                    response.IsSucceded = false;
                }
            }
            catch (Exception ex)
            {
                response.ResponseCode = (int)ResponseCodesEnum.DbException;
                response.Errors?.Add(new Error() { ErrorMessage = ex.Message });
                response.IsSucceded = false;
            }
            return response;
        }
    }
}
