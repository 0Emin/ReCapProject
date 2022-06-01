namespace Business.Constants
{
    //Constant: sabit demek
    //newlemek zorunda kalmamak için static verdik
    public static class Messages
    {
        public static string CarAdded = "Araba Eklendi";
        public static string CarNameInvalid = "Araba ismi geçersiz";
        public static string MaintenanceTime = "Sistem bakımda";
        public static string CarsListed = "Arabalar Listelendi";
        public static string CarCountOfBrandError = "Bir markada en fazla 10 araba olabilir";
        public static string CarUpdated = "Araba bilgileri güncellendi";
        public static string CarNameAlreadyExist = "Bu isme sahip başka bir araba zaten mevcut";
        public static string BrandLimitExceded = "Marka sayısı maksimum limite (15) ulaştı";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string UserRegistered = "Kayıt oldu";
        public static string UserNotFound = "Kullanıcı bulunamadı";
        public static string PasswordError = "Parola hatası";
        public static string SuccessfulLogin = "Başarılı giriş";
        public static string UserAlreadyExists = "Kullanıcı mevcut";
        public static string AccessTokenCreated = "Token oluşturuldu";
    }
}
