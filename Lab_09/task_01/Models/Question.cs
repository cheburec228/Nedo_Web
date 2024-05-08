using System.Collections.Generic;

public class TestQuestion
{
    public string Text { get; set; }
    public QuestionType Type { get; set; }
    public List<Option> Options { get; set; }
}

public class Option
{
    public string Text { get; set; }
}

public enum QuestionType
{
    SingleChoice,
    MultipleChoice,
    Text
}
