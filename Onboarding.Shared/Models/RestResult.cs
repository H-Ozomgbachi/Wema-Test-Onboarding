namespace Onboarding.Shared.Models
{
    public record RestResult<T>
    {
        public T result { get; set; }
        public object errorMessage { get; set; }
        public object errorMessages { get; set; }
        public bool hasError { get; set; }
        public DateTime timeGenerated { get; set; }
    }
}
