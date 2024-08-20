using AutoMapper;
using AutoMapper.QueryableExtensions;
using Net.payOS.Types;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels.PayOS;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.MoneyTransaction;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Package;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class PackageService : IPackageService
    {
        private readonly IMapper _mapper;
        private readonly IPackageRepository _repository;
        private readonly IPayOSService _payOSService;
        private readonly IMonetaryTransactionService _monetaryTransactionService;
        private readonly IMonetaryTransactionRepository _monetaryTransactionRepository;

        public PackageService(IPackageRepository repository, IMapper mapper, IPayOSService payOSService, IMonetaryTransactionService monetaryTransactionService, IMonetaryTransactionRepository monetaryTransactionRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _payOSService = payOSService;
            _monetaryTransactionService = monetaryTransactionService;
            _monetaryTransactionRepository = monetaryTransactionRepository;
        }
        public async Task<ResponseResult<PackageViewModel>> CreatePackage(PackageRequestModel request)
        {
            Package rs = new Package();
            try
            {
                rs = _mapper.Map<Package>(request);

                rs.Status = (int)PackageStatus.INACTIVE;
                rs.CreatedDate = DateTime.Now;

                if (!await _repository.CreatePackage(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }
            }
            catch (Exception)
            {
                return new ResponseResult<PackageViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<PackageViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PackageViewModel>(rs)
            };
        }

        public async Task<ResponseResult<PackageViewModel>> DeletePackage(Guid id)
        {
            Package rs = new Package();
            try
            {
                rs = await _repository.GetByIdGuid(id);
                if (rs is null)
                {
                    return new ResponseResult<PackageViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                rs.Status = (int)PackageStatus.INACTIVE;

                //_repository.DetachEntity(rs);
                //_repository.MotifyEntity(rs);

                if (!await _repository.UpdatePackage(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<PackageViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<PackageViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PackageViewModel>(rs)
            };
        }

        public async Task<ResponseResult<PackageViewModel>> EnablePackage(Guid id)
        {
            Package rs = new Package();
            try
            {
                rs = await _repository.GetByIdGuid(id);
                if (rs is null)
                {
                    return new ResponseResult<PackageViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                rs.Status = (int)PackageStatus.ACTIVE;

                //_repository.DetachEntity(rs);
                //_repository.MotifyEntity(rs);

                if (!await _repository.UpdatePackage(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<PackageViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<PackageViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PackageViewModel>(rs)
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<PackageViewModel>> GetPackagesForAdmin(string? filter, PagingRequest paging, PackageOrderFilter orderFilter)
        {
            (int, IQueryable<PackageViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Package>()))
                        .AsQueryable()
                        .ProjectTo<PackageViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilterForAdmin(filter);

                    string? colName = Enum.GetName(typeof(PackageOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PackageViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PackageViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()
            };
        }


        public async Task<DynamicModelResponse.DynamicModelsResponse<PackageViewModel>> GetPackages(PackageViewModel filter, PagingRequest paging, PackageOrderFilter orderFilter)
        {
            (int, IQueryable<PackageViewModel>) result;
            try
            {
                lock (_repository)
                {
                    var data = _repository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Package>()))
                        .AsQueryable()
                        .ProjectTo<PackageViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(PackageOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<PackageViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<PackageViewModel>()
            {
                Message = Constraints.INFORMATION,
                Metadata = new DynamicModelResponse.PagingMetadata()
                {
                    Page = paging.page,
                    Size = paging.pageSize,
                    Total = result.Item1
                },
                Results = result.Item2.ToList()
            };
        }


        public async Task<ResponseResult<PackageViewModel>> UpdatePackage(PackageRequestModel request, Guid id)
        {
            Package rs = new Package();
            try
            {
                var data = await _repository.GetByIdGuid(id);
                if (data is null)
                {
                    return new ResponseResult<PackageViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };
                }

                rs = _mapper.Map<Package>(request);

                rs.CreatedDate = data.CreatedDate;
                rs.Status = data.Status;
                rs.PackageId = id;

                _repository.DetachEntity(data);
                _repository.MotifyEntity(rs);

                if (!await _repository.UpdatePackage(rs))
                {
                    _repository.DetachEntity(rs);
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<PackageViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                };
            }
            finally { lock (_repository) { } }

            return new ResponseResult<PackageViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = _mapper.Map<PackageViewModel>(rs)
            };
        }

        #region payOS

        public async Task<ResponseResult<PayOSPackagePaymentResponse>> CreatePayOSPackagePurchasePayment(MonetaryTransactionRequestModel transactionRequest)
        {
            long orderCode = GenerateOrderCode();
            transactionRequest.PaymentCode = orderCode.ToString();
            // Create monetary transaction
            var transactionResult = await _monetaryTransactionService.CreateTransaction(transactionRequest);
            if (transactionResult == null || transactionResult.Value == null)
            {
                return new ResponseResult<PayOSPackagePaymentResponse>()
                {
                    Message = $"Tạo yêu cầu thanh toán bằng payOS thất bại. Vui lòng thử lại!",
                    Value = null,
                    result = false,
                };
            }
            var transaction = transactionResult.Value;
            var package = await _repository.GetByIdGuid((Guid)transaction.PackageId);

            var listItems = GetTransactionListItemData(transaction);

            string description = $"Người dùng nạp {package.StarNumber} UP";
            var createPaymentLink = await _payOSService.CreatePaymentLink(orderCode, description, listItems);

            if (createPaymentLink == null)
            {
                return new ResponseResult<PayOSPackagePaymentResponse>()
                {
                    Message = $"Tạo yêu cầu thanh toán bằng payOS thất bại. Vui lòng thử lại!",
                    Value = null,
                    result = false,
                };
            }

            var paymentResponse = new PayOSPackagePaymentResponse();
            paymentResponse.MapPayOSPaymentLink(createPaymentLink);
            paymentResponse.MapTransactionData(transaction, package.StarNumber);
            return new ResponseResult<PayOSPackagePaymentResponse>()
            {
                Message = "Thành công!",
                result = true,
                Value = paymentResponse
            };
        }

        private List<ItemData> GetTransactionListItemData(MonetaryTransactionViewModel monetaryTransaction)
        {
            var list = new List<ItemData>();
            ItemData item = new ItemData(monetaryTransaction.PackageName!, 1, (int)monetaryTransaction.MoneyAmount!);
            list.Add(item);
            return list;
        }

        private long GenerateOrderCode()
        {
            int count = _monetaryTransactionRepository.Count();
            return count++;
        }

        #endregion
    }
}
