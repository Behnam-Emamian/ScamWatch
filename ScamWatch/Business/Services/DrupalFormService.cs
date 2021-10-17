using Business.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Business.Services;

public class DrupalFormService : IDrupalFormService
{
    private readonly HttpClient _client;

    public DrupalFormService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://www.scamwatch.gov.au/");

        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/html"));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xhtml+xml"));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml", 0.9));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/avif"));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/webp"));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("image/apng"));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*", 0.8));
        var signedExchange = new MediaTypeWithQualityHeaderValue("application/signed-exchange", 0.9);
        signedExchange.Parameters.Add(new NameValueHeaderValue("v", "b3"));
        _client.DefaultRequestHeaders.Accept.Add(signedExchange);

        //_client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        //_client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("deflate"));
        //_client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("br"));

        _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
        _client.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en", 0.9));

        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(Windows NT 10.0; Win64; x64)"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("AppleWebKit", "537.36"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("(KHTML, like Gecko)"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Chrome", "96.0.4655.0"));
        _client.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Safari", "537.36"));

        _client.DefaultRequestHeaders.Connection.Add("keep-alive");

    }

    public async Task<bool> Process(Report report)
    {

        #region Step 0

        var response0 = await _client.GetAsync("report-a-scam");
        if (!response0.IsSuccessStatusCode)
        {
            return false;
        }
        var html0 = await response0.Content.ReadAsStringAsync();
        var formBuildId0 = GetFormBuildId(html0);

        #endregion

        #region Step 1

        var webForm1 = new WebForm();
        webForm1.AddParameter("form_build_id", formBuildId0);
        webForm1.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm1.AddParameter("op", "Next");
        webForm1.AddParameter("url", "");

        var response1 = await _client.PostAsync("report-a-scam", webForm1.GetContent());
        if (!response1.IsSuccessStatusCode)
        {
            return false;
        }
        var html1 = await response1.Content.ReadAsStringAsync();
        var formBuildId1 = GetFormBuildId(html1);

        #endregion

        #region Step 2

        var webForm2 = new WebForm();
        webForm2.AddParameter("afcx_scammer_bsb_account_number", "");
        webForm2.AddParameter("afcx_scammer_swift_code", "");
        webForm2.AddParameter("amount_lost", "");
        webForm2.AddParameter("attachments[fids]", "");
        webForm2.AddParameter("briefly_describe_the_scam", report.Description);
        webForm2.AddParameter("form_build_id", formBuildId1);
        webForm2.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm2.AddParameter("gift_card_provider", "");
        webForm2.AddParameter("gumtree_ad_id", "");
        webForm2.AddParameter("how_was_the_money_lost[other]", "");
        webForm2.AddParameter("how_was_the_money_lost[select]", "");
        webForm2.AddParameter("how_were_you_contacted_by_the_scammer", report.DeliveryMethod.ToString());
        webForm2.AddParameter("moneygram_reference_number", "");
        webForm2.AddParameter("moneygram_transaction_date", "");
        webForm2.AddParameter("name_of_online_dating_site", "");
        webForm2.AddParameter("op", "Next");
        webForm2.AddParameter("payment_provider[other]", "");
        webForm2.AddParameter("payment_provider[select]", "");
        webForm2.AddParameter("scammer_s_phone_number", report.ScammerPhoneNumber);
        webForm2.AddParameter("scammers_address_1", "");
        webForm2.AddParameter("scammers_address_2", "");
        webForm2.AddParameter("scammers_address_city_suburb", "");
        webForm2.AddParameter("scammers_address_known", "no");
        webForm2.AddParameter("scammers_address_postcode", "");
        webForm2.AddParameter("scammers_address_state", "");
        webForm2.AddParameter("scammers_email", "");
        webForm2.AddParameter("scammers_facebook_post_url", "");
        webForm2.AddParameter("scammers_name", "");
        webForm2.AddParameter("scammers_website", "");
        webForm2.AddParameter("western_union_mtcn", "");
        webForm2.AddParameter("western_union_transaction_date", "");
        webForm2.AddParameter("what_type_of_scam_is_it", $"{(int)report.ScamType}");
        webForm2.AddParameter("when_were_you_first_contacted", report.FirstContactDate.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture));
        webForm2.AddParameter("which_website_were_you_using[other]", "");
        webForm2.AddParameter("which_website_were_you_using[select]", "");
        webForm2.AddParameter("your_facebook_url", "");

        var response2 = await _client.PostAsync("report-a-scam", webForm2.GetContent());
        if (!response2.IsSuccessStatusCode)
        {
            return false;
        }
        var html2 = await response2.Content.ReadAsStringAsync();
        var formBuildId2 = GetFormBuildId(html2);

        #endregion

        #region Step 3

        var webForm3 = new WebForm();
        webForm3.AddParameter("age_of_business", "");
        webForm3.AddParameter("do_you_use_a_tty_telephone_typewriter", "no");
        webForm3.AddParameter("form_build_id", formBuildId2);
        webForm3.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm3.AddParameter("im_reporting", "as_myself");
        webForm3.AddParameter("name_of_business", "");
        webForm3.AddParameter("number_of_employees", "");
        webForm3.AddParameter("op", "Preview");
        webForm3.AddParameter("person_reporting_address_1", "");
        webForm3.AddParameter("person_reporting_address_2", "");
        webForm3.AddParameter("person_reporting_city_suburb", "");
        webForm3.AddParameter("person_reporting_email", "");
        webForm3.AddParameter("person_reporting_name", "");
        webForm3.AddParameter("person_reporting_phone", "");
        webForm3.AddParameter("person_reporting_postcode", "");
        webForm3.AddParameter("person_reporting_state", "");
        webForm3.AddParameter("person_targeted_address_1", "");
        webForm3.AddParameter("person_targeted_address_2", "");
        webForm3.AddParameter("person_targeted_age", "");
        webForm3.AddParameter("person_targeted_city_suburb", "");
        webForm3.AddParameter("person_targeted_email", report.TargetEmail);
        webForm3.AddParameter("person_targeted_first_name", report.TargetFirstName);
        webForm3.AddParameter("person_targeted_gender", report.TargetGender.ToString());
        webForm3.AddParameter("person_targeted_last_name", report.TargetLastName);
        webForm3.AddParameter("person_targeted_phone_number", report.TargetPhoneNumber);
        webForm3.AddParameter("person_targeted_postcode", "");
        webForm3.AddParameter("person_targeted_state", report.TargetState.ToString());

        var response3 = await _client.PostAsync("report-a-scam", webForm3.GetContent());
        if (!response3.IsSuccessStatusCode)
        {
            return false;
        }
        var html3 = await response2.Content.ReadAsStringAsync();
        var formBuildId3 = GetFormBuildId(html3);

        #endregion

        #region Step 4

        var webForm4 = new WebForm();
        webForm4.AddParameter("form_build_id", formBuildId3);
        webForm4.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm4.AddParameter("op", "Submit");
        webForm4.AddParameter("share_report_government", "yes");
        webForm4.AddParameter("share_report_private_sector", "yes");
        webForm4.AddParameter("would_you_be_willing_to_share_your_story", "yes_anonymous");
        var response4 = await _client.PostAsync("report-a-scam", webForm4.GetContent());
        if (!response4.IsSuccessStatusCode)
        {
            return false;
        }

        #endregion

        return true;
    }

    private static string GetFormBuildId(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var element = doc.DocumentNode.SelectSingleNode("//input[@name='form_build_id']");
        return element.GetAttributeValue("value", "");
    }
}

internal class WebForm
{
    private readonly List<KeyValuePair<string, string>> parameters = new();

    public void AddParameter(string name, string value)
    {
        parameters.Add(new KeyValuePair<string, string>($"Content-Disposition: form-data; name=\"{name}\"", value));
    }

    public FormUrlEncodedContent GetContent()
    {
        return new FormUrlEncodedContent(parameters);
    }
}

public interface IDrupalFormService
{
    Task<bool> Process(Report report);
}