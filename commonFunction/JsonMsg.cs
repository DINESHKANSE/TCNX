using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCNX.commonFunction
{
    public class JsonMsg
    {
        public static ErrorResponse SuccessResponse(string msg = "", object data = null, Int64 tNoOfRecord = 0)
        {
            if (msg == "")
                msg = "Request completed successfully.";
            ErrorResponse res = new ErrorResponse();
            res.ResponseCode = 101;
            res.ResponseStatus = "success";
            res.ResponseMessage = msg;
            res.NoOfRecord = tNoOfRecord;
            res.Data = data;
            return res;
        }

        public static ErrorResponse ErrorResponse(string msg = "", Int64 tNoOfRecord = 0)
        {
            if (msg == "")
                msg = "Request failed with error.";
            ErrorResponse res = new ErrorResponse();
            res.ResponseCode = 404;
            res.ResponseStatus = "error";
            res.ResponseMessage = msg;
            res.NoOfRecord = tNoOfRecord;
            res.Data = null;
            return res;
        }

        public static ErrorResponse LoginResponse(string msg = "", string Data = "", Int64 tNoOfRecord = 0)
        {
            if (msg == "")
                msg = "Request failed with error.";
            if (Data == "")
                Data = "/Login/Login";
            ErrorResponse res = new ErrorResponse();
            res.ResponseCode = 555;
            res.ResponseStatus = "error";
            res.ResponseMessage = msg;
            res.NoOfRecord = tNoOfRecord;
            res.Data = Data;
            return res;
        }
    }

    #region ErrorResponse
    public class ErrorResponse
    {
        public Int64 ResponseCode { get; set; }
        public string ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public Int64 NoOfRecord { get; set; }
        public object Data { get; set; }
    }
    #endregion
}