using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.DTOs.Requests;
using TeamSpace.Application.Services.Base;
using TeamSpace.Domain.Entities;
using TeamSpace.Domain.Exceptions;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Domain.Specifications.Note;

namespace TeamSpace.Application.Services
{
    public class NoteService : INoteService
    {
        private readonly IRepository<Note> repository;

        public NoteService(IRepository<Note> repository)
        {
            this.repository = repository;
        }

        public async Task<NoteDto> CreateAsync(NotePostRequest noteDto)
        {
            Note noteDatabase = new Note
            {
                Id = Guid.NewGuid(),
                Title = noteDto.Title,
                Content = noteDto.Content,
                SpaceId = new Guid("ccfff139-a86d-4001-96f5-bce00e707d01"),
                CreatedBy = new Guid("5750bf75-9551-49e7-a5f6-1867bc2c4ce8")
            };
            var resultEntity = await repository.AddAsync(noteDatabase);
            await repository.SaveChangesAsync();
            NoteDto noteResult = new NoteDto
            {
                Id = resultEntity.Id,
                Title = resultEntity.Title,
                Content = resultEntity.Content
            };
            return noteResult;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await repository.DeleteAsync(id);
            await repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<NoteDto>> GetBySpaceIdAsync(Guid spaceId)
        {
            var notes = await repository.ListAsync(new NoteBySpaceId(spaceId));
            var notesDto = new List<NoteDto>();
            foreach (var note in notes)
            {
                var noteDto = new NoteDto
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content ?? string.Empty
                };
                notesDto.Add(noteDto);
            }
            return notesDto;
        }

        public async Task<IEnumerable<NoteDto>> GetAllAsync()
        {
            var notes = await repository.ListAllAsync();
            var notesDto = new List<NoteDto>();
            foreach (var note in notes)
            {
                var noteDto = new NoteDto
                {
                    Id = note.Id,
                    Title = note.Title,
                    Content = note.Content ?? string.Empty
                };
                notesDto.Add(noteDto);
            }
            return notesDto;
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
    }
}
