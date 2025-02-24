using System.Linq.Expressions;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Selectors.Base;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Selectors;

internal sealed class SpaceToSpaceWithNoteDetailsGetResponse : Selector<Space, SpaceWithNoteDetailsGetResponse>
{
    public override Expression<Func<Space, SpaceWithNoteDetailsGetResponse>> BuildExpression() =>
        x => new SpaceWithNoteDetailsGetResponse
        {
            Id = x.Id,
            CreatedAt = x.CreatedAt,
            Name = x.Name,
            Description = x.Description,
            Owner = x.Owner,
            Notes = x.Notes.Select(x => new NoteDto
            {
                Id = x.Id,
                Title = x.Title,
                Content = x.Content,
            }).ToList()
        };
}