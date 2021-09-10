using HtmlAgilityPack;

namespace Business;

public class DrupalForm
{
    HttpClient client;

    public DrupalForm()
    {
        client = new HttpClient
        {
            BaseAddress = new Uri("https://www.scamwatch.gov.au/")
        };
    }

    public async Task Process()
    {
        // Step 1
        var response1 = await client.GetAsync("report-a-scam");
        if (!response1.IsSuccessStatusCode)
        {
            return;
        }

        var html1 = await response1.Content.ReadAsStringAsync();
        var formBuildId1 = GetFormBuildId(html1);

        // Step 2
        var formContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("Content-Disposition: form-data; name=\"form_build_id\"", formBuildId1),
            new KeyValuePair<string, string>("Content-Disposition: form-data; name=\"form_id\"", "webform_submission_report_a_scam_node_1000_add_form"),
            new KeyValuePair<string, string>("Content-Disposition: form-data; name=\"op\"", "Next"),
            new KeyValuePair<string, string>("Content-Disposition: form-data; name=\"url\"", "")
        });
        var response2 = await client.PostAsync("report-a-scam", formContent);
        if (!response2.IsSuccessStatusCode)
        {
            return;
        }
        var html2 = await response2.Content.ReadAsStringAsync();
        var formBuildId2 = GetFormBuildId(html2);

    }

    string GetFormBuildId(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var element = doc.DocumentNode.SelectSingleNode("//input[@name='form_build_id']");
        return element.GetAttributeValue("value", "");
    }


}

