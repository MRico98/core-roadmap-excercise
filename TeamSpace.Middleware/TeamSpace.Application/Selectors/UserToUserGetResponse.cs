using System.Linq.Expressions;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Selectors.Base;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Selectors;

internal sealed class UserToUserGetResponse : Selector<User, UserGetResponse>
{
    public override Expression<Func<User, UserGetResponse>> BuildExpression() =>
        x => new ()
        {
            Id = x.Id,
            Username = x.UserName,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            CreatedAt = x.CreatedAt,
        };
}