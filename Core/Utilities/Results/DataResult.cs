using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public class DataResult<T> : Result, IDataResult<T>
    {//ctor la açtık contructor ımızı
        public DataResult(T data,bool success,string message):base(success,message)
        {                 //_carDal.GetAll() bir data olduğu için parametre olarak data vereceğimizi belirtiyoruz "T data " yazarken. Resulttan farkı da bu aslında. Data veriyor olmamız.
            Data = data;
        }

        public DataResult(T data,bool success):base(success)
        {
            Data = data;    //set ettik
        }

        public T Data { get; }
    }
}
