using Flashcards.v2.Dtos.Flashcard;
using Flashcards.v2.Models;

namespace Flashcards.v2.Mappers;

public static class FlashcardMappers
{
    public static FlashcardDto ToFlashcardDto(this Flashcard flashcardModel)
    {
        return new FlashcardDto
        {
            Id = flashcardModel.Id,
            Question = flashcardModel.Question,
            Answer = flashcardModel.Answer,
            Stack = flashcardModel.Stack
        };
    }
}
