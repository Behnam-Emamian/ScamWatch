﻿using Business.Models;
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

    public async Task Process(Report report)
    {
        const string FormDataSufix = "Content-Disposition: form-data; ";
        // Step 0
        var response0 = await client.GetAsync("report-a-scam");
        if (!response0.IsSuccessStatusCode)
        {
            return;
        }
        var html0 = await response0.Content.ReadAsStringAsync();
        var formBuildId0 = GetFormBuildId(html0);

        // Step 1
        var webForm1 = new WebForm();
        webForm1.AddParameter("form_build_id", formBuildId0);
        webForm1.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm1.AddParameter("op", "Next");
        webForm1.AddParameter("url", "");

        var response1 = await client.PostAsync("report-a-scam", webForm1.GetContent());
        if (!response1.IsSuccessStatusCode)
        {
            return;
        }
        var html1 = await response1.Content.ReadAsStringAsync();
        var formBuildId1 = GetFormBuildId(html1);

        // Step 2
        var webForm2 = new WebForm();
        webForm2.AddParameter("afcx_scammer_bsb_account_number", "");
        webForm2.AddParameter("afcx_scammer_swift_code", "");
        webForm2.AddParameter("amount_lost", "");
        webForm2.AddParameter("attachments[fids]", "");
        webForm2.AddParameter("briefly_describe_the_scam", "");
        webForm2.AddParameter("form_build_id", formBuildId1);
        webForm2.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm2.AddParameter("gift_card_provider", "");
        webForm2.AddParameter("gumtree_ad_id", "");
        webForm2.AddParameter("how_was_the_money_lost[other]", "");
        webForm2.AddParameter("how_was_the_money_lost[select]", "");
        webForm2.AddParameter("how_were_you_contacted_by_the_scammer", "");
        webForm2.AddParameter("moneygram_reference_number", "");
        webForm2.AddParameter("moneygram_transaction_date", "");
        webForm2.AddParameter("name_of_online_dating_site", "");
        webForm2.AddParameter("op", "Next");
        webForm2.AddParameter("payment_provider[other]", "");
        webForm2.AddParameter("payment_provider[select]", "");
        webForm2.AddParameter("scammer_s_phone_number", "");
        webForm2.AddParameter("scammers_address_1", "");
        webForm2.AddParameter("scammers_address_2", "");
        webForm2.AddParameter("scammers_address_city_suburb", "");
        webForm2.AddParameter("scammers_address_known", "");
        webForm2.AddParameter("scammers_address_postcode", "");
        webForm2.AddParameter("scammers_address_state", "");
        webForm2.AddParameter("scammers_email", "");
        webForm2.AddParameter("scammers_facebook_post_url", "");
        webForm2.AddParameter("scammers_name", "");
        webForm2.AddParameter("scammers_website", "");
        webForm2.AddParameter("western_union_mtcn", "");
        webForm2.AddParameter("western_union_transaction_date", "");
        webForm2.AddParameter("what_type_of_scam_is_it", "");
        webForm2.AddParameter("when_were_you_first_contacted", "");
        webForm2.AddParameter("which_website_were_you_using[other]", "");
        webForm2.AddParameter("which_website_were_you_using[select]", "");
        webForm2.AddParameter("your_facebook_url", "");

        var response2 = await client.PostAsync("report-a-scam", webForm2.GetContent());
        if (!response2.IsSuccessStatusCode)
        {
            return;
        }
        var html2 = await response2.Content.ReadAsStringAsync();
        var formBuildId2 = GetFormBuildId(html2);

        // Step 3
        var webForm3 = new WebForm();
        webForm3.AddParameter("age_of_business", "");
        webForm3.AddParameter("do_you_use_a_tty_telephone_typewriter", "");
        webForm3.AddParameter("form_build_id", formBuildId2);
        webForm3.AddParameter("form_id", "webform_submission_report_a_scam_node_1000_add_form");
        webForm3.AddParameter("im_reporting", "");
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
        webForm3.AddParameter("person_targeted_email", "");
        webForm3.AddParameter("person_targeted_first_name", "");
        webForm3.AddParameter("person_targeted_gender", "");
        webForm3.AddParameter("person_targeted_last_name", "");
        webForm3.AddParameter("person_targeted_phone_number", "");
        webForm3.AddParameter("person_targeted_postcode", "");
        webForm3.AddParameter("person_targeted_state", "");

        var response3 = await client.PostAsync("report-a-scam", webForm3.GetContent());
        if (!response3.IsSuccessStatusCode)
        {
            return;
        }
        var html3 = await response2.Content.ReadAsStringAsync();
        var formBuildId3 = GetFormBuildId(html3);

        // Step 4
        var webForm4 = new WebForm();

        var response4 = await client.PostAsync("report-a-scam", webForm4.GetContent());

    }

    string GetFormBuildId(string html)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        var element = doc.DocumentNode.SelectSingleNode("//input[@name='form_build_id']");
        return element.GetAttributeValue("value", "");
    }


}

class WebForm
{
    List<KeyValuePair<string, string>> parameters = new();

    public void AddParameter(string name, string value)
    {
        parameters.Add(new KeyValuePair<string, string>($"Content-Disposition: form-data; \"{name}\"", value));
    }

    public FormUrlEncodedContent GetContent()
    {
        return new FormUrlEncodedContent(parameters);
    }
}