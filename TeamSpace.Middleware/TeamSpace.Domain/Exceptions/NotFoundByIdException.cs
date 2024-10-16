using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSpace.Domain.Exceptions;

public class NotFoundByIdException : Exception
{
    public NotFoundByIdException(object id)
        : base($"Entity with id {id} not found")
    {
    }
}
