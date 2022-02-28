using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorecaShared.Dtos
{
    public class BaseResponseDto
    {
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
    }
}