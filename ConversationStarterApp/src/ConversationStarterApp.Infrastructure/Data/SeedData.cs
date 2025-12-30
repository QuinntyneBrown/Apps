// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using ConversationStarterApp.Core;

namespace ConversationStarterApp.Infrastructure.Data;

/// <summary>
/// Provides seed data for the ConversationStarterApp system.
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Gets sample prompts for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with custom prompts (null for system prompts).</param>
    /// <returns>A collection of sample prompts.</returns>
    public static IEnumerable<Prompt> GetSamplePrompts(Guid? userId = null)
    {
        return new List<Prompt>
        {
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = null,
                Text = "What's something that made you smile today?",
                Category = Category.Icebreaker,
                Depth = Depth.Surface,
                Tags = "positive, simple, everyday",
                IsSystemPrompt = true,
                UsageCount = 45,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = null,
                Text = "If you could have dinner with anyone from history, who would it be and why?",
                Category = Category.Hypothetical,
                Depth = Depth.Moderate,
                Tags = "history, imagination, values",
                IsSystemPrompt = true,
                UsageCount = 78,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = null,
                Text = "What's the most important lesson life has taught you so far?",
                Category = Category.Deep,
                Depth = Depth.Deep,
                Tags = "wisdom, reflection, growth",
                IsSystemPrompt = true,
                UsageCount = 92,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = null,
                Text = "Describe a perfect day in your ideal life 10 years from now.",
                Category = Category.DreamsAndAspirations,
                Depth = Depth.Deep,
                Tags = "future, goals, dreams",
                IsSystemPrompt = true,
                UsageCount = 67,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = null,
                Text = "What's your go-to karaoke song?",
                Category = Category.Fun,
                Depth = Depth.Surface,
                Tags = "music, fun, entertainment",
                IsSystemPrompt = true,
                UsageCount = 34,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = null,
                Text = "What values do you prioritize most in your relationships?",
                Category = Category.ValuesAndBeliefs,
                Depth = Depth.Deep,
                Tags = "values, relationships, priorities",
                IsSystemPrompt = true,
                UsageCount = 53,
                CreatedAt = DateTime.UtcNow.AddDays(-90)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = userId,
                Text = "What's a childhood memory that still brings you joy?",
                Category = Category.PastExperiences,
                Depth = Depth.Moderate,
                Tags = "childhood, nostalgia, memories",
                IsSystemPrompt = false,
                UsageCount = 5,
                CreatedAt = DateTime.UtcNow.AddDays(-30)
            },
            new()
            {
                PromptId = Guid.NewGuid(),
                UserId = userId,
                Text = "If you could master any skill instantly, what would it be?",
                Category = Category.Hypothetical,
                Depth = Depth.Moderate,
                Tags = "skills, dreams, hypothetical",
                IsSystemPrompt = false,
                UsageCount = 8,
                CreatedAt = DateTime.UtcNow.AddDays(-25)
            }
        };
    }

    /// <summary>
    /// Gets sample favorites for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with favorites.</param>
    /// <param name="promptId">The prompt ID to favorite.</param>
    /// <returns>A collection of sample favorites.</returns>
    public static IEnumerable<Favorite> GetSampleFavorites(Guid userId, Guid promptId)
    {
        return new List<Favorite>
        {
            new()
            {
                FavoriteId = Guid.NewGuid(),
                PromptId = promptId,
                UserId = userId,
                Notes = "Great for getting to know new people",
                CreatedAt = DateTime.UtcNow.AddDays(-20)
            }
        };
    }

    /// <summary>
    /// Gets sample sessions for seeding.
    /// </summary>
    /// <param name="userId">The user ID to associate with sessions.</param>
    /// <returns>A collection of sample sessions.</returns>
    public static IEnumerable<Session> GetSampleSessions(Guid userId)
    {
        return new List<Session>
        {
            new()
            {
                SessionId = Guid.NewGuid(),
                UserId = userId,
                Title = "Date Night Conversation",
                StartTime = DateTime.UtcNow.AddDays(-7),
                EndTime = DateTime.UtcNow.AddDays(-7).AddHours(2),
                Participants = "Me and my partner",
                PromptsUsed = "3 deep questions, 2 fun questions",
                Notes = "Had a great conversation about our future goals. Learned new things about each other.",
                WasSuccessful = true,
                CreatedAt = DateTime.UtcNow.AddDays(-7)
            },
            new()
            {
                SessionId = Guid.NewGuid(),
                UserId = userId,
                Title = "Family Dinner Conversation",
                StartTime = DateTime.UtcNow.AddDays(-3),
                EndTime = DateTime.UtcNow.AddDays(-3).AddMinutes(45),
                Participants = "Family of 4",
                PromptsUsed = "4 icebreaker questions",
                Notes = "Kids really enjoyed the hypothetical questions. Made dinner time more engaging.",
                WasSuccessful = true,
                CreatedAt = DateTime.UtcNow.AddDays(-3)
            },
            new()
            {
                SessionId = Guid.NewGuid(),
                UserId = userId,
                Title = "Coffee with Friends",
                StartTime = DateTime.UtcNow.AddDays(-1),
                EndTime = DateTime.UtcNow.AddDays(-1).AddHours(1),
                Participants = "3 friends",
                PromptsUsed = "5 moderate depth questions",
                Notes = "Sparked interesting discussions about values and life experiences. Everyone contributed.",
                WasSuccessful = true,
                CreatedAt = DateTime.UtcNow.AddDays(-1)
            }
        };
    }
}
