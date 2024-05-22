using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace Flashcards.v2.Models;

[Index(nameof(Question), IsUnique = true)]

public class Flashcard
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Question { get; set; }
    [Required]
    public string Answer { get; set; }
    [ForeignKey(nameof(StackId))]
    public int StackId { get; set; }
    
    // Navigation Property:
    public Stack Stack { get; set; } // This links the StudySession to the Stack
}
