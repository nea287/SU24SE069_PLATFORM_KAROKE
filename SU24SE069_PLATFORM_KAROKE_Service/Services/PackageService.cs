using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.Extensions.Caching.Memory;
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
        private const string CheckoutUrlCacheName = "checkoutUrl";
        private const string QRCodeCacheName = "qrCode";
        private readonly IMapper _mapper;
        private readonly IPackageRepository _repository;
        private readonly IPayOSService _payOSService;
        private readonly IMonetaryTransactionService _monetaryTransactionService;
        private readonly IMonetaryTransactionRepository _monetaryTransactionRepository;
        private readonly IMemoryCache _memoryCache;

        public PackageService(IPackageRepository repository,
            IMapper mapper,
            IPayOSService payOSService,
            IMonetaryTransactionService monetaryTransactionService,
            IMonetaryTransactionRepository monetaryTransactionRepository,
            IMemoryCache memoryCache)
        {
            _mapper = mapper;
            _repository = repository;
            _payOSService = payOSService;
            _monetaryTransactionService = monetaryTransactionService;
            _monetaryTransactionRepository = monetaryTransactionRepository;
            _memoryCache = memoryCache;
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


        public async Task<ResponseResult<PackageViewModel>> ChangeStatus(Guid id, PackageStatus status)
        {
            Package rs = new Package();
            try
            {
                var data = await _repository.GetByIdGuid(id);

                if(data == null)
                {
                    await _repository.DisponseAsync();
                    return new ResponseResult<PackageViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false
                    };
                }

                data.Status = (int) status;
                data.PackageId = id;

                if(!await _repository.UpdatePackage(data))
                {
                    throw new Exception();  
                }

            }
            catch (Exception)
            {
                return new ResponseResult<PackageViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false
                };
            }
            finally
            {
                await _repository.DisponseAsync();
            }

            return new ResponseResult<PackageViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
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
            // Check for existing monetary transaction.
            // If there is any, cancel create purchase request
            if (await _monetaryTransactionRepository.IsAnyMemberPendingTransactionExist(transactionRequest.MemberId))
            {
                return new ResponseResult<PayOSPackagePaymentResponse>()
                {
                    Message = $"Người dùng có yêu cầu mua gói UP chưa xử lý! Vui lòng thanh toán hoặc hủy yêu cầu trước đó để tạo yêu cầu mới.",
                    Value = null,
                    result = false,
                };
            }

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

            // Set payment url and qr code data to memory cache
            SupportingFeature.Instance.SetDataToCache(_memoryCache, $"{orderCode.ToString()}_{CheckoutUrlCacheName}", paymentResponse.checkoutUrl, 120);
            SupportingFeature.Instance.SetDataToCache(_memoryCache, $"{orderCode.ToString()}_{QRCodeCacheName}", paymentResponse.qrCode, 120);

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

        public async Task<ResponseResult<string>> CancelPayOSPackagePurchaseRequest(Guid monetaryTransactionId)
        {
            var monetaryTransaction = await _monetaryTransactionRepository.GetByIdGuid(monetaryTransactionId);

            // Check if transaction exists
            if (monetaryTransaction == null)
            {
                return new ResponseResult<string>()
                {
                    Message = "Hủy yêu cầu mua gói UP thất bại! Không tìm thấy yêu cầu mua gói.",
                    Value = "Hủy yêu cầu mua gói UP thất bại! Không tìm thấy yêu cầu mua gói.",
                    result = false
                };
            }

            // Check if transaction has been processed
            if (monetaryTransaction.Status != (int)PaymentStatus.PENDING)
            {
                return new ResponseResult<string>()
                {
                    Message = "Hủy yêu cầu mua gói UP thất bại! Yêu cầu mua gói không ở trạng thái đang chờ.",
                    Value = "Hủy yêu cầu mua gói UP thất bại! Yêu cầu mua gói không ở trạng thái đang chờ.",
                    result = false
                };
            }

            // Update transaction to cancelled status
            try
            {
                monetaryTransaction.Status = (int)PaymentStatus.CANCELLED;
                await _monetaryTransactionRepository.Update(monetaryTransaction);
                await _monetaryTransactionRepository.SaveChagesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to update monetary transaction status to Cancelled: {ex.Message}");
                return new ResponseResult<string>()
                {
                    Message = "Có lỗi xảy ra trong quá trình hủy yêu cầu mua gói UP. Vui lòng thử lại!",
                    Value = "Có lỗi xảy ra trong quá trình hủy yêu cầu mua gói UP. Vui lòng thử lại!",
                    result = false
                };
            }

            // Cancel payOS payment request
            if (long.TryParse(monetaryTransaction.PaymentCode, out long orderCode))
            {
                await _payOSService.CancelPaymentLinkInformation(orderCode, "Transaction cancelled by user!");
            }

            // Delete cache data of checkoutUrl and qrCode data
            SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, $"{monetaryTransaction.PaymentCode}_{CheckoutUrlCacheName}");
            SupportingFeature.Instance.RemoveDataFromCache(_memoryCache, $"{monetaryTransaction.PaymentCode}_{QRCodeCacheName}");

            return new ResponseResult<string>()
            {
                Message = "Hủy yêu cầu mua gói UP thành công!",
                Value = "Hủy yêu cầu mua gói UP thành công!",
                result = true
            };
        }

        public async Task<ResponseResult<PayOSPackagePaymentMethodResponse>> GetMemberLatestPendingPurchaseRequest(Guid memberId)
        {
            MonetaryTransaction? monetaryTransaction = null;
            try
            {
                monetaryTransaction = await _monetaryTransactionRepository.GetMemberLatestPendingTransaction(memberId);
            }
            catch (Exception)
            {
                return new ResponseResult<PayOSPackagePaymentMethodResponse>()
                {
                    Message = $"Tải yêu cầu mua gói UP đang chờ xử lý của người dùng thất bại. Vui lòng thử lại!.",
                    Value = null,
                    result = false,
                };
            }

            if (monetaryTransaction == null)
            {
                return new ResponseResult<PayOSPackagePaymentMethodResponse>()
                {
                    Message = $"Người dùng không có yêu cầu mua gói UP đang chờ xử lý.",
                    Value = null,
                    result = false,
                };
            }

            var upPackage = await _repository.GetByIdGuid(monetaryTransaction.PackageId);

            PayOSPackagePaymentMethodResponse response = new PayOSPackagePaymentMethodResponse();
            response.MapTransactionEntityData(monetaryTransaction, upPackage);

            response.checkoutUrl = SupportingFeature.Instance.GetDataFromCache(_memoryCache, $"{monetaryTransaction.PaymentCode}_{CheckoutUrlCacheName}");
            response.qrCode = SupportingFeature.Instance.GetDataFromCache(_memoryCache, $"{monetaryTransaction.PaymentCode}_{QRCodeCacheName}");

            return new ResponseResult<PayOSPackagePaymentMethodResponse>()
            {
                Message = $"Tải yêu cầu mua gói UP đang chờ xử lý của người dùng thành công!",
                Value = response,
                result = true,
            };
        }

        #endregion
    }
}
