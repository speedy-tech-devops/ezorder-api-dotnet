using Amazon.Runtime;
using AutoMapper;
using CommonServices;
using DataServices;
using DataServices.Models;
using DataServices.Repository;
using EZOrderApi.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Writers;
using MongoDB.Bson;
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
        private readonly EmailService _EmailService;
        public ShopService(IMapper mapper, MongoDBServices mongoDBService, IConfiguration configuration)
        {
            _mapper = mapper;
            _mongoDBService = mongoDBService;
            _configuration = configuration;
        }
        public async Task<ResponseBaseModel> Register(RegisterModel registerModel, ResponseBaseModel response)
        {
            LineNotifyExtension lineNotifyExtension = new("lzReMRETIZzlUfee7XeTAToQBOK2dxONV6AMkvKGQlz");
            registerModel.Email = registerModel.Email?.ToLower();
            string passwordEncrypt = await BCryptExtension.Encrypt(registerModel.Password);
            var filter = Builders<ShopRoles>.Filter.Eq("code", "shop-owner");
            var role = await _mongoDBService.GetFilteredDocumentsAsync(filter);
            var filterRef = Builders<ReferralCode>.Filter.Where(c => c.IsEnable == true && c.Code == registerModel.ReferralCode && c.IsDelete == false);
            var filterCategory = Builders<ShopCategories>.Filter.Where(c => c.IsDelete == false && registerModel.ShopCategories.Contains(c.Id));
            var referral = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            try
            {
                var category = await _mongoDBService.GetFilteredDocumentsAsync(filterCategory);
                if (!category.Any())
                {
                    response.Status = 400;
                    response.StatusText = $"ShopCategories ไม่พบในระบบ";
                    return response;
                }
            }
            catch (Exception)
            {
                response.Status = 400;
                response.StatusText = $"ShopCategories ไม่พบในระบบ";
                return response;
            }
            var filterShop = Builders<ShopUsers>.Filter.Where(c => c.Email == registerModel.Email);
            var shopUsersCheck = await _mongoDBService.GetFilteredDocumentsAsync(filterShop);
            if (shopUsersCheck.Any())
            {
                response.Status = (int)HttpStatusCode.BadRequest;
                response.StatusText = "อีเมลนี้มีผู้ใช้งานแล้ว กรุณาระบุอีเมลใหม่อีกครั้ง";
                string notiMsgError = $"{Environment.NewLine}*❌❌❌มีร้านสมัครเข้ามาซ้ำ❌❌❌*{Environment.NewLine}" +
                    $"ชื่อร้าน: {registerModel.ShopName}{Environment.NewLine}" +
                    $"ชื่อผู้ติดต่อ: {registerModel.FullName}{Environment.NewLine}" +
                    $"เบอร์โทร: {registerModel.Mobile}{Environment.NewLine}" +
                    $"อีเมล: {registerModel.Email}{Environment.NewLine}" +
                    $"Password: {registerModel.Password}";
                if (!string.IsNullOrWhiteSpace(registerModel.ReferralCode))
                {
                    notiMsgError += $"{Environment.NewLine}Referral Code: {registerModel.ReferralCode}";
                }
                await lineNotifyExtension.SendMessageAsync(notiMsgError, "false");
                return response;
            }
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
                ReferralCodeId = referral?.FirstOrDefault()?.Id,
                ExpirdAt = DateTime.Now.AddDays(45),
                ApprovedBy = "",
                UpdatedBy = "",
                ShopCategories = registerModel.ShopCategories
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
                LineConfigs = new BranchLineConfigs()
                {
                    LineAccessToken = "",
                    LineAccountType = "",
                    LineAccount = "",
                    LineLiffId = ""
                },
                LogoImage = "",
                Name = new BranchName()
                {
                    EN = "branch 1",
                    TH = "สาขา 1"
                },
                PaymentConfigs = new BranchPaymentConfigs()
                {
                    AccountName = "",
                    AccountNumber = "",
                    BankName = "",
                    BankType = ""
                },
                PhoneNumber = "",
                ReceiptConfigs = new BranchReceiptConfigs()
                {
                    FooterText = "",
                    HeaderText = "",
                },
                Shop = shops.Id,
                OpeningHours = "",
                UpdatedBy = "",
                CreatedBy = "",
                DeletedBy = ""
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
            else
                shopUsers.LastName = "";
            shopUsers.Password = passwordEncrypt;
            shopUsers.OwnerShop = shops.Id;
            shopUsers.IsDelete = false;
            shopUsers.IsOwnerAccount = true;
            shopUsers.IsEnable = true;
            shopUsers.Roles = role.Select(c => c.Id).ToList();
            shopUsers.UpdatedAt = DateTime.Now;
            shopUsers.version = Convert.ToInt32(_configuration.GetSection("Version").Value);
            shopUsers = await _mongoDBService.InsertDocumentAsync(shopUsers);
            string notiMsg = $"{Environment.NewLine}⚠️*มีร้านสมัครเข้ามาใหม่*{Environment.NewLine}" +
                $"ชื่อร้าน: {registerModel.ShopName}{Environment.NewLine}" +
                $"ชื่อผู้ติดต่อ: {registerModel.FullName}{Environment.NewLine}" +
                $"เบอร์โทร: {registerModel.Mobile}{Environment.NewLine}" +
                $"อีเมล: {registerModel.Email}{Environment.NewLine}" +
                $"Password: {registerModel.Password}";
            if (!string.IsNullOrWhiteSpace(registerModel.ReferralCode))
            {
                notiMsg += $"{Environment.NewLine}Referral Code: {registerModel.ReferralCode}";
            }
            var lineResponse = await lineNotifyExtension.SendMessageAsync(notiMsg, "false");

            return response;
        }
        public async Task<ShopWaitApproveResponse> GetShopWaitApprove(ShopWaitApproveResponse response)
        {
            var filter = Builders<Shops>.Filter.Where(c => c.IsApproved == false);
            var shops = await _mongoDBService.GetFilteredDocumentsAsync(filter);
            response.Shops = _mapper.Map<List<ShopWaitApproveItemResponse>>(shops);
            return response;
        }
        public async Task<ShopListResponse> GetShop(ShopListResponse response, string searchText, int? pageIndex, int? pageSize, string sortFieldName, string sortDirection)
        {
            var filterBuilder = Builders<Shops>.Filter;
            var filter = filterBuilder.Where(c => true == true);
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                filter &= filterBuilder.Where(c => c.Name != null && (c.Name.TH.Contains(searchText) || c.Name.EN.Contains(searchText)));
            }
            int skipCount = ((pageIndex ?? 1) - 1) * (pageSize ?? 10);
            SortDefinition<Shops> sortDefinition = null;
            sortDirection = sortDirection ?? "asc";
            sortFieldName = sortFieldName ?? "_id";
            sortFieldName = char.ToUpper(sortFieldName[0]) + sortFieldName.Substring(1);
            var shops = await _mongoDBService.GetFilteredDocumentsAsync(filter, skipCount, pageSize ?? 10, sortDirection);
            response.TotalRecord = await _mongoDBService.GetTotalCountAsync(filter);
            response.TotalPage = (int)Math.Ceiling((double)response.TotalRecord / (pageSize ?? 10));
            response.Shops = _mapper.Map<List<Shop>>(shops);
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

        public async Task<ResponseBaseModel> CreateShopCategories(ShopCategoriesModel request, ResponseBaseModel response)
        {
            var filterRef = Builders<ShopCategories>.Filter.Where(c => c.IsDelete == true && c.Name.TH == request.NameTh && c.Name.EN == request.NameEn);
            var filter = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            if (filter.Any())
            {
                response.Status = (int)HttpStatusCode.BadRequest;
                response.StatusText = $"ชื่อประเภทมีซ้ำในระบบ";
                return response;
            }
            ShopCategories shopCategories = new ShopCategories()
            {
                CreatedAt = DateTime.Now,
                Name = new ShopCategoriesName()
                {
                    EN = request.NameEn,
                    TH = request.NameTh,
                },
                IsDelete = false
            };
            shopCategories = await _mongoDBService.InsertDocumentAsync(shopCategories);
            return response;
        }
        public async Task<ShopCategoriesResponse> GetShopCategories(ShopCategoriesResponse response)
        {
            var filterRef = Builders<ShopCategories>.Filter.Where(c => c.IsDelete == false);
            var referral = await _mongoDBService.GetFilteredDocumentsAsync(filterRef);
            response.Referral = _mapper.Map<List<ShopCategoriesItemResponse>>(referral);
            return response;
        }
        public async Task<ResponseBaseModel> DeleteShopCategories(string id, ResponseBaseModel response)
        {
            try
            {
                var filter = Builders<ShopCategories>.Filter.Where(c => c.Id == id);
                var shops = _mongoDBService.GetFilteredDocumentsAsync(filter).GetAwaiter().GetResult().FirstOrDefault();
                if (shops == null)
                {
                    response.Status = (int)HttpStatusCode.BadRequest;
                    response.StatusText = "ไม่พบข้อมูล";
                    return response;
                }
                var update = Builders<ShopCategories>.Update.Set(x => x.IsDelete, true)
                                                            .Set(x => x.UpdatedAt, DateTime.Now);
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
