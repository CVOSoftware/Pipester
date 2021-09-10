namespace Pipester.Protocol.Process.Steps.Interface
{
    internal interface IEndStep<TOutput> : IStep
    {
        TOutput End();
    }
}