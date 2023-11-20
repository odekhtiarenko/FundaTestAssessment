using FundaTestAssessment.InegrationTests;
using LightBDD.Core.Configuration;
using LightBDD.Framework.Configuration;
using LightBDD.Framework.Reporting.Formatters;
using LightBDD.XUnit2;

[assembly: ConfiguredLightBddScope]
[assembly: ClassCollectionBehavior(AllowTestParallelization = false)]

namespace FundaTestAssessment.InegrationTests
{
    public class ConfiguredLightBddScopeAttribute : LightBddScopeAttribute
    {
        protected override void OnConfigure(LightBddConfiguration configuration)
        {
            configuration
                .ReportWritersConfiguration()
                .AddFileWriter<XmlReportFormatter>("~\\Reports\\FeaturesReport.xml")
                .AddFileWriter<PlainTextReportFormatter>("~\\Reports\\{TestDateTimeUtc:yyyy-MM-dd-HH_mm_ss}_FeaturesReport.txt");
        }
    }
}

