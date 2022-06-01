using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.JWT
{
    public class AccessToken  //Kullanıcı sistemde yetki gerektiren bir şey yapmak isterse client bu tokeni pakete koyar ve api ye gönderir. Buna AccessToken denir
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; } // Token in biteceği zaman
    }
}
