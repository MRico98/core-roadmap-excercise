﻿using System.ComponentModel.DataAnnotations;

namespace TeamSpace.Middleware.DTOs.Requests;
public class NotePostRequest
{
    [Required(ErrorMessage = "Title is required")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Content is required")]
    public string Content { get; set; }
}
