using CommonServices;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataServices.Models
{
    [DisplayName("branches")]
    [BsonIgnoreExtraElements]
    public class Branches
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        [BsonElement("shop")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Shop { get; set; }
        [BsonElement("name")]
        public BranchName? Name { get; set; }
        [BsonElement("description")]
        public BrancheDescription? Description { get; set; }
        [BsonElement("address")]
        public string? Address { get; set; }
        [BsonElement("system_configs")]
        public BranchSystemConfigs? SystemConfigs { get; set; }
        [BsonElement("receipt_configs")]
        public BranchReceiptConfigs? ReceiptConfigs { get; set; }
        [BsonElement("line_configs")]
        public BranchLineConfigs? LineConfigs { get; set; }
        [BsonElement("payment_configs")]
        public BranchPaymentConfigs? PaymentConfigs { get; set; }
        [BsonElement("event_configs")]
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> EventConfigs { get; set; } = new List<string>();
        [BsonElement("tax_id")]
        public string? TaxId { get; set; }
        [BsonElement("phone_number")]
        public string? PhoneNumber { get; set; }
        [BsonElement("logo_image")]
        public string? LogoImage { get; set; }
        [BsonElement("opening_hours")]
        public string? OpeningHours { get; set; }
        [BsonElement("closing_time")]
        public string? ClosingTime { get; set; }
        [BsonElement("is_enable")]
        public bool? IsEnable { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("created_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? CreatedBy { get; set; }

        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("updated_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? UpdatedBy { get; set; }
        [BsonElement("is_delete")]
        public bool? IsDelete { get; set; }
        [BsonElement("deleted_at")]
        public DateTime DeletedAt { get; set; }
        [BsonElement("deleted_by")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? DeletedBy { get; set; }
        [BsonElement("is_open")]
        public bool? IsOpen { get; set; }
    }
    public class BrancheDescription
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
    public class BranchName
    {
        [BsonElement("th")]
        public string? TH { get; set; }
        [BsonElement("en")]
        public string? EN { get; set; }
    }
    public class BranchSystemConfigs
    {
        [BsonElement("is_vat_enable")]
        public bool? IsVatEnable { get; set; }
        [BsonElement("is_service_charge_enable")]
        public bool? IsServiceChargeEnable { get; set; }
        [BsonElement("is_vat_exclude")]
        public bool? IsVatExclude { get; set; }
        [BsonElement("vat_rate")]
        public int? VatRate { get; set; }
        [BsonElement("service_charge_rate")]
        public int? ServiceChargeRate { get; set; }
    }
    public class BranchReceiptConfigs
    {
        [BsonElement("header_text")]
        public string? HeaderText { get; set; }
        [BsonElement("footer_text")]
        public string? FooterText { get; set; }
    }
    public class BranchLineConfigs
    {
        [BsonElement("line_account")]
        public string? LineAccount { get; set; }
        [BsonElement("line_account_type")]
        public string? LineAccountType { get; set; }
        [BsonElement("line_liff_id")]
        public string? LineLiffId { get; set; }
        [BsonElement("line_access_token")]
        public string? LineAccessToken { get; set; }
    }
    public class BranchPaymentConfigs
    {
        [BsonElement("bank_type")]
        public string? BankType { get; set; }
        [BsonElement("account_name")]
        public string? AccountName { get; set; }
        [BsonElement("account_number")]
        public string? AccountNumber { get; set; }
        [BsonElement("bank_name")]
        public string? BankName { get; set; }
    }
}
