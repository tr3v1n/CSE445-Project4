using System;
using System.Xml.Schema;
using System.Xml;
using Newtonsoft.Json;
using System.IO;



/**
 * This template file is created for ASU CSE445 Distributed SW Dev Assignment 4.
 * Please do not modify or delete any existing class/variable/method names. However, you can add more variables and functions.
 * Uploading this file directly will not pass the autograder's compilation check, resulting in a grade of 0.
 * **/


namespace ConsoleApp1
{


    public class Submission
    {
        public static string xmlURL = "https://tr3v1n.github.io/CSE445-Project4/NationalParks.xml";
        public static string xmlErrorURL = "https://tr3v1n.github.io/CSE445-Project4/NationalParksErrors.xml";
        public static string xsdURL = "https://tr3v1n.github.io/CSE445-Project4/NationalParks.xsd";

        public static void Main(string[] args)
        {
            string result = Verification(args[0], args[2]);
            Console.WriteLine(result);

            result = Verification(args[1], args[2]);
            Console.WriteLine(result);

            result = Xml2Json(args[0]);
            Console.WriteLine(result);
        }

        public static void Main(string[] args)
        {
            string result = Verification(xmlURL, xsdURL);
            Console.WriteLine(result);


            result = Verification(xmlErrorURL, xsdURL);
            Console.WriteLine(result);


            result = Xml2Json(xmlURL);
            Console.WriteLine(result);
        }

        // Q2.1
        public static string Verification(string xmlUrl, string xsdUrl)
        {
            //return "No Error" if XML is valid. Otherwise, return the desired exception message.
            string errorMessage = null;

            XmlSchemaSet sc = new XmlSchemaSet();

            sc.Add(null, xsdUrl);

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.Schemas = sc;

            settings.ValidationEventHandler += (sender, e) =>
            {
                errorMessage = e.Message;
            };

            try
            {
                using (XmlReader reader = XmlReader.Create(xmlUrl, settings))
                {
                    while (reader.Read()) { }
                }
            } catch (Exception e)
            {
                return e.Message;
            }
            if (errorMessage == null)
            {
                errorMessage = "No Error";
            }
            return errorMessage;
        }

        public static string Xml2Json(string xmlUrl)
        {
            // The returned jsonText needs to be de-serializable by Newtonsoft.Json package. (JsonConvert.DeserializeXmlNode(jsonText))
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlUrl);

            string jsonText = JsonConvert.SerializeXmlNode(doc);
            return jsonText;
        }

        // Helper method to download content from URL
        private static string DownloadContent(string url)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }

}