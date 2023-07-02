using Amazon.Runtime.Internal.Transform;
using AutoMapper;
using CommonServices;
using DataServices.Models;
using DataServices.Repository;
using EZOrderApi.DTO;
using MongoDB.Driver;
using System.Net;

namespace EZOrderApi.Services
{
    public class ReferralService
    {
        private readonly MongoDBServices _mongoDBService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ReferralService(IMapper mapper, MongoDBServices mongoDBService, IConfiguration configuration)
        {
            _mapper = mapper;
            _mongoDBService = mongoDBService;
            _configuration = configuration;
        }
        public async Task<ResponseBaseModel> CreateSaleReferral(SaleReferralModel referralModel, ResponseBaseModel response)
        {
            var filterRef = Builders<ReferralCode>.Filter.Where(c => c.IsEnable == true && c.Code == referralModel.Code && c.IsDelete == false);
            var referral = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            if (referral.Any())
            {
                response.Status = (int)HttpStatusCode.BadRequest;
                response.StatusText = $"Referral Code : {referralModel.Code} ซ้ำในระบบ";
                return response;
            }
            var notiEvents = await _mongoDBService.GetAllDocumentsAsync<NotificationEvents>();
            ReferralCode referralCode = new ReferralCode()
            {
                ReferralType = "SALE",
                Code = referralModel.Code,
                CreatedAt = DateTime.Now,
                Description = referralModel.ReferralName,
                IsEnable = referralModel.IsEnable,
                IsDelete = false
            };
            referralCode = await _mongoDBService.InsertDocumentAsync(referralCode);
            return response;
        }
        public async Task<SaleReferralResponse> GetSaleReferral(SaleReferralResponse response)
        {
            var filterRef = Builders<ReferralCode>.Filter.Where(c => c.ReferralType == "SALE");
            var referral = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            response.Referral = _mapper.Map<List<SaleReferralItemResponse>>(referral);
            return response;
        }
    }
}
