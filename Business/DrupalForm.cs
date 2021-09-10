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

    public async Task Step1()
    {
        var response = await client.GetAsync("report-a-scam");

        var form_build_id = "";

    }

    public async Task Step2()
    {
        //HttpResponseMessage response = await client.GetAsync("https://www.scamwatch.gov.au/report-a-scam");


    }
}

