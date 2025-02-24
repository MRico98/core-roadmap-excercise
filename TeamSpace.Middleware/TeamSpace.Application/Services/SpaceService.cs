using System.Security.Claims;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Application.Selectors;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Domain.Specifications.Space;

namespace TeamSpace.Application.Services;
public class SpaceService(IRepository<Space> repository) : ISpaceService
{
    private readonly IRepository<Space> repository = repository;

    public async Task<SpacePostRequest> CreateAsync(SpacePostRequest spacePostRequest)
    {
        var space = new Space
        {
            Name = spacePostRequest.Name,
            Description = spacePostRequest.Description,
            Owner = spacePostRequest.Owner
        };
        var result = await repository.AddAsync(space);
        return new SpacePostRequest
        {
            Name = result.Name,
            Description = result.Description,
            Owner = result.Owner
        };
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<SpaceWithNoteDetailsGetResponse> GetByIdAsync(Guid id)
    {
        var space = await repository.GetByIdAsync(id);

        return new SpaceToSpaceWithNoteDetailsGetResponse().BuildExpression().Compile()(space);
    }

    public async Task<IEnumerable<SpaceGetResponse>> GetSpacesByUserId(Guid userId)
    {
        var spaces = await repository.ListAsync(new SpaceByOwnerId<Space>(userId));

        return spaces.Select(new SpaceToSpaceGetResponse().BuildExpression().Compile());
    }
}
