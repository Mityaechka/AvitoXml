using AvitoXml.Wpf.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace AvitoXml.Wpf.Models
{
    public class Ad
    {
        public Dictionary<string, object> Parametrs = new Dictionary<string, object>();
        AdBlank AdBlank;
        public Ad(AdBlank adBlank)
        {
            AdBlank = adBlank;
            Parametrs.Add("Id", adBlank.Profile.Prefix);
            Parametrs.Add("Category", adBlank.Category.Name);
            Parametrs.Add("GoodsType", adBlank.Type.Name);
            Parametrs.Add("AllowEmail", "Да");
            Parametrs.Add("ManagerName", adBlank.Profile.Manager);
            Parametrs.Add("ContactPhone", adBlank.Profile.Phone);
            Parametrs.Add("Address", adBlank.Profile.Adress);
            Parametrs.Add("AdType", adBlank.AdType);
            if (adBlank.Title != null)
                Parametrs.Add("Title", adBlank.Title);
            if (adBlank.Description != null)
                Parametrs.Add("Description", adBlank.Description);
            if (adBlank.Price != null)
                Parametrs.Add("Price", adBlank.Price);
            if (adBlank.Condition != null)
                Parametrs.Add("Condition", adBlank.Condition);
            if (adBlank.StartDate != null)
                Parametrs.Add("DateBegin", adBlank.StartDate.Value.ToString("yyyy-MM-d"));
            if (adBlank.EndDate != null)
                Parametrs.Add("DateEnd", adBlank.EndDate.Value.ToString("yyyy-MM-d"));
        }
        public static string Xml(List<Ad> ads)
        {
            XmlDocument doc = new XmlDocument();
            XmlElement root = (XmlElement)doc.AppendChild(doc.CreateElement("Ads"));
            root.SetAttribute("formatVersion", "3");
            root.SetAttribute("target", "Avito.ru");
            int index = 1;
            foreach (var ad in ads)
            {
                var adXml = doc.CreateElement("Ad");
                var id = ad.AdBlank.Profile.Prefix;
                foreach (var param in ad.Parametrs)
                {
                    
                    switch (param.Key)
                    {
                        case "Id":
                            id = param.Value.ToString();
                            break;
                        case "Images":
                            var imagesXml = doc.CreateElement("Images");
                            foreach (var image in param.Value.ToString().Split("\n"))
                            {
                                var imageXml = doc.CreateElement("Image");
                                imageXml.SetAttribute("url", image);
                                imagesXml.AppendChild(imageXml);
                            }
                            adXml.AppendChild(imagesXml);
                            break;
                        default:
                            if (string.IsNullOrEmpty(param.Value.ToString()))
                                continue;
                            var e = doc.CreateElement(param.Key);
                            e.InnerText = param.Value.ToString().Trim();
                            adXml.AppendChild(e);
                            break;
                    }
                    if (id == ad.AdBlank.Profile.Prefix)
                    {
                        id += index.ToString();
                        index++;
                    }
                    
                }
                var idXml = doc.CreateElement("Id");
                if (id == ad.AdBlank.Profile.Prefix)
                {
                    id += index.ToString();
                    index++;
                }
                idXml.InnerText = id;
                adXml.AppendChild(idXml);
                root.AppendChild(adXml);
            }
            return doc.OuterXml;
        }
    }
}
/*
 public string Id { get; set; }
    public string Category { get; set; }
    public string GoodsType { get; set; }
    public string AdType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public string Condition { get; set; }
    public Dictionary<string, object> Parametrs = new Dictionary<string, object>();
    /*public DateTime? DateBegin { get; set; } = null;
    public DateTime? DateEnd { get; set; } = null;
    public string AvitoId { get; set; } = null;
    public ListingFee? ListingFee { get; set; } = null;
    public AdStatus? AdStatus { get; set; } = null;
    public AllowEmail? AllowEmail { get; set; } = null;
    public string ManagerName { get; set; } = null;
    public string ContactPhone { get; set; } = null;
    public string Address { get; set; } = null;

public List<string> Images { get; set; } = new List<string>();
public List<string> VideoUR { get; set; } = new List<string>();
 */

