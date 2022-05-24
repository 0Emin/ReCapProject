using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    //Temel voidler için başlangıç

    //işlem sonucu dönecek
    //bilgilendirme yapılacak
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}
