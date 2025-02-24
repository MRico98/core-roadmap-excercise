using System.Linq.Expressions;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Selectors.Base;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Selectors;

internal sealed class SpacePostRequestToSpace : Selector<SpacePostRequest, Space>
{
    public override Expression<Func<SpacePostRequest, Space>> BuildExpression() =>
        x => new Space
        {
            Id = Guid.NewGuid(),
            Name = x.Name,
            Description = x.Description,
            Owner = x.Owner
        };
}