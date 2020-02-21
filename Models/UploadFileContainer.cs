using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BarMenuBoardAPI.Models
{
    public class UploadFileContainer
    {
        public IFormFile File { get; set; }
    }
}
