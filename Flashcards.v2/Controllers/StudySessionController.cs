using Flashcards.v2.Models;
using Microsoft.EntityFrameworkCore;

namespace Flashcards.v2.Controllers;

internal class StudySessionController
{
    internal static void AddStudySession(StudySession studySession)
    {
        using var db = new ApplicationDBContext();
        db.Add(studySession);
        db.SaveChanges();
    }

    internal static void DeleteStudySession(StudySession studySession)
    {
        using var db = new ApplicationDBContext();
        db.Remove(studySession);
        db.SaveChanges();
    }

    internal static List<StudySession> GetStudySessions()
    {
        using var db = new ApplicationDBContext();
        var studySessions = db.StudySessions
            .Include(ss => ss.Stack)
            .ToList();
        return studySessions;
    }
}
