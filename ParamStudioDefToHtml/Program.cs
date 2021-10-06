using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ParamStudioDefToHtml
{
    class Program
    {
        static void Main(string[] args)
        {
#if  DEBUG
            args = new string[] {"BULLET_PARAM.xml"};
#endif
            if (!args.Any())
            {
                Console.WriteLine("Drag ParamStudio Meta Param Def files onto here to generate an html table from them.");
            }
            foreach (var path in args)
            {
                var htmlStringBuilder = new StringBuilder();
                htmlStringBuilder.AppendLine(@"||~ Name ||~ Description ||~ Notes ||");
                var htmlPath = Path.ChangeExtension(path, ".txt");
                var xFields = XDocument.Load(path).Root.Element("Field").Elements();
                foreach (var xField in xFields)
                {
                    var fieldName = xField.Name;
                    var altName = xField.Attribute("AltName")?.Value ?? "";
                    var wiki = xField.Attribute("Wiki")?.Value ?? "";
                    htmlStringBuilder.AppendLine($"|| {fieldName} || {altName} || {wiki} ||");
                }
                File.WriteAllText(htmlPath ,htmlStringBuilder.ToString());
                Console.WriteLine($"{path} => {htmlPath}");
            }
            Console.WriteLine();
            Console.WriteLine("Press ENTER to continue...");
            Console.ReadLine();
        }
    }
}