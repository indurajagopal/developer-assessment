using System.Collections.Generic;
using System.Net;
using System;

namespace TodoList.Api.Model
{
    public class APIResponse
    {
        public APIResponse()
        {
            ErrorMessage = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String> ErrorMessage { get; set; }
        public Object Result { get; set; }
    }
}
