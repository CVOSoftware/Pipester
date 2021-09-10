namespace Pipester.Protocol.Process.Steps.Interface
{
    internal interface IExecutableStep : IStep
    {
        void Execute();

        IStep Next();
    }
}