using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Application.DTOs.Common
{
    public record ResultDTO
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

    public record ResultDTO<T>
    {
        public bool IsSuccess { get; set; } 
        public string? Message { get; set; }
        public T? Data { get; set; }


        public  static void Success(bool IsSuccess = true, string Message)
        {

        }

    }



}
