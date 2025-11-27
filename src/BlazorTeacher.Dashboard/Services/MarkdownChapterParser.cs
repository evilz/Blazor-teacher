using System.Text.RegularExpressions;
using BlazorTeacher.Shared.Models;
using Markdig;
using Markdig.Syntax;

namespace BlazorTeacher.Dashboard.Services;

/// <summary>
/// Parses markdown files into Chapter objects.
/// </summary>
public static partial class MarkdownChapterParser
{
    /// <summary>
    /// Parses a markdown file content into a Chapter object.
    /// </summary>
    /// <param name="markdownContent">The raw markdown content including YAML frontmatter.</param>
    /// <returns>A parsed Chapter object.</returns>
    public static Chapter Parse(string markdownContent)
    {
        var (frontMatter, body) = SplitFrontMatterAndBody(markdownContent);
        var chapter = ParseFrontMatter(frontMatter);
        ParseBody(body, chapter);
        return chapter;
    }

    private static (string frontMatter, string body) SplitFrontMatterAndBody(string content)
    {
        var lines = content.Split('\n');
        var frontMatterLines = new List<string>();
        var bodyLines = new List<string>();
        
        bool inFrontMatter = false;
        bool frontMatterEnded = false;
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            if (!inFrontMatter && !frontMatterEnded && trimmedLine == "---")
            {
                inFrontMatter = true;
                continue;
            }
            
            if (inFrontMatter && trimmedLine == "---")
            {
                inFrontMatter = false;
                frontMatterEnded = true;
                continue;
            }
            
            if (inFrontMatter)
            {
                frontMatterLines.Add(line);
            }
            else if (frontMatterEnded)
            {
                bodyLines.Add(line);
            }
        }
        
        return (string.Join('\n', frontMatterLines), string.Join('\n', bodyLines));
    }

    private static Chapter ParseFrontMatter(string frontMatter)
    {
        var chapter = new Chapter();
        var lines = frontMatter.Split('\n');
        string? currentListKey = null;
        
        foreach (var line in lines)
        {
            var trimmedLine = line.Trim();
            
            // Check for list items
            if (trimmedLine.StartsWith("- ") && currentListKey != null)
            {
                var listValue = trimmedLine[2..].Trim().Trim('"');
                AddToList(chapter, currentListKey, listValue);
                continue;
            }
            
            // Check for key-value pairs
            var colonIndex = trimmedLine.IndexOf(':');
            if (colonIndex > 0)
            {
                var key = trimmedLine[..colonIndex].Trim();
                var value = trimmedLine[(colonIndex + 1)..].Trim().Trim('"');
                
                // Check if this is a list start (empty value)
                if (string.IsNullOrEmpty(value))
                {
                    currentListKey = key;
                    continue;
                }
                
                currentListKey = null;
                SetProperty(chapter, key, value);
            }
        }
        
        return chapter;
    }

    private static void SetProperty(Chapter chapter, string key, string value)
    {
        switch (key.ToLowerInvariant())
        {
            case "id":
                if (int.TryParse(value, out var id))
                    chapter.Id = id;
                break;
            case "number":
                if (int.TryParse(value, out var number))
                    chapter.Number = number;
                break;
            case "title":
                chapter.Title = value;
                break;
            case "description":
                chapter.Description = value;
                break;
            case "route":
                chapter.Route = value;
                break;
            case "category":
                chapter.Category = ParseCategory(value);
                break;
        }
    }

    private static void AddToList(Chapter chapter, string key, string value)
    {
        switch (key.ToLowerInvariant())
        {
            case "topics":
                chapter.Topics.Add(value);
                break;
            case "keypoints":
                chapter.KeyPoints.Add(value);
                break;
        }
    }

    private static ChapterCategory ParseCategory(string value)
    {
        return value.ToLowerInvariant() switch
        {
            "introduction" => ChapterCategory.Introduction,
            "setup" => ChapterCategory.Setup,
            "apidevelopment" => ChapterCategory.ApiDevelopment,
            "blazorbasics" => ChapterCategory.BlazorBasics,
            "components" => ChapterCategory.Components,
            "statemanagement" => ChapterCategory.StateManagement,
            "dashboard" => ChapterCategory.Dashboard,
            "advanced" => ChapterCategory.Advanced,
            "wrapup" => ChapterCategory.WrapUp,
            "csharpfundamentals" => ChapterCategory.CSharpFundamentals,
            "csharptypes" => ChapterCategory.CSharpTypes,
            "dataaccess" => ChapterCategory.DataAccess,
            "nextsteps" => ChapterCategory.NextSteps,
            _ => ChapterCategory.Introduction
        };
    }

    private static void ParseBody(string body, Chapter chapter)
    {
        var sections = SplitByH2(body);
        
        foreach (var section in sections)
        {
            if (section.Title.StartsWith("Step", StringComparison.OrdinalIgnoreCase))
            {
                var step = ParseStep(section);
                chapter.Steps.Add(step);
            }
            else if (section.Title.Equals("Quiz", StringComparison.OrdinalIgnoreCase))
            {
                chapter.Quiz = ParseQuiz(section.Content);
            }
        }
    }

    private static List<(string Title, string Content)> SplitByH2(string body)
    {
        var result = new List<(string Title, string Content)>();
        var h2Regex = H2Regex();
        var matches = h2Regex.Matches(body);
        
        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            var title = match.Groups[1].Value.Trim();
            var startIndex = match.Index + match.Length;
            var endIndex = i + 1 < matches.Count ? matches[i + 1].Index : body.Length;
            var content = body[startIndex..endIndex].Trim();
            result.Add((title, content));
        }
        
        return result;
    }

    private static ChapterStep ParseStep((string Title, string Content) section)
    {
        var step = new ChapterStep();
        
        // Parse step title (remove "Step N: " prefix)
        var titleMatch = StepTitleRegex().Match(section.Title);
        step.Title = titleMatch.Success ? titleMatch.Groups[1].Value.Trim() : section.Title;
        
        // Parse step type from **Type: xxx**
        var typeMatch = StepTypeRegex().Match(section.Content);
        if (typeMatch.Success)
        {
            var typeValue = typeMatch.Groups[1].Value.Trim().ToLowerInvariant();
            step.Type = typeValue == "action" ? StepType.Action : StepType.Read;
        }
        
        // Get content after the type line
        var contentLines = section.Content.Split('\n');
        var contentBuilder = new List<string>();
        bool typeLinePassed = false;
        
        foreach (var line in contentLines)
        {
            if (!typeLinePassed && line.TrimStart().StartsWith("**Type:"))
            {
                typeLinePassed = true;
                continue;
            }
            if (typeLinePassed)
            {
                contentBuilder.Add(line);
            }
        }
        
        step.Content = string.Join('\n', contentBuilder).Trim();
        
        // Remove the separator "---" at the end if present
        if (step.Content.EndsWith("---"))
        {
            step.Content = step.Content[..^3].TrimEnd();
        }
        
        return step;
    }

    private static Quiz ParseQuiz(string content)
    {
        var quiz = new Quiz();
        var questionSections = SplitByH3(content);
        
        foreach (var section in questionSections)
        {
            if (section.Title.StartsWith("Question", StringComparison.OrdinalIgnoreCase))
            {
                var question = ParseQuestion(section.Content);
                quiz.Questions.Add(question);
            }
        }
        
        return quiz;
    }

    private static List<(string Title, string Content)> SplitByH3(string body)
    {
        var result = new List<(string Title, string Content)>();
        var h3Regex = H3Regex();
        var matches = h3Regex.Matches(body);
        
        for (int i = 0; i < matches.Count; i++)
        {
            var match = matches[i];
            var title = match.Groups[1].Value.Trim();
            var startIndex = match.Index + match.Length;
            var endIndex = i + 1 < matches.Count ? matches[i + 1].Index : body.Length;
            var content = body[startIndex..endIndex].Trim();
            result.Add((title, content));
        }
        
        return result;
    }

    private static QuizQuestion ParseQuestion(string content)
    {
        var question = new QuizQuestion();
        var lines = content.Split('\n');
        
        // First non-empty line is the question text
        var textLines = new List<string>();
        var optionStartIndex = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            var trimmedLine = lines[i].Trim();
            if (trimmedLine.StartsWith("- "))
            {
                optionStartIndex = i;
                break;
            }
            if (!string.IsNullOrWhiteSpace(trimmedLine))
            {
                textLines.Add(trimmedLine);
            }
        }
        
        question.Text = string.Join(' ', textLines);
        
        // Parse options
        for (int i = optionStartIndex; i < lines.Length; i++)
        {
            var line = lines[i].Trim();
            if (line.StartsWith("- "))
            {
                question.Options.Add(line[2..].Trim().Trim('"'));
            }
            else if (line.StartsWith("**Correct:"))
            {
                var correctStr = line.Replace("**Correct:", "").Replace("**", "").Trim();
                if (int.TryParse(correctStr, out var correctIndex))
                {
                    question.CorrectOptionIndex = correctIndex;
                }
            }
            else if (line.StartsWith("**Explanation:"))
            {
                question.Explanation = line.Replace("**Explanation:", "").Replace("**", "").Trim();
            }
        }
        
        return question;
    }

    [GeneratedRegex(@"^## (.+)$", RegexOptions.Multiline)]
    private static partial Regex H2Regex();

    [GeneratedRegex(@"^### (.+)$", RegexOptions.Multiline)]
    private static partial Regex H3Regex();

    [GeneratedRegex(@"Step \d+:\s*(.+)")]
    private static partial Regex StepTitleRegex();

    [GeneratedRegex(@"\*\*Type:\s*(\w+)\*\*", RegexOptions.IgnoreCase)]
    private static partial Regex StepTypeRegex();
}
