using HCI_Tim10_Putovanja.User;
using HCI_Tim10_Putovanja.User.View;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
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

        public static async void GetAddressForCreateTrip(double lat, double lon, TripDataContext tdt, Boolean isStartLocation, CreateTrip updateTripPage)
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
    [DataContract]
    public class ResourceSet2
    {
        [DataMember(Name = "estimatedTotal", EmitDefaultValue = false)]
        public long EstimatedTotal { get; set; }

        [DataMember(Name = "resources", EmitDefaultValue = false)]
        public Resource[] Resources { get; set; }
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

    public class Response
    {
        [DataMember(Name = "copyright", EmitDefaultValue = false)]
        public string Copyright { get; set; }

        [DataMember(Name = "brandLogoUri", EmitDefaultValue = false)]
        public string BrandLogoUri { get; set; }

        [DataMember(Name = "statusCode", EmitDefaultValue = false)]
        public int StatusCode { get; set; }

        [DataMember(Name = "statusDescription", EmitDefaultValue = false)]
        public string StatusDescription { get; set; }

        [DataMember(Name = "authenticationResultCode", EmitDefaultValue = false)]
        public string AuthenticationResultCode { get; set; }

        [DataMember(Name = "errorDetails", EmitDefaultValue = false)]
        public string[] errorDetails { get; set; }

        [DataMember(Name = "traceId", EmitDefaultValue = false)]
        public string TraceId { get; set; }

        [DataMember(Name = "resourceSets", EmitDefaultValue = false)]
        public ResourceSet2[] ResourceSets { get; set; }
    }
    [DataContract]
    [KnownType(typeof(Location))]
    [KnownType(typeof(Route))]
    public class Resource
    {
        [DataMember(Name = "bbox", EmitDefaultValue = false)]
        public double[] BoundingBox { get; set; }

        [DataMember(Name = "__type", EmitDefaultValue = false)]
        public string Type { get; set; }
    }


    [DataContract(Namespace = "http://schemas.microsoft.com/search/local/ws/rest/v1")]
    public class Route : Resource
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        [DataMember(Name = "distanceUnit", EmitDefaultValue = false)]
        public string DistanceUnit { get; set; }

        [DataMember(Name = "durationUnit", EmitDefaultValue = false)]
        public string DurationUnit { get; set; }

        [DataMember(Name = "travelDistance", EmitDefaultValue = false)]
        public double TravelDistance { get; set; }

        [DataMember(Name = "travelDuration", EmitDefaultValue = false)]
        public double TravelDuration { get; set; }

        [DataMember(Name = "routeLegs", EmitDefaultValue = false)]
        public RouteLeg[] RouteLegs { get; set; }

        [DataMember(Name = "routePath", EmitDefaultValue = false)]
        public RoutePath RoutePath { get; set; }
    }

    [DataContract]
    public class RouteLeg
    {
        [DataMember(Name = "travelDistance", EmitDefaultValue = false)]
        public double TravelDistance { get; set; }

        [DataMember(Name = "travelDuration", EmitDefaultValue = false)]
        public double TravelDuration { get; set; }

        [DataMember(Name = "actualStart", EmitDefaultValue = false)]
        public Point2 ActualStart { get; set; }

        [DataMember(Name = "actualEnd", EmitDefaultValue = false)]
        public Point2 ActualEnd { get; set; }

        [DataMember(Name = "startLocation", EmitDefaultValue = false)]
        public Location StartLocation { get; set; }

        [DataMember(Name = "endLocation", EmitDefaultValue = false)]
        public Location EndLocation { get; set; }

        [DataMember(Name = "itineraryItems", EmitDefaultValue = false)]
        public ItineraryItem[] ItineraryItems { get; set; }
    }

    [DataContract]
    public class RoutePath
    {
        [DataMember(Name = "line", EmitDefaultValue = false)]
        public Line Line { get; set; }

        [DataMember(Name = "generalizations", EmitDefaultValue = false)]
        public Generalization[] Generalizations { get; set; }
    }
    [DataContract]
    public class Generalization
    {
        [DataMember(Name = "pathIndices", EmitDefaultValue = false)]
        public int[] PathIndices { get; set; }

        [DataMember(Name = "latLongTolerance", EmitDefaultValue = false)]
        public double LatLongTolerance { get; set; }
    }

    [DataContract]
    public class Point2 : Shape
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>  
        /// Latitude,Longitude  
        /// </summary>  
        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public double[] Coordinates { get; set; }

        [DataMember(Name = "calculationMethod", EmitDefaultValue = false)]
        public string CalculationMethod { get; set; }

        [DataMember(Name = "usageTypes", EmitDefaultValue = false)]
        public string[] UsageTypes { get; set; }
    }

    [DataContract]
    [KnownType(typeof(Point2))]
    public class Shape
    {
        [DataMember(Name = "boundingBox", EmitDefaultValue = false)]
        public double[] BoundingBox { get; set; }
    }

    [DataContract]
    public class Line
    {
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        [DataMember(Name = "coordinates", EmitDefaultValue = false)]
        public double[][] Coordinates { get; set; }
    }

    [DataContract]
    public class ItineraryItem
    {
        [DataMember(Name = "childItineraryItems", EmitDefaultValue = false)]
        public ItineraryItem[] ChildItineraryItems { get; set; }

        [DataMember(Name = "compassDirection", EmitDefaultValue = false)]
        public string CompassDirection { get; set; }

        [DataMember(Name = "details", EmitDefaultValue = false)]
        public Object[] Details { get; set; }

        [DataMember(Name = "exit", EmitDefaultValue = false)]
        public string Exit { get; set; }

        [DataMember(Name = "hints", EmitDefaultValue = false)]
        public Object[] Hints { get; set; }

        [DataMember(Name = "iconType", EmitDefaultValue = false)]
        public string IconType { get; set; }

        [DataMember(Name = "instruction", EmitDefaultValue = false)]
        public Object Instruction { get; set; }

        [DataMember(Name = "maneuverPoint", EmitDefaultValue = false)]
        public Point2 ManeuverPoint { get; set; }

        [DataMember(Name = "sideOfStreet", EmitDefaultValue = false)]
        public string SideOfStreet { get; set; }

        [DataMember(Name = "signs", EmitDefaultValue = false)]
        public string[] Signs { get; set; }

        [DataMember(Name = "time", EmitDefaultValue = false)]
        public string Time { get; set; }

        [DataMember(Name = "tollZone", EmitDefaultValue = false)]
        public string TollZone { get; set; }

        [DataMember(Name = "towardsRoadName", EmitDefaultValue = false)]
        public string TowardsRoadName { get; set; }

        [DataMember(Name = "transitLine", EmitDefaultValue = false)]
        public Object TransitLine { get; set; }

        [DataMember(Name = "transitStopId", EmitDefaultValue = false)]
        public int TransitStopId { get; set; }

        [DataMember(Name = "transitTerminus", EmitDefaultValue = false)]
        public string TransitTerminus { get; set; }

        [DataMember(Name = "travelDistance", EmitDefaultValue = false)]
        public double TravelDistance { get; set; }

        [DataMember(Name = "travelDuration", EmitDefaultValue = false)]
        public double TravelDuration { get; set; }

        [DataMember(Name = "travelMode", EmitDefaultValue = false)]
        public string TravelMode { get; set; }

        [DataMember(Name = "warning", EmitDefaultValue = false)]
        public Object[] Warning { get; set; }
    }

}
