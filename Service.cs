using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace small_prgrms
{
    class Service
    {
        public int CheckXml(string inputXml)
        {
            /*
              <InputDocument>
	            <DeclarationList>
		            <Declaration Command="DEFAULT" Version="5.13">
			            <DeclarationHeader>
				            <Jurisdiction>IE</Jurisdiction>
				            <CWProcedure>IMPORT</CWProcedure>
							            <DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>
				            <DocumentRef>71Q0019681</DocumentRef>
				            <SiteID>DUB</SiteID>
				            <AccountCode>G0779837</AccountCode>
			            </DeclarationHeader>
		            </Declaration>
	            </DeclarationList>
            </InputDocument>
              */

            string masterXml = "<InputDocument>"
                + "<DeclarationList>"
                 + "<Declaration Command='DEFAULT' Version='5.13'>"
                        + "<DeclarationHeader>"
                            + "<Jurisdiction>IE</Jurisdiction>"
                            + "<CWProcedure>IMPORT</CWProcedure>"
                                        + "<DeclarationDestination>CUSTOMSWAREIE</DeclarationDestination>"
                            + "<DocumentRef>71Q0019681</DocumentRef>"
                            + "<SiteID>DUB</SiteID>"
                            + "<AccountCode>G0779837</AccountCode>"
                        + "</DeclarationHeader>"
                    + "</Declaration>"
                + "</DeclarationList>"
            + "</InputDocument>";

            //string masterXml = File.ReadAllText(@"C:\Users\Me\Documents\xml\master.xml");
            //inputXml = File.ReadAllText(@"C:\Users\Me\Documents\xml\input.xml");

            int result = 0;
            XmlDocument doc = new XmlDocument();
            var xmldiff = new XmlDiff();
            var r1 = XmlReader.Create(new StringReader(masterXml));
            var r2 = XmlReader.Create(new StringReader(inputXml));
            var sw = new StringWriter();
            var xw = new XmlTextWriter(sw) { Formatting = Formatting.Indented };

            xmldiff.Options = XmlDiffOptions.IgnorePI |
                XmlDiffOptions.IgnoreChildOrder |
                XmlDiffOptions.IgnoreComments |
                XmlDiffOptions.IgnoreWhitespace;
            if (!xmldiff.Compare(r1, r2, xw))
            {
                XmlNode node;
                //var xmlStr = File.ReadAllText(@"C:\Users\Me\Documents\xml\first.xml");
                doc.LoadXml(inputXml);

                string xpath1 = "InputDocument/DeclarationList/Declaration";
                //XmlNode node1 = doc.SelectSingleNode(xpath1);
                node = doc.SelectSingleNode(xpath1);
                //var value2 = node.Attributes[0].Value;
                if (node.Attributes[0].Value != "DEFAULT")
                {
                    result = -1;
                }

                string xpath = "InputDocument/DeclarationList/Declaration/DeclarationHeader/SiteID";
                //XmlNode node = doc.SelectSingleNode(xpath);
                node = doc.SelectSingleNode(xpath);
                //var value = node.InnerText;
                if (node.InnerText != "DUB")
                {
                    result = -2;
                }
            }
            return result;
        }
    }
}
