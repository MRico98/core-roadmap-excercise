using TeamSpace.Application.DTOs;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Domain.Specifications.Space;

namespace TeamSpace.Application.Services;
public class SpaceService : ISpaceService
{
    private readonly IRepository<Space> repository;

    public SpaceService(IRepository<Space> repository)
    {
        this.repository = repository;
    }

    public Task<SpaceDto> CreateAsync(SpaceDto noteDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SpaceDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SpaceDto> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<SpaceDto> UpdateAsync(SpaceDto noteDto)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<SpaceDto>> GetSpacesByUserId(Guid userId)
    {
        var spaces = await repository.ListAsync(new SpaceByOwnerId<Space>(userId));
        return spaces.Select(space => new SpaceDto
        {
            Id = space.Id,
            Name = space.Name,
            Description = space.Description
        });
    }
}
