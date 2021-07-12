using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleJwt.Models.Responses
{
    public class ResponseDto<T> where T : class
    {
        public int Status { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}
