using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace analytics.Models.DTO
{
    public class OperationResult
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        private string _type="operation";
        public string Type { get{ return _type;} }
        public OperationResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
        public OperationResult(bool isSuccess, string message, object data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
        public static OperationResult Success()
        {
            return new OperationResult(true, "La operacion se realizó con éxito");
        }

        public static OperationResult Success(string Message)
        {
            return new OperationResult(true, Message);
        }
        public static OperationResult Failure(string Message)
        {
            return new OperationResult(false, Message);
        }
        public static OperationResult Success(string Message, object Data)
        {
            return new OperationResult(true, Message, Data);
        }
        public static OperationResult Failure(string Message, object Data)
        {
            return new OperationResult(false, Message, Data);
        }
        public static OperationResult ValidationFailure(string Message, string field)
        {
            OperationResult or = new OperationResult(false, Message);
            or._type = "validation";
            Dictionary<string, List<string>> data = new Dictionary<string, List<string>>();
            data.Add(field, new List<string>() { Message });
            or.Data = data;
            return or;
        }

        public static OperationResult ValidationFailure(string Message, Dictionary<string,List<string>> errors)
        {
            OperationResult or = new OperationResult(false, Message);
            or._type = "validation";
            or.Data = errors;
            return or;
        }
    }
}