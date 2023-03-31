namespace Master.Shared.ResultDtos
{
    public class ValidationErrorDto
    {
        public string Field { get; }

        public string Message { get; }

        public ValidationErrorDto(string field, string message)
        {
            Field = field != string.Empty ? field : null;
            Message = message;
        }
    }
}
