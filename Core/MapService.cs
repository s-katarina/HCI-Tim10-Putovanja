using HCI_Tim10_Putovanja.User;
using HCI_Tim10_Putovanja.User.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static HCI_Tim10_Putovanja.User.View.UpdateTouristicStop;

namespace HCI_Tim10_Putovanja.Core
{
    class MapService
    {
        static HttpClient client = new HttpClient();

        public static void SetUpService()
        {
            client.BaseAddress = new Uri("http://dev.virtualearth.net/REST/v1/Locations/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
        }

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

        public static async void GetAddressForUpdateTrip(double lat, double lon, TripDataContext tdt, Boolean isStartLocation, UpdateTrip updateTripPage)
        {
            {
                RootObject lr = await GetLocationAsync(lat + "," + lon + "?&key=SnHYMam6TI3eih8XdGcM~O9u2ALoPJSaWq00iBIY_gQ~AloMVoJvFusZA7hMcea7h0eqZ0f7EkNT5VkUGBz_WOP9oYxgem-Dm5h4JnC_hILn");
                if (lr.statusCode == 200
                    && lr.resourceSets.Length > 0
                    && lr.resourceSets[0].resources.Length > 0)
                {
                    updateTripPage.Tdt = tdt;
                    if (isStartLocation)
                    {
                        tdt.Trip = new User.Trip(tdt.Trip, new User.Location(lr.resourceSets[0].resources[0].point.coordinates[0], lr.resourceSets[0].resources[0].point.coordinates[1], lr.resourceSets[0].resources[0].name), true);
                        updateTripPage.txtStartLocation.Text = tdt.startAddress;
                    }
                    else
                    {
                        tdt.Trip = new User.Trip(tdt.Trip, new User.Location(lr.resourceSets[0].resources[0].point.coordinates[0], lr.resourceSets[0].resources[0].point.coordinates[1], lr.resourceSets[0].resources[0].name));
                        updateTripPage.txtEndLocation.Text = tdt.endAddress;
                    }
                }
            }
        }

        public static async void GetAddressForUpdateTouristicStop(double lat, double lon, TuristicStops ts, UpdateTouristicStop page)
        {
            {
                RootObject lr = await GetLocationAsync(lat + "," + lon + "?&key=SnHYMam6TI3eih8XdGcM~O9u2ALoPJSaWq00iBIY_gQ~AloMVoJvFusZA7hMcea7h0eqZ0f7EkNT5VkUGBz_WOP9oYxgem-Dm5h4JnC_hILn");
                if (lr.statusCode == 200
                    && lr.resourceSets.Length > 0
                    && lr.resourceSets[0].resources.Length > 0)
                {
                    ts.Location = new User.Location(lr.resourceSets[0].resources[0].point.coordinates[0], lr.resourceSets[0].resources[0].point.coordinates[1], lr.resourceSets[0].resources[0].name);
                    page.txtLocation.Text = ts.Location.Address;
                 }
            }
        }

        public static async void GetAddressForCreateTouristicStop(double lat, double lon, TSDataContext tsc, CreateTouristicStop page)
        {
            {
                RootObject lr = await GetLocationAsync(lat + "," + lon + "?&key=SnHYMam6TI3eih8XdGcM~O9u2ALoPJSaWq00iBIY_gQ~AloMVoJvFusZA7hMcea7h0eqZ0f7EkNT5VkUGBz_WOP9oYxgem-Dm5h4JnC_hILn");
                if (lr.statusCode == 200
                    && lr.resourceSets.Length > 0
                    && lr.resourceSets[0].resources.Length > 0)
                {
                    tsc.CoordinatesString = lr.resourceSets[0].resources[0].point.coordinates[0] + "," + lr.resourceSets[0].resources[0].point.coordinates[1];
                    tsc.LocationAddress = lr.resourceSets[0].resources[0].name;
                    page.txtLocation.Text = lr.resourceSets[0].resources[0].name;
                }
            }
        }

        public static async void GetAddressForCreateAttraction(double lat, double lon, CreateAttraction page)
        {
            {
                RootObject lr = await GetLocationAsync(lat + "," + lon + "?&key=SnHYMam6TI3eih8XdGcM~O9u2ALoPJSaWq00iBIY_gQ~AloMVoJvFusZA7hMcea7h0eqZ0f7EkNT5VkUGBz_WOP9oYxgem-Dm5h4JnC_hILn");
                if (lr.statusCode == 200
                    && lr.resourceSets.Length > 0
                    && lr.resourceSets[0].resources.Length > 0)
                {
                    page.CoordinatesString = lr.resourceSets[0].resources[0].point.coordinates[0] + "," + lr.resourceSets[0].resources[0].point.coordinates[1];
                    page.txtLocation.Text = lr.resourceSets[0].resources[0].name;
                }
            }
        }

        public static async void GetAddressForUpdateAttraction(double lat, double lon, UpdateAttraction page)
        {
            {
                RootObject lr = await GetLocationAsync(lat + "," + lon + "?&key=SnHYMam6TI3eih8XdGcM~O9u2ALoPJSaWq00iBIY_gQ~AloMVoJvFusZA7hMcea7h0eqZ0f7EkNT5VkUGBz_WOP9oYxgem-Dm5h4JnC_hILn");
                if (lr.statusCode == 200
                    && lr.resourceSets.Length > 0
                    && lr.resourceSets[0].resources.Length > 0)
                {
                    page.CoordinatesString = lr.resourceSets[0].resources[0].point.coordinates[0] + "," + lr.resourceSets[0].resources[0].point.coordinates[1];
                    page.txtLocation.Text = lr.resourceSets[0].resources[0].name;
                }
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
