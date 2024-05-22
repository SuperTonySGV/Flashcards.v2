namespace Flashcards.v2;

internal class Enums
{
    internal enum MainMenuOptions
    {
        ManageStacks,
        ManageFlashcards,
        ManageStudySessions,
        Quit
    }
    internal enum StackMenu
    {
        AddStack,
        UpdateStack,
        DeleteStack,
        ViewStack,
        GoBack,
    }
    internal enum FlashcardMenu
    {
        AddFlashcard,
        UpdateFlashcard,
        DeleteFlashcard,
        ViewFlashcard,
        ViewAllFlashcards,
        GoBack
    }
    internal enum StudySessionMenu
    {
        StartStudySession,
        DeleteStudySession,
        ViewStudySession,
        ViewAllStudySessions,
        GoBack
    }
}
