using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Internal;
using Org.BouncyCastle.Asn1.Ocsp;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Commons;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.ReponseModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_BusinessLayer.RequestModels.Helpers;
using SU24SE069_PLATFORM_KAROKE_DataAccess.Models;
using SU24SE069_PLATFORM_KAROKE_Repository.IRepository;
using SU24SE069_PLATFORM_KAROKE_Repository.Repository;
using SU24SE069_PLATFORM_KAROKE_Service.IServices;
using SU24SE069_PLATFORM_KAROKE_Service.ReponseModels;
using SU24SE069_PLATFORM_KAROKE_Service.RequestModels.Recording;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SU24SE069_PLATFORM_KAROKE_Service.Services
{
    public class RecordingService : IRecordingService
    {
        private readonly IMapper _mapper;
        private readonly IRecordingRepository _recordingRepository;
        private readonly IVoiceAudioRepository _voiceAudioRepository;

        public RecordingService(IMapper mapper, IRecordingRepository recordingRepository, IVoiceAudioRepository voiceAudioRepository)
        {
            _mapper = mapper;
            _recordingRepository = recordingRepository;
            _voiceAudioRepository = voiceAudioRepository;
        }

        public async Task<ResponseResult<RecordingViewModel>> CreateRecording(CreateRecordingRequestModel request)
        {
            RecordingViewModel result = new RecordingViewModel();
            try
            {

                var data = _mapper.Map<Recording>(request);

                data.CreatedDate = DateTime.Now;
                data.UpdatedDate = DateTime.Now;


                if (!data.VoiceAudios.IsNullOrEmpty())
                {
                    data.VoiceAudios = data.VoiceAudios
                        .Select(item => {item.UploadTime = DateTime.Now; return item; })
                        .ToList();
                }

                if (!await _recordingRepository.AddRecording(data))
                {
                    _recordingRepository.DetachEntity(data);
                    throw new Exception();
                }

                await _recordingRepository.SaveChagesAsync();
                result = _mapper.Map<RecordingViewModel>(data);



            }
            catch (Exception)
            {
                return new ResponseResult<RecordingViewModel>()
                {
                    Message = Constraints.CREATE_FAILED,
                    result = false,
                };
            }
            finally
            {
                await _recordingRepository.DisponseAsync();
            }

            return new ResponseResult<RecordingViewModel>()
            {
                Message = Constraints.CREATE_SUCCESS,
                result = true,
                Value = result
            };
        }

        public async Task<ResponseResult<RecordingViewModel>> Delete(Guid id)
        {
            try
            {
                var data = await _recordingRepository.GetByIdGuid(id);
                if (data == null)
                    return new ResponseResult<RecordingViewModel>()
                    {
                        Message = Constraints.NOT_FOUND,
                        result = false,
                    };

                if ( !await _recordingRepository.DeleteRecording(id)) 
                {
                    throw new Exception();
                }

            }
            catch (Exception)
            {
                return new ResponseResult<RecordingViewModel>()
                {
                    Message = Constraints.DELETE_FAILED,
                    result = false,
                };
            }
            finally { await _recordingRepository.DisponseAsync(); }

            return new ResponseResult<RecordingViewModel>()
            {
                Message = Constraints.DELETE_SUCCESS,
                result = true,
            };
        }

        public async Task<ResponseResult<RecordingViewModel>> GetRecording(Guid id)
        {
            Recording data = new Recording();
            try
            {
                data = await _recordingRepository.GetByIdGuid(id);

                if(data == null)
                {
                    return new ResponseResult<RecordingViewModel>()
                    {
                        Message = Constraints.EMPTY_INPUT_INFORMATION,
                        result = false,
                    };
                }
            }catch(Exception)
            {
                return new ResponseResult<RecordingViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                    result = false,
                };
            }

            return new ResponseResult<RecordingViewModel>()
            {
                Message = Constraints.INFORMATION,
                result = true,
            };
        }

        public async Task<DynamicModelResponse.DynamicModelsResponse<RecordingViewModel>> GetRecordings(RecordingViewModel filter, PagingRequest paging, RecordingOrderFilter orderFilter)
        {
            (int, IQueryable<RecordingViewModel>) result;
            try
            {
                lock (_recordingRepository)
                {
                    var data = _recordingRepository.GetAll(
                                                includeProperties: String.Join(",",
                                                SupportingFeature.GetNameIncludedProperties<Recording>()))
                        .AsQueryable()

                        .ProjectTo<RecordingViewModel>(_mapper.ConfigurationProvider)
                        .DynamicFilter(filter);

                    string? colName = Enum.GetName(typeof(RecordingOrderFilter), orderFilter);

                    data = SupportingFeature.Sorting(data.AsEnumerable(), (SortOrder)paging.OrderType, colName).AsQueryable();

                    result = data.PagingIQueryable(paging.page, paging.pageSize,
                            Constraints.LimitPaging, Constraints.DefaultPaging);
                }


            }
            catch (Exception)
            {
                return new DynamicModelResponse.DynamicModelsResponse<RecordingViewModel>()
                {
                    Message = Constraints.LOAD_FAILED,
                };
            }

            return new DynamicModelResponse.DynamicModelsResponse<RecordingViewModel>()
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

        public async Task<ResponseResult<RecordingViewModel>> UpdateRecording(Guid id, UpdateRecording1RequestModel request)
        {
            RecordingViewModel result = new RecordingViewModel();
            try
            {
                lock (_recordingRepository)
                {
                    var data1 = _recordingRepository.GetRecording(id).Result;

                    if (data1 is null)
                    {
                        return new ResponseResult<RecordingViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                        };
                    }

                    var data = _mapper.Map<Recording>(data1);

                    data.UpdatedDate = DateTime.Now;
                    data.StartTime = request.StartTime;
                    data.EndTime = request.EndTime;
                    if(request.VoiceAudios != null)
                    {
                        foreach(var audio in data.VoiceAudios)
                        {
                            if(request.VoiceAudios.Any(x => x.VoiceId == audio.VoiceId))
                            {
                                audio.StartTime = request.VoiceAudios.FirstOrDefault(x => x.VoiceId == audio.VoiceId).StartTime;
                                audio.EndTime = request.VoiceAudios.FirstOrDefault(x => x.VoiceId == audio.VoiceId).EndTime;
                                audio.DurationSecond = request.VoiceAudios.FirstOrDefault(x => x.VoiceId == audio.VoiceId).DurationSecond;
                            }
                        }
                    }

                    if (data == null)
                    {
                        return new ResponseResult<RecordingViewModel>()
                        {
                            Message = Constraints.NOT_FOUND,
                            result = false,
                            Value = _mapper.Map<RecordingViewModel>(data)
                        };
                    }

                    _recordingRepository.DetachEntity(data1);
                    _recordingRepository.MotifyEntity(data);

                    if (!_recordingRepository.UpdateRecording(data1).Result)
                    {
                        _recordingRepository.DetachEntity(data);
                        throw new Exception();
                    }

                    result = _mapper.Map<RecordingViewModel>(data);
                };


            }
            catch (Exception)
            {
                return new ResponseResult<RecordingViewModel>()
                {
                    Message = Constraints.UPDATE_FAILED,
                    result = false,
                    Value = result
                };
            }
            finally
            {
                await _recordingRepository.DisponseAsync();
            }

            return new ResponseResult<RecordingViewModel>()
            {
                Message = Constraints.UPDATE_SUCCESS,
                result = true,
                Value = result
            };
        }
    }
}
