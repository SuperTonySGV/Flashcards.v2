using Flashcards.v2.Controllers;
using Flashcards.v2.Models;
using Spectre.Console;

namespace Flashcards.v2.Services;

internal class StackService
{
    internal static void InsertStack()
    {
        var stack = new Stack();
        stack.Name = AnsiConsole.Ask<string>("What do you want to name your new stack?");

        try
        {
            StackController.AddStack(stack);
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[bold red]{ex.Message}[/]");
            InsertStack();
        }
    }

    internal static void UpdateStack()
    {
        var stack = GetStackOptionInput();
        stack.Name = AnsiConsole.Confirm("Update stack name?") ? AnsiConsole.Ask<string>("What is the new name?") : stack.Name;
        StackController.UpdateStack(stack);
    }

    internal static void DeleteStack()
    {
        var stack = GetStackOptionInput();
        StackController.DeleteStack(stack);
    }

    internal static void GetStack()
    {
        var stack = GetStackOptionInput();
        UserInterface.ShowStack(stack);
    }

    internal static void GetStacks()
    {
        var stacks = StackController.GetStacks();
        UserInterface.ShowStackTable(stacks);
    }

    internal static bool StacksExist()
    {
        return StackController.GetStacks().Count > 0;
    }

    internal static Stack GetStackOptionInput(string defaultMessage = "Choose a stack")
    {
        var stacks = StackController.GetStacks();
        var stacksArray = stacks.Select(x => x.Name).ToArray();
        var option = AnsiConsole.Prompt(new SelectionPrompt<string>()
            .Title(defaultMessage)
            .AddChoices(stacksArray));
        var stack = stacks.Single(x => x.Name == option);

        return stack;
    }
}
