using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GourmetGo.Domain.DTOs.usuarios
{
    public class AuthResponseDto
    {

        public bool Success { get; set; }
        public string? Token { get; set; }
        public string Message { get; set; } = string.Empty;

    }
}
