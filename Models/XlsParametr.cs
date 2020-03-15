using System;
using System.Collections.Generic;
using System.Text;

namespace AvitoXml.Wpf.Models
{
    public class AvitoParametr
    {
        public string AvitoXml { get; set; }
        public string AvitoRu { get; set; }

        public AvitoParametr(string avitoXml, string avitoRu)
        {
            AvitoXml = avitoXml;
            AvitoRu = avitoRu;
        }
    }
    public class XlsParametr
    {
        public int XlsIndex { get; set; }
        public string XlsHeader { get; set; }

        public XlsParametr(int xlsIndex, string xlsHeader)
        {
            XlsIndex = xlsIndex;
            XlsHeader = xlsHeader;
        }
    }
    public class ImportParametr
    {
        public AvitoParametr AvitoParametr { get; set; }
        public XlsParametr XlsParametr { get; set; }
    }
}
