using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace HCI_Tim10_Putovanja.Core
{
    class MapService
    {
        static HttpClient client = new HttpClient();

        static async Task<RootObject> GetLocationAsync(string path)
        {
            RootObject lr = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                lr = await response.Content.ReadAsAsync<RootObject>();
            }
            return lr;
        }

        public static async Task<string> GetAddress(double lat, double lon)
        {
            try
            {
                string p = "http://dev.virtualearth.net/REST/v1/Locations/" + lat + "," + lon + "?&key=";
                //client.BaseAddress = new Uri("http://dev.virtualearth.net/REST/v1/Locations/{point}?&key={BingMapsKey}");
                Console.WriteLine(p);
                client.BaseAddress = new Uri(p);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            
                RootObject lr = await GetLocationAsync("");
                Console.WriteLine(lr.statusCode);
                if (lr.statusCode == 200
                    && lr.resourceSets.Length > 0
                    && lr.resourceSets[0].resources.Length > 0)
                {
                    return lr.resourceSets[0].resources[0].name;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

    }

    public class RootObject
    {
        public string authenticationResultCode { get; set; }
        public string brandLogoUri { get; set; }
        public string copyright { get; set; }
        public ResourceSet[] resourceSets { get; set; }
        public int statusCode { get; set; }
        public string statusDescription { get; set; }
        public string traceId { get; set; }
    }

    public class ResourceSet
    {
        public int estimatedTotal { get; set; }
        public LocationR[] resources { get; set; }
    }

    public class LocationR
    {
        public string __type { get; set; }
        public double[] bbox { get; set; }
        public string name { get; set; }
        public PointR point { get; set; }
        public Address address { get; set; }
        public string confidence { get; set; }
        public string entityType { get; set; }
        public GeocodePoint[] geocodePoints { get; set; }
        public string[] matchCodes { get; set; }
    }

    public class PointR
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
    }

    public class Address
    {
        public string addressLine { get; set; }
        public string adminDistrict { get; set; }
        public string adminDistrict2 { get; set; }
        public string countryRegion { get; set; }
        public string formattedAddress { get; set; }
        public string locality { get; set; }
        public string postalCode { get; set; }
    }

    public class GeocodePoint
    {
        public string type { get; set; }
        public double[] coordinates { get; set; }
        public string calculationMethod { get; set; }
        public string[] usageTypes { get; set; }
    }

}
