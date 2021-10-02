namespace DagAir.IngestionNode.Data.Influx
{
    public struct InfluxConfiguration
    {
        public InfluxConfiguration(char[] token, string org, string orgId, string bucketName, string url, int retention)
        {
            Token = token;
            Org = org;
            OrgId = orgId;
            BucketName = bucketName;
            Url = url;
            Retention = retention;
        }
        public char[] Token { get; }
        public string Org { get; }
        public string OrgId { get; }
        public string BucketName { get; }
        public string Url { get; }
        public int Retention { get; }
    }
}