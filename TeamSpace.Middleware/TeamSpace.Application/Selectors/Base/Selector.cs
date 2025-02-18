using System.Linq.Expressions;

namespace TeamSpace.Application.Selectors.Base;

public abstract class Selector<T, TU>
{
    public abstract Expression<Func<T, TU>> BuildExpression();
}