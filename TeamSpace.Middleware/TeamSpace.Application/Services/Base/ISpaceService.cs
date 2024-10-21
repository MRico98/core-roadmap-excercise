using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Application.DTOs;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Services.Base;
    
public interface ISpaceService    
{
    Task<IEnumerable<SpaceDto>> GetAllAsync();
    Task<SpaceDto> GetByIdAsync(Guid id);
    Task<SpaceDto> CreateAsync(SpaceDto noteDto);
    Task<SpaceDto> UpdateAsync(SpaceDto noteDto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<SpaceDto>> GetSpacesByUserId(Guid id);
}
