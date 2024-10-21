using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Application.DTOs;
using TeamSpace.Application.Services.Base;
using TeamSpace.Middleware.Controllers;

namespace TeamSpace.Middleware.Test.Controllers
{
    public class NotesControllerTests
    {
        private readonly Mock<INoteService> _noteServiceMock;
        private readonly NotesController _controller;

        public NotesControllerTests()
        {
            _noteServiceMock = new Mock<INoteService>();
            _controller = new NotesController(_noteServiceMock.Object);
        }

        [Fact]
        public async Task GetNotes_ReturnsOkObjectResult()
        {
            //Arrange
            var notes = new List<NoteDto>
            {
                new NoteDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Note 1",
                    Content = "Content 1"
                },
                new NoteDto
                {
                    Id = Guid.NewGuid(),
                    Title = "Note 2",
                    Content = "Content 2"
                }
            };

            _noteServiceMock.Setup(service => service.GetAllAsync()).ReturnsAsync(notes);

            //Act
            var result = await _controller.GetNotes();
            
            //Assert
            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var returnNotes = okResult.Value.Should().BeAssignableTo<IEnumerable<NoteDto>>().Subject;

            returnNotes.Count().Should().Be(2);
        }
    }
}
