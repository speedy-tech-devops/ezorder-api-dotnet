using Amazon.Runtime;
using AutoMapper;
using CommonServices;
using DataServices;
using DataServices.Models;
using DataServices.Repository;
using EZOrderApi.DTO;
using MongoDB.Driver;
using MongoDB.Libmongocrypt;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace EZOrderApi.Services
{
    public class ShopService
    {
        private readonly MongoDBServices _mongoDBService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public ShopService(IMapper mapper, MongoDBServices mongoDBService, IConfiguration configuration)
        {
            _mapper = mapper;
            _mongoDBService = mongoDBService;
            _configuration = configuration;
        }
        public async Task Register(RegisterModel registerModel)
        {
            string passwordEncrypt = await BCryptExtension.Encrypt(registerModel.Password);
            var filter = Builders<ShopRoles>.Filter.Eq("code", "shop-owner");
            var role = await _mongoDBService.GetFilteredDocumentsAsync(filter);
            var filterRef = Builders<ReferralCode>.Filter.Where(c => c.IsEnable == true && c.Code == registerModel.ReferralCode && c.IsDelete == false);
            var referral = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            var notiEvents = await _mongoDBService.GetAllDocumentsAsync<NotificationEvents>();
            Shops shops = new Shops()
            {
                IsDelete = false,
                IsEnable = true,
                PackageType = registerModel.PackageType,
                ShopType = registerModel.ShopType,
                PhoneNumber = registerModel.Mobile,
                IsApproved = true,
                ApprovedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Name = new ShopName()
                {
                    EN = registerModel.ShopName,
                    TH = registerModel.ShopName
                },
                Description = new ShopDescription()
                {
                    EN = "",
                    TH = ""
                },
                Address = "",
                Addresses = new List<ShopAddress>()
                {
                    new ShopAddress()
                    {
                        City = "",
                        Coutry = "",
                        IsDefault = true,
                        State = "",
                        StreetAddress = "",
                        ZipCode = ""
                    }
                },
                SocialConfigs = new ShopSocial()
                {
                    FacebookUrl = "",
                    IGUrl = "",
                    WebsiteUrl = ""
                },
                ReferralCodeId = referral?.FirstOrDefault()?.Id
            };
            shops = await _mongoDBService.InsertDocumentAsync(shops);
            Branches branches = new Branches()
            {
                Address = "",
                ClosingTime = "",
                CreatedAt = DateTime.Now,
                //CreatedBy = shops.Id,
                TaxId = "",
                SystemConfigs = new BranchSystemConfigs()
                {
                    IsVatEnable = true,
                    IsVatExclude = false,
                    ServiceChargeRate = 10,
                    VatRate = 7,
                    IsServiceChargeEnable = true
                },
                Description = new BrancheDescription()
                {
                    EN = "",
                    TH = ""
                },
                EventConfigs = notiEvents?.Select(c => c.Id).ToList(),
                IsDelete = false,
                IsEnable = true,
                IsOpen = true,
                LineConfigs = new BranchLineConfigs(),
                LogoImage = "",
                Name = new BranchName()
                {
                    EN = "branch 1",
                    TH = "สาขา 1"
                },
                PaymentConfigs = new BranchPaymentConfigs(),
                PhoneNumber = "",
                ReceiptConfigs = new BranchReceiptConfigs()
                {
                    FooterText = "",
                    HeaderText = "",
                },
                Shop = shops.Id,
                OpeningHours = "",
            };
            branches = await _mongoDBService.InsertDocumentAsync(branches);
            Categories categories = new Categories()
            {
                Description = new CategoryDescription()
                {
                    EN = "Recommend",
                    TH = "เมนูแนะนำ"
                },
                Name = new CategoryName()
                {
                    EN = "⭐ Recommend",
                    TH = "⭐ เมนูแนะนำ"
                },
                IsDelete = false,
                IsEnable = true,
                Status = "PUBLISHED",
                Branch = branches.Id,
                DisplayOrder = 1,
                Shop = shops.Id,
                IsRecommendCategory = true,
                UpdatedAt = DateTime.Now,
            };
            categories = await _mongoDBService.InsertDocumentAsync(categories);
            var shopUsers = _mapper.Map<ShopUsers>(registerModel);
            var name = registerModel.FullName.Split(' ').ToList();
            shopUsers.FirstName = name[0];
            if (name.Count > 1)
                shopUsers.LastName = name[1];
            shopUsers.Password = passwordEncrypt;
            shopUsers.OwnerShop = shops.Id;
            shopUsers.IsDelete = false;
            shopUsers.IsOwnerAccount = true;
            shopUsers.IsEnable = true;
            shopUsers.Roles = role.Select(c => c.Id).ToList();
            shopUsers.UpdatedAt = DateTime.Now;
            shopUsers.version = Convert.ToInt32(_configuration.GetSection("Version").Value);
            shopUsers = await _mongoDBService.InsertDocumentAsync(shopUsers);
            LineNotifyExtension lineNotifyExtension = new("lzReMRETIZzlUfee7XeTAToQBOK2dxONV6AMkvKGQlz");
            string notiMsg = $"{Environment.NewLine}⚠️*มีร้านสมัครเข้ามาใหม่*{Environment.NewLine}" +
                $"Shop: {registerModel.ShopName}{Environment.NewLine}" +
                $"Tel: {registerModel.Mobile}{Environment.NewLine}" +
                $"Email: {registerModel.Email}{Environment.NewLine}" +
                $"Password: {registerModel.Password}{Environment.NewLine}" +
                $"Name: {registerModel.FullName}";
            var lineResponse = await lineNotifyExtension.SendMessageAsync(notiMsg, "false");
        }
        public async Task<ShopWaitApproveResponse> GetShopWaitApprove(ShopWaitApproveResponse response)
        {
            var filter = Builders<Shops>.Filter.Where(c => c.IsApproved == false);
            var shops = await _mongoDBService.GetFilteredDocumentsAsync(filter);
            response.Shops = _mapper.Map<List<ShopWaitApproveItemResponse>>(shops);
            return response;
        }
        public async Task<ResponseBaseModel> ApproveShop(string shopId, ResponseBaseModel response)
        {
            try
            {
                var filter = Builders<Shops>.Filter.Where(c => c.Id == shopId);
                var shops = _mongoDBService.GetFilteredDocumentsAsync(filter).GetAwaiter().GetResult().FirstOrDefault();
                if (shops == null)
                {
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.StatusText = "ไม่พบข้อมูล";
                    return response;
                }
                var update = Builders<Shops>.Update.Set(x => x.IsApproved, true)
                                                                     .Set(x => x.ApprovedAt, DateTime.Now);
                await _mongoDBService.UpdateDocumentAsync(filter, update);
                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
