using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>:DataResult<T>
    {
        //ctor                 //istersen data ver, mesaj ver
        public SuccessDataResult(T data,string message):base(data,true,message)
        {
            //tüm bilgileri verdiği versiyonu
        }
                              
        public SuccessDataResult(T data):base(data,true)
        {                       //ister sadece data ver

        }                    
        public SuccessDataResult(string message):base(default,true,message)
        {                       //ister sadece mesaj ver

        }
        public SuccessDataResult():base(default,true)
        {                      //istersen hiçbir şey verme

        }
    }
}
// son 2 ctoru pek kullanmayız