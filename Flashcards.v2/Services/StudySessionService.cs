using Flashcards.v2.Controllers;
using Flashcards.v2.Models;
using Spectre.Console;

namespace Flashcards.v2.Services;

internal class StudySessionService
{
    internal static void InsertStudySession()
    {
        var studySession = new StudySession();
        studySession.StackId = StackService.GetStackOptionInput("Choose a stack to study").Id;
        studySession.Date = DateTime.Now;
        studySession.Score = 0;

        StudySessionGame(studySession);

        AnsiConsole.MarkupLine($"\n[bold green]Your score is {studySession.Score}[/]");
        AnsiConsole.MarkupLine($"\n[slowblink]Press any key to continue[/]");
        Console.ReadLine();

        try
        {
            StudySessionController.AddStudySession(studySession);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[bold red]An error occurred while saving the study session: {ex.Message}[/]");
        }
    }

    internal static void GetStudySession()
    {
        var studySession = GetStudySessionOptionInput();
        UserInterface.ShowStudySession(studySession);
    }

    internal static void GetStudySessions()
    {
        var studySessions = StudySessionController.GetStudySessions();
        UserInterface.ShowStudySessionsTable(studySessions);
    }

    internal static StudySession? GetStudySessionOptionInput()
    {
        var studySession = StudySessionController.GetStudySessions();

        if (studySession.Count == 0)
        {
            AnsiConsole.MarkupLine("[bold red]No study session found. Please create a study session first.[/]");
            AnsiConsole.MarkupLine("[bold red]You will be redirected to the previous menu in a moment.[/]");
            Thread.Sleep(5000);
            UserInterface.StudySessionsMenu();
            return null;
        }

        var studySessionArray = studySession.Select(x => x.Date.ToString()).ToArray();
        var option = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Choose a study session")
                .AddChoices(studySessionArray)
        );

        // what is this returning?
        return studySession.SingleOrDefault(x => x.Date.ToString() == option);
    }

    internal static void StudySessionGame(StudySession studySession)
    {
        var flashcards = FlashcardController.GetFlashcardsByStackId(studySession.StackId);

        foreach (var flashcard in flashcards)
        {
            AnsiConsole.MarkupLine($"\n[bold blue]{flashcard.Question}[/]");

            var answer = AnsiConsole.Ask<string>("What is the answer?");

            if (answer.Equals(flashcard.Answer, StringComparison.OrdinalIgnoreCase)) // Case-insensitive comparison
            {
                studySession.Score++;
                AnsiConsole.MarkupLine("[bold green]Correct![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[bold red]Incorrect. The correct answer was: {flashcard.Answer}[/]");
            }
        }
    }
}
