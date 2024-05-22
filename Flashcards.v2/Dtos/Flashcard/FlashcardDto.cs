using Flashcards.v2.Models;

namespace Flashcards.v2.Dtos.Flashcard;

public class FlashcardDto
{
    public int Id { get; set; }
    public string Question { get; set; }
    public string Answer { get; set; }

    // Navigation Property:
    public Stack Stack { get; set; } // This links the StudySession to the Stack
}
