using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        
        //overloading uyguladık. Her zaman mesaj vermek istemeyebiliriz diye.
        public Result(bool success, string message):this(success)
        {
            Message= message;
            
        }

        public Result(bool success)
        {
            
            Success = success;
        }

        public bool Success { get; }

        public string Message { get; }
    }
}

//sadece get; olduğu için return olayını gerçekleştircek, implemente ettiğimizde bize verdiği "throw.." lu olan kısma. Biz de o kısma lambdalar da dahil silip {get;} yazdık.
//OverDesign yani aşırı tasarım olmasın diye Abstract ve Concrete klasörleri açmak yerinde aynı klasörde oluşturdum IResult ve Result ü