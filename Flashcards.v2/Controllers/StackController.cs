using Flashcards.v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.v2.Controllers;

internal class StackController
{
    internal static void AddStack(Stack stack)
    {
        using var db = new ApplicationDBContext();
        if (db.Stacks.Any(s => s.Name.ToLower() == stack.Name.ToLower()))
        {
            throw new ArgumentException("A stack with that name already exists.");
        }
        db.Add(stack);
        db.SaveChanges();
    }

    internal static void DeleteStack(Stack stack)
    {
        using var db = new ApplicationDBContext();
        db.Remove(stack);
        db.SaveChanges();
    }

    internal static List<Stack> GetStacks()
    {
        using var db = new ApplicationDBContext();
        var stacks = db.Stacks
            .Include(x => x.Flashcards)
            .ToList();
        return stacks;
    }

    internal static void UpdateStack(Stack stack)
    {
        using var db = new ApplicationDBContext();
        db.Update(stack);
        db.SaveChanges();
    }
}
