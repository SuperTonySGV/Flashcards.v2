using Flashcards.v2.Dtos.Flashcard;
using Flashcards.v2.Models;
using Flashcards.v2.Services;
using Spectre.Console;
using static Flashcards.v2.Enums;

namespace Flashcards.v2;

static internal class UserInterface
{
    static internal void MainMenu()
    {
        var isAppRunning = true;
        while (isAppRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("What would you like to do?")
                .AddChoices(
                    MainMenuOptions.ManageStacks,
                    MainMenuOptions.ManageFlashcards,
                    MainMenuOptions.ManageStudySessions,
                    MainMenuOptions.Quit));

            switch (option)
            {
                case MainMenuOptions.ManageStacks:
                    StacksMenu();
                    break;
                case MainMenuOptions.ManageFlashcards:
                    FlashcardsMenu();
                    break;
                case MainMenuOptions.ManageStudySessions:
                    StudySessionsMenu();
                    break;
                case MainMenuOptions.Quit:
                    isAppRunning = false;
                    break;
            }
        }
    }

    static internal void StacksMenu()
    {
        var isStacksMenuRunning = true;
        while (isStacksMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<StackMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    StackMenu.AddStack,
                    StackMenu.UpdateStack,
                    StackMenu.DeleteStack,
                    StackMenu.ViewStack,
                    StackMenu.GoBack));

            switch (option)
            {
                case StackMenu.AddStack:
                    StackService.InsertStack();
                    break;
                case StackMenu.UpdateStack:
                    StackService.UpdateStack();
                    break;
                case StackMenu.DeleteStack:
                    StackService.DeleteStack();
                    break;
                case StackMenu.ViewStack:
                    StackService.GetStack();
                    break;
                case StackMenu.GoBack:
                    MainMenu();
                    break;
            }
        }
    }

    static internal void FlashcardsMenu()
    {
        if (!StackService.StacksExist())
        {
            AnsiConsole.MarkupLine("[bold red]No stacks found. Please create a stack first by going to ManageStacks.[/]");
            AnsiConsole.MarkupLine("[bold red]You will be redirected back to the main menu in a moment.[/]");
            Thread.Sleep(3000); // Block the thread for 3 seconds
            MainMenu();
        }
        var isFlashcardMenuRunning = true;
        while (isFlashcardMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<FlashcardMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    FlashcardMenu.AddFlashcard,
                    FlashcardMenu.UpdateFlashcard,
                    FlashcardMenu.DeleteFlashcard,
                    FlashcardMenu.ViewFlashcard,
                    FlashcardMenu.ViewAllFlashcards,
                    FlashcardMenu.GoBack));

            switch (option)
            {
                case FlashcardMenu.AddFlashcard:
                    FlashcardService.InsertFlashcard();
                    break;
                case FlashcardMenu.UpdateFlashcard:
                    FlashcardService.UpdateFlashcard();
                    break;
                case FlashcardMenu.DeleteFlashcard:
                    FlashcardService.DeleteFlashcard();
                    break;
                case FlashcardMenu.ViewFlashcard:
                    FlashcardService.GetFlashcard();
                    break;
                case FlashcardMenu.ViewAllFlashcards:
                    FlashcardService.GetFlashcards();
                    break;
                case FlashcardMenu.GoBack:
                    MainMenu();
                    break;
            }
        }
    }

    static internal void StudySessionsMenu()
    {
        if (!StackService.StacksExist())
        {
            AnsiConsole.MarkupLine("[bold red]No stacks found. Please create a stack first by going to ManageStacks.[/]");
            AnsiConsole.MarkupLine("[bold red]You will be redirected back to the main menu in a moment.[/]");
            Thread.Sleep(3000); // Block the thread for 3 seconds
            MainMenu();
        }
        var isStudySessionMenuRunning = true;
        while (isStudySessionMenuRunning)
        {
            Console.Clear();
            var option = AnsiConsole.Prompt(
                new SelectionPrompt<StudySessionMenu>()
                .Title("What would you like to do?")
                .AddChoices(
                    StudySessionMenu.StartStudySession,
                    StudySessionMenu.ViewStudySession,
                    StudySessionMenu.ViewAllStudySessions,
                    StudySessionMenu.GoBack));

            switch (option)
            {
                case StudySessionMenu.StartStudySession:
                    StudySessionService.InsertStudySession();
                    break;
                case StudySessionMenu.ViewStudySession:
                    StudySessionService.GetStudySession();
                    break;
                case StudySessionMenu.ViewAllStudySessions:
                    StudySessionService.GetStudySessions();
                    break;
                case StudySessionMenu.GoBack:
                    MainMenu();
                    break;
            }
        }
    }

    internal static void ShowFlashcard(FlashcardDto flashcardDto)
    {
        var panel = new Panel($"Id: {flashcardDto.Id}\nQuestion: {flashcardDto.Question}\nAnswer: {flashcardDto.Answer}\nStack Name: {flashcardDto.Stack.Name}");
        //panel.Header = new PanelHeader($"{flashcard.Question}");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowFlashcardsTable(List<Flashcard> flashcards)
    {
        var table = new Table().Title("Flashcards");
        table.AddColumn("Id");
        table.AddColumn("Question");
        table.AddColumn("Answer");
        table.AddColumn("Stack Name");

        for (int i = 0; i < flashcards.Count; i++)
        {
            var flashcard = flashcards[i];
            table.AddRow(
                (i + 1).ToString(),
                flashcard.Question,
                flashcard.Answer,
                flashcard.Stack.Name
            );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ShowStack(Stack stack)
    {
        var panel = new Panel($"Id: {stack.Id}\nName: {stack.Name}\nStack Count: {stack.Flashcards.Count}");
        panel.Header = new PanelHeader($"{stack.Name}");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        ShowFlashcardsTable(stack.Flashcards);
    }

    static internal void ShowStackTable(List<Stack> stacks)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Name");

        foreach (var stack in stacks)
        {
            table.AddRow(stack.Id.ToString(), stack.Name);
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    internal static void ShowStudySession(StudySession studySession)
    {
        var panel = new Panel($"Id: {studySession.Id}\nDate: {studySession.Date.ToString()}\nScore: {studySession.Score.ToString()}");
        panel.Header = new PanelHeader($"{studySession.Date}");
        panel.Padding = new Padding(2, 2, 2, 2);

        AnsiConsole.Write(panel);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }

    static internal void ShowStudySessionsTable(List<StudySession> studySessions)
    {
        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Date");
        table.AddColumn("Score");
        table.AddColumn("Stack Name");

        foreach (var studySession in studySessions)
        {
            table.AddRow(
                studySession.Id.ToString(),
                studySession.Date.ToString(),
                studySession.Score.ToString(),
                studySession.Stack?.Name ?? "Unknown Stack"
            );
        }

        AnsiConsole.Write(table);

        Console.WriteLine("Press any key to continue");
        Console.ReadLine();
        Console.Clear();
    }
}
