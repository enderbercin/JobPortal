using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Domain
{
    public class BaseServiceResponse
    {
        public BaseServiceResponse()
        {
            Success = true;
            Status = 200;
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string>();
        }

        public BaseServiceResponse(bool success)
        {
            this.Success = success;
            if (success)
                this.Status = 200;
            else
                this.Status = 500;
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string>();
        }

        public BaseServiceResponse(bool success, int statusCode)
        {
            this.Success = success;
            this.Status = statusCode;
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string>();
        }

        public BaseServiceResponse(bool success, HttpStatusCode statusCode)
        {
            this.Success = success;
            this.Status = (int)statusCode;
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string>();
        }

        public BaseServiceResponse(Exception exception)
        {
            this.Success = false;
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string> {
                exception.Message
            };
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
                ExceptionList.Add(exception.Message);
            }
        }

        public BaseServiceResponse(bool success, IEnumerable<Exception> exceptions)
        {
            this.Success = success;
            this.Status = success ? 200 : 500;
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string>();
            foreach (var exception in exceptions)
            {
                ExceptionList.Add(exception.Message);
                var innerEx = exception;
                while (innerEx.InnerException != null)
                {
                    innerEx = innerEx.InnerException;
                    ExceptionList.Add(innerEx.Message);
                }
            }
        }

        public BaseServiceResponse(params string[] resultDescriptionList)
        {
            ResultDescriptionList = new List<string>();
            ExceptionList = new List<string>();
            this.Success = false;
            this.ResultDescriptionList = resultDescriptionList.ToList();
        }

  
        public bool Success { get; set; }

      
        public int Status { get; set; }

        public IList<String> ResultDescriptionList { get; set; }

        public IList<String> ExceptionList { get; set; }
    }

    public class BaseServiceResponse<T> : BaseServiceResponse
    {
        public BaseServiceResponse() : base(true)
        {
        }
        public BaseServiceResponse(T obj) : base(true)
        {
            this.Data = obj;
        }
        public BaseServiceResponse(T[] obj) : base(true)
        {
            this.DataList = obj.ToList();
        }
        public BaseServiceResponse(List<T> obj) : base(true)
        {
            this.DataList = obj;
        }
        public BaseServiceResponse(IEnumerable<T> obj) : base(true)
        {
            this.DataList = obj.ToList();
        }
        public BaseServiceResponse(bool success, T obj) : base(success)
        {
            this.Data = obj;
        }
        public BaseServiceResponse(bool success, int status, T obj) : base(success)
        {
            this.Data = obj;
            this.Status = status;
        }
        public BaseServiceResponse(bool success, T[] obj) : base(success)
        {
            this.DataList = obj.ToList();
        }
        public BaseServiceResponse(bool success, IEnumerable<T> obj) : base(success)
        {
            this.DataList = obj.ToList();
        }

        public BaseServiceResponse(Exception exception) : base(exception)
        {
        }

        public BaseServiceResponse(params string[] resultDescriptionList) : base(resultDescriptionList)
        {
        }

        public List<T> DataList { get; set; }

        public T Data { get; set; }
    }

    public class BaseServiceResponse<T, TList> : BaseServiceResponse
    {
        public BaseServiceResponse() : base(true)
        {
        }
        public BaseServiceResponse(T obj) : base(true)
        {
            this.Data = obj;
        }
        public BaseServiceResponse(TList[] obj) : base(true)
        {
            this.DataList = obj.ToList();
        }
        public BaseServiceResponse(T obj, TList[] objList) : base(true)
        {
            this.Data = obj;
            this.DataList = objList.ToList();
        }
        public BaseServiceResponse(T obj, List<TList> objList) : base(true)
        {
            this.Data = obj;
            this.DataList = objList;
        }
        public BaseServiceResponse(T obj, IEnumerable<TList> objList) : base(true)
        {
            this.Data = obj;
            this.DataList = objList.ToList();
        }
        public BaseServiceResponse(List<TList> obj) : base(true)
        {
            this.DataList = obj;
        }
        public BaseServiceResponse(IEnumerable<TList> obj) : base(true)
        {
            this.DataList = obj.ToList();
        }
        public BaseServiceResponse(bool success, T obj) : base(success)
        {
            this.Data = obj;
        }
        public BaseServiceResponse(bool success, TList[] obj) : base(success)
        {
            this.DataList = obj.ToList();
        }
        public BaseServiceResponse(bool success, IEnumerable<TList> obj) : base(success)
        {
            this.DataList = obj.ToList();
        }

        public BaseServiceResponse(Exception exception) : base(exception)
        {
        }

        public BaseServiceResponse(params string[] resultDescriptionList) : base(resultDescriptionList)
        {
        }

        public List<TList> DataList { get; set; }
        public T Data { get; set; }
    }
}
