namespace Account.CdWaddell.Services
{
    public class IdentityCertificate
    {
        public int Id { get; set; }
        public bool Active { get; set; }
        public bool Primary { get; set; }
        public byte[] FileData { get; set; }
    }
}
