﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TeamSpace.Domain.Specifications.Base;
public interface ISpecification<T>
{
    Expression<Func<T, bool>> Criteria { get; }
}