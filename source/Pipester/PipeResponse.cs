namespace Pipester
{
    public sealed class PipeResponse<T> where T : class
    {
        internal PipeResponse() { }

        internal PipeResponse(T value)
        {
            if(value != null)
            {
                IsSuccessul = true;
                Value = value;
            }
        }

        public bool IsSuccessul { get; }

        public T Value { get; }
    }
}