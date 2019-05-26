using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Models.Api
{
    public class BaseResponse
    {
        public int Status { get; set; }
        public string Description { get; set; }

    }
}