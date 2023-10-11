using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace EZOrderApi.DTO
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? ShopName { get; set; } = null;
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? FullName { get; set; } = null;
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? Mobile { get; set; } = null;
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public string? Email { get; set; } = null;
        public string PackageType { get; set; } = "PLATINUM";
        [Required(ErrorMessage = "{0} ไม่สามารถเป็นค่าว่าง")]
        public required string Password { get; set; }
        public string ShopType { get; set; } = "RESTAURANT";
        public string? ReferralCode { get; set; }
        public List<string> ShopCategories { get; set; } = new List<string>();
    }
}
