using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiaKooshar.Domain.Enums;

public enum ErrorType
{
    Success,
    Failure,
    NotFound,
    Unauthorized,
    Forbid,
    ValidationError,
    Conflict,
    BadRequest
}
