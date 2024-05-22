using Flashcards.v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.v2.Controllers;

internal class FlashcardController
{
    internal static void AddFlashcard(Flashcard flashcard)
    {
        using var db = new ApplicationDBContext();
        db.Add(flashcard);
        db.SaveChanges();
    }

    internal static void DeleteFlashcard(Flashcard flashcard)
    {
        using var db = new ApplicationDBContext();
        db.Remove(flashcard);
        db.SaveChanges();
    }

    internal static List<Flashcard> GetFlashcards()
    {
        using var db = new ApplicationDBContext();
        var flashcards = db.Flashcards
            .Include(fc => fc.Stack)
            .ToList();
        return flashcards;
    }

    public static List<Flashcard> GetFlashcardsByStackId(int stackId)
    {
        using var db = new ApplicationDBContext();
        return db.Flashcards.Where(f => f.StackId == stackId).ToList();
    }


    internal static void UpdateFlashcard(Flashcard flashcard)
    {
        using var db = new ApplicationDBContext();
        db.Update(flashcard);
        db.SaveChanges();
    }
}
