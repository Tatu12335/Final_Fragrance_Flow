namespace Fragrance_flow_DL_VERSION_.models
{
    // The api location model,
    // I know i dont need all this data from the users ip but i decided to keep it just in case 
    public class IpLocation
    {

        public string status { get; set; }
        public string country { get; set; }
        public string countryCode { get; set; }
        public string region { get; set; }
        public string regionName { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public float lat { get; set; }
        public float lon { get; set; }
        public string timezone { get; set; }
        public string isp { get; set; }
        public string org { get; set; }

    }
}
