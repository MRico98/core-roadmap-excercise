using System.Linq.Expressions;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Selectors.Base;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Selectors;

internal sealed class UserPostRequestToUser : Selector<UserPostRequest, User>
{
    public override Expression<Func<UserPostRequest, User>> BuildExpression() =>
        x => new User
        {
            Id = Guid.NewGuid(),
            UserName = x.Username,
            Email = x.Email,
            PhoneNumber = x.PhoneNumber,
            CreatedAt = DateTime.Now,
            RoleId = x.RoleId,
        };
}