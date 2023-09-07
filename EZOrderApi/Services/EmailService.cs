using AutoMapper;
using CommonServices;
using DataServices.Models;
using DataServices.Repository;
using EZOrderApi.DTO;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Net;

namespace EZOrderApi.Services
{
    public class EmailService
    {
        private readonly EmailSetting _emailSetting;
        public EmailService(IOptions<EmailSetting> emailSettingOptions)
        {
            _emailSetting = emailSettingOptions.Value;
        }
        public async Task<ResponseBaseModel> SendEmail(SendEmail sendEmail)
        {
            ResponseBaseModel response = new ResponseBaseModel();
            GEmailExtension gEmailExtension = new GEmailExtension(_emailSetting.NoReplyMail, _emailSetting.NoReplyPassword);
            await gEmailExtension.SendEmail(sendEmail.SendTo, sendEmail.Subject, sendEmail.Body);
            return response;
        }
        public string RegisterTemplate(string shopName)
        {
            return $@"
                <div style=""
    color: black;
"">
                    EZorder ทำให้การสั่งอาหารเป็นเรื่องง่ายๆ
                    <br>
                    <br>ขอต้อนรับ&nbsp;<b>{shopName}</b>
                    <br>
                    สำหรับการใช้งานระบบ EZorder ค่ะ
                    <br>
                    <br>
                    ขออนุญาตส่งข้อมูลสำหรับการใช้งานให้นะคะ
                    <br>
                    <br>
                    <b>
                        1. Back Office ระบบดูรายงานยอดขายและใส่ข้อมูล รายละเอียดเมนูอาหาร
                    </b>
                    <br>
                    <br>
                    วิธีการเข้าสู่ระบบ Back Office&nbsp;
                    <br>ลิงก์สำหรับเข้าสู่ระบบ:
                    <a href='https://ezorder-backoffice.speedy-tech.co/login' target='_blank'>https://ezorder-backoffice.speedy-tech.co/login</a>
                    <br>วีดีโอการใช้งานส่วนของ Back Office :
                    <br>
                    <br>
                    <b>
                        2. แอพพลิเคชัน EZorder Merchant ในการรับออเดอร์และจัดการร้าน
                    </b>
                    <br>
                    <br>
                    วิธีการเข้าสู่แอพพลิเคชัน EZorder Merchant

                    ลิงค์สำหรับดาวน์โหลดแอพ :&nbsp;
                    <a href='https://play.google.com/store/apps/details?id=com.ezorder.notification' target='_blank'>https://play.google.com/store/apps/details?id=com.ezorder.notification</a>
                    <br>
                    วีดีโอการใช้งานส่วนของแอพพลิ<wbr>เคชัน EZorder :
                    <br>
                    <br>
                    <br>ทางEZorder ได้ส่งรายละเอียดวิธีการใช้<wbr>งานให้เรียบร้อยแล้ว
                    <div>หากทางลูกค้ามีข้อสงสัยด้านระบบหรือการใช้งาน</div>
                    <b>
                        <font size='4'>สามารถติดต่อทีม Customer Support ได้ที่</font>
                    </b>
                    <br>
                    <b>
                        <font size='4'>
                            <font face='arial, sans-serif' color='#000000'>
                                Line Official: @EZorder (มี&nbsp;@ ด้านหน้า)&nbsp;
                            </font><br>
                        </font>
                    </b>
                    <b><font size='4'>เวลา 9.00 - 00.00 น. หรือโทร</font><br></b>
                    <br>
                    ขอบคุณค่ะ
                    <br>
                    EZorder ยินดีให้บริการค่ะ
                    <br>
                    <br>
                    <b>EZorder Customer Support Team</b>
                    <br>
                    <b style='color:rgb(136,136,136)'>
                        <a href='https://www.ez-order.co/' target='_blank'>www.ez-order.co</a>
                    </b>
                    <br>
                    <div>
                        <img src='https://static.wixstatic.com/media/9117f6_86a1eab4a68d4090a336a1d7f308336e~mv2.png/v1/fill/w_64,h_64,al_c,q_85,usm_0.66_1.00_0.01,enc_auto/9117f6_86a1eab4a68d4090a336a1d7f308336e~mv2.png' alt='EZorder' width='95' height='95' style='margin-right:0px'>
                    </div>
                </div>";
        }

    }
}
