using Xunit;

namespace Business.Tests;

public class DrupalFormTest
{
    [Fact]
    public async Task SubmitAFrom()
    {
        var drupalForm = new DrupalForm();
        await drupalForm.Step1();
    }
}
