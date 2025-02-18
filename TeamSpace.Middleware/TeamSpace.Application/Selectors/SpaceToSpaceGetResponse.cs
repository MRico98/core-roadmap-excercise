using System.Linq.Expressions;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Selectors.Base;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Selectors;

internal sealed class SpaceToSpaceGetResponse : Selector<Space, SpaceGetResponse>
{
    public override Expression<Func<Space, SpaceGetResponse>> BuildExpression() =>
        x => new SpaceGetResponse
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            Name = x.Name,
            Description = x.Description,
            Owner = x.Owner
        };
}