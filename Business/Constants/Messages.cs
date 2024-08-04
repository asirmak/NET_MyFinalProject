using Core.Entities.Concrete;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün Eklendi";
        public static string ProductListed = "Ürün Listelendi";
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string ProductCategoryMaximumNumber = "Bir kategoride maksimum 10 ürün olabilir.";
        public static string ProductNameAlreadyExists = "Aynı isime sahip başka bir ürün var.";
        public static string ProductCategoryLimitExceeded = "Kategori limiti aşıldı.";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        internal static string UserRegistered = "Kullanıcı kayıt oldu.";
        internal static string UserNotFound = "Kullanıcı bulunamadı.";
        internal static string PasswordError = "Şifre hatalı";
        internal static string SuccessfulLogin = "Başarılı giriş";
        internal static string UserAlreadyExists = "Kullanıcı zaten var.";
        internal static string AccessTokenCreated = "Token oluşturuldu";
    }
}
