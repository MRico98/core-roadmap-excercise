using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Domain.Repositories.Base;

namespace TeamSpace.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> repository;

        public NoteService(IRepository<Note> repository)
        {
            this.repository = repository;
        }

        public Task<NoteDto> CreateAsync(NoteDto noteDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<NoteDto> GetByIdAsync(Guid id)
        {
            var note = await repository.GetByIdAsync(id);

            if (note == null) throw new NotFoundByIdException(id);

            var noteDto = new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content?? string.Empty
            };
            return noteDto;
        }

        public Task<NoteDto> UpdateAsync(NoteDto noteDto)
        {
            throw new NotImplementedException();
        }
    }
}
