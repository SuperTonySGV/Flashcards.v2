using Flashcards.v2.Controllers;
using Flashcards.v2.Mappers;
using Flashcards.v2.Models;
using Spectre.Console;

namespace Flashcards.v2.Services;

internal class FlashcardService
{
    internal static void InsertFlashcard()
    {
        var flashcard = new Flashcard();
        flashcard.StackId = StackService.GetStackOptionInput("Add this flashcard to a stack").Id;
        flashcard.Question = AnsiConsole.Ask<string>("What is the flashcard question?");
        flashcard.Answer = AnsiConsole.Ask<string>("What is the flashcard answer?");
        FlashcardController.AddFlashcard(flashcard);
    }

    internal static void UpdateFlashcard()
    {
        var flashcard = GetFlashcardOptionInput();
        flashcard.Question = AnsiConsole.Confirm("Update flashcard question?") ? AnsiConsole.Ask<string>("What is the new question?") : flashcard.Question;
        flashcard.Answer = AnsiConsole.Confirm("Update flashcard answer?") ? AnsiConsole.Ask<string>("What is the new anwer?") : flashcard.Answer;

        FlashcardController.UpdateFlashcard(flashcard);
    }

    internal static void DeleteFlashcard()
    {
        var flashcard = GetFlashcardOptionInput();
        FlashcardController.DeleteFlashcard(flashcard);
    }

    internal static void GetFlashcard()
    {
        var flashcard = GetFlashcardOptionInput().ToFlashcardDto();
        UserInterface.ShowFlashcard(flashcard);
    }

    internal static void GetFlashcards()
    {
        var flashcards = FlashcardController.GetFlashcards();
        UserInterface.ShowFlashcardsTable(flashcards);
    }

    internal static Flashcard? GetFlashcardOptionInput()
    {
        var flashcards = FlashcardController.GetFlashcards();

        if (flashcards.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]No flashcards found. Please create a flashcard first.[/]");
            AnsiConsole.MarkupLine("[bold red]You will be redirected to the previous menu in a moment.[/]");
            UserInterface.FlashcardsMenu();
        }

        var flashcardsArray = flashcards.Select(x => x.Question).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a flashcard")
                .AddChoices(flashcardsArray)
        );

        return flashcards.SingleOrDefault(x => x.Question == option);
    }
}
