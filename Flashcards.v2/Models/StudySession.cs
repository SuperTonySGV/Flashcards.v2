using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flashcards.v2.Models;

internal class StudySession
{
    [Key]
    public int Id { get; set; }
    [Required]
    public DateTime Date { get; set; }
    [Required]
    public int Score { get; set; }
    [ForeignKey(nameof(StackId))]
    public int StackId { get; set; }
    
    // Navigation Property:
    public Stack Stack { get; set; } // This links the StudySession to the Stack


}
