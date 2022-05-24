using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        //ctor                 //istersen data ver, mesaj ver
        public ErrorDataResult(T data, string message) : base(data, false, message)
        {
            //tüm bilgileri verdiği versiyonu
        }

        public ErrorDataResult(T data) : base(data, false)
        {                       //ister sadece data ver

        }
        public ErrorDataResult(string message) : base(default, false, message)
        {                       //ister sadece mesaj ver

        }
        public ErrorDataResult() : base(default, false)
        {                      //istersen hiçbir şey verme

        }
    }
}
