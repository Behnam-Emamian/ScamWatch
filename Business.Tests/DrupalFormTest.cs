using Business.Models;
using Xunit;

namespace Business.Tests;

public class DrupalFormTest
{
    [Fact]
    public async Task SubmitAFrom()
    {
        var drupalForm = new DrupalForm();
        var report = new Report
        {

        };
        await drupalForm.Process(report);
    }
}
