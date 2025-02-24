using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.DTOs.Responses;
using TeamSpace.Domain.Entities;

namespace TeamSpace.Application.Services.Base;
    
public interface ISpaceService    
{
    Task<SpaceWithNoteDetailsGetResponse> GetByIdAsync(Guid id);
    Task<SpacePostRequest> CreateAsync(SpacePostRequest noteDto);
    Task<bool> DeleteAsync(Guid id);
    Task<IEnumerable<SpaceGetResponse>> GetSpacesByUserId(Guid id);
}
