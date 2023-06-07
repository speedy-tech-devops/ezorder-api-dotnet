using AutoMapper;
using CommonServices;
using DataServices;
using DataServices.Models;
using DataServices.Repository;
using EZOrderApi.DTO;
using MongoDB.Driver;
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
            var filterRef = Builders<ReferralCode>.Filter.Where(c => c.IsEnable == true && c.Code == registerModel.ReferralCode);
            var referral = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            var notiEvents = await _mongoDBService.GetAllDocumentsAsync<NotificationEvents>();
            Shops shops = new Shops()
            {
                IsDelete = false,
                IsEnable = true,
                PackageType = registerModel.PackageType,
                ShopType = registerModel.ShopType,
                PhoneNumber = registerModel.Mobile,
                IsApproved = false,
                UpdatedAt = DateTime.UtcNow,
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
                CreatedAt = DateTime.UtcNow,
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
                UpdatedAt = DateTime.UtcNow,
            };
            categories = await _mongoDBService.InsertDocumentAsync(categories);
            var shopUsers = _mapper.Map<ShopUsers>(registerModel);
            shopUsers.OwnerShop = shops.Id;
            shopUsers.IsDelete = false;
            shopUsers.IsOwnerAccount = true;
            shopUsers.IsEnable = true;
            shopUsers.Roles = role.Select(c => c.Id).ToList();
            shopUsers.UpdatedAt = DateTime.UtcNow;
            shopUsers.version = Convert.ToInt32(_configuration.GetSection("Version").Value);
            shopUsers = await _mongoDBService.InsertDocumentAsync(shopUsers);
        }
        private async Task CreateShop(RegisterModel registerModel)
        {

        }
    }
}
