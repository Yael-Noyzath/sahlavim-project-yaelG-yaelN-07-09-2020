using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace SachlavimService.Utilities
{
    [Serializable]
    public class Direction
    {
        public Routes[] routes;
        public string status;
    }
    [Serializable]
    public class Routes
    {
        public Elements1[] legs;
        public int[] waypoint_order;
    }
    [Serializable]
    public class Elements1
    {
        public DistanceDuration distance;
        public DistanceDuration duration;
        public location start_location;
        public step[] steps;
        //  public string status;
    }
    [Serializable]
    public class step
    {
        public DistanceDuration distance;
        public DistanceDuration duration;
        public location start_location;
        public location end_location;
        public string html_instructions;
        public polyline polyline;
        //  public string status;
    }

    [Serializable]
    public class polyline
    {
        public string points { get; set; }
    }

    [Serializable]
    public class location
    {
        public string lat { get; set; }
        public string lng { get; set; }
    }


    [Serializable]
    public class MyObject
    {
        public Routes[] routes;
        public string status;
    }

    [Serializable]
    public class Rows
    {
        public Elements[] elements;
    }

    [Serializable]
    public class DistanceMatrix
    {
        public string[] destination_addresses;
        public string[] origin_addresses;
        public Rows[] rows;
        public string status;

        public static DistanceMatrix GetDistanceMatrix(int iCounter, string origins, string destinations)
        {
            string url1 = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origins + "&destinations=" + destinations + "|&language=he-IL&sensor=false&&mode=traveling&key=" + ConfigSettings.ReadSetting("DistanceMatrixKey");

            HttpWebRequest webRequest1 = (HttpWebRequest)WebRequest.Create(url1);
            HttpWebResponse webResponse1 = (HttpWebResponse)webRequest1.GetResponse();
            Encoding enc1 = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream1 = new StreamReader(webResponse1.GetResponseStream(), enc1);
            string html1 = responseStream1.ReadToEnd();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            DistanceMatrix distanceMatrix = serializer.Deserialize<DistanceMatrix>(html1);
            webResponse1.Close();
            responseStream1.Close();


            if (iCounter < 3 && distanceMatrix.rows.Count() == 0)
            {
                //LogWriter.WriteLog("null res  : " + distanceMatrix.status + "  ::" + url1, "GetDistanceMatrix");
                Thread.Sleep(1000);
                return GetDistanceMatrix(iCounter + 1, origins, destinations);
            }
            return distanceMatrix;
        }

    }

    [Serializable]
    public class DistanceDuration
    {
        public string text;
        public double value;

    }

    [Serializable]
    public class Elements
    {
        public DistanceDuration distance;
        public DistanceDuration duration;
        public DistanceDuration duration_in_traffic;
    }
}