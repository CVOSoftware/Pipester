namespace Pipester.Protocol.Setting
{
    internal sealed class WorkflowSetting
    {
        public WorkflowSetting(bool isCompress)
        {
            IsCompress = isCompress;
        }

        public WorkflowSetting(string encryptionKey)
        {
            EncryptionKey = encryptionKey;
        }

        public bool IsCompress { get; }

        public string EncryptionKey { get; }
    }
}