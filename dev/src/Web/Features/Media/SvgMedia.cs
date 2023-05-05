using EPiServer.DataAbstraction;
using EPiServer.DataAnnotations;
using EPiServer.Framework.Blobs;
using EPiServer.Framework.DataAnnotations;
using System.Xml;

namespace Perficient.Web.Features.Media
{
    [ContentType(GUID = "{E8AF3E78-E1A2-4812-ADA9-2DF4866C14AB}", DisplayName = "SVG Media")]
    [MediaDescriptor(ExtensionString = "svg")]
    public class SvgMedia : ImageMediaData
    {
        public override Blob Thumbnail { get => BinaryData; }

        public virtual string XML
        {
            get
            {
                try
                {
                    var blob = BinaryData;
                    var xmlDoc = new XmlDocument();
                    xmlDoc.Load(blob.OpenRead());

                    if (!string.IsNullOrWhiteSpace(base.AltText))
                    {
                        var titleNode = xmlDoc.GetElementsByTagName("title")[0];

                        if (titleNode == null) // add a title if one doesn't already exist
                        {
                            //Create a new node.
                            titleNode = xmlDoc.CreateElement("title");
                            xmlDoc.DocumentElement.AppendChild(titleNode);
                        }

                        // set title to the alt text from the CMS
                        titleNode.InnerText = base.AltText;
                    }

                    return xmlDoc.InnerXml;
                }
                catch
                {
                    //If this fails, dont cause a 5xx error, fail gracefully.
                    return "";
                }
            }
        }
    }
}
