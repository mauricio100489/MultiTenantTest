namespace MultiTenantTest.Domain.Models.Responses
{
    public class ServiceResult<T>
    {
        public IReadOnlyDictionary<string, IEnumerable<string>> ValidationMessages { get; protected set; }

        public bool Succeeded { get; protected set; }

        public T Result { get; set; }

        public ServiceResult(T result, bool succeeded, Dictionary<string, IEnumerable<string>> errors)
        {
            Result = result;
            Succeeded = succeeded;
            ValidationMessages = errors;
        }

        public static ServiceResult<T> ErrorResult(string[] errors)
        {
            return new(default, false, new Dictionary<string, IEnumerable<string>> { { string.Empty, errors.Select(e => e) } });
        }
        public static ServiceResult<T> ErrorResult(string error)
        {
            return new(default, false, new Dictionary<string, IEnumerable<string>> { { string.Empty, new[] { error } } });
        }

        public string ErrorMessage()
        {
            var errores = ValidationMessages.Select(v => v.Value.First()).ToList();
            var messages = string.Join(".", errores);
            return messages;
        }

        public static ServiceResult<T> SuccessResult(T result) =>
            new ServiceResult<T>(result, true, new Dictionary<string, IEnumerable<string>>());
    }
}
