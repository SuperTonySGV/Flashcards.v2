﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Flashcards.v2.Models;

[Index(nameof(Name), IsUnique = true)]
public class Stack
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Flashcard> Flashcards { get; set; }
}
